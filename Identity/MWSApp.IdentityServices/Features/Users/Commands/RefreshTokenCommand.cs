using MWSApp.IdentityContexts;
using MWSApp.IdentityServices.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class RefreshTokenCommand : IRequest<Token>
    {
        public string Token { get; set; } = "";
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Token>
    {
        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;
        TokenTools tokenTools = new TokenTools();
        IdentityContext _IdentityContext;
        public RefreshTokenCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, IdentityContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _IdentityContext = context;
        }

        public async Task<Token> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            Token response = new();
            try
            {
                if (request.Token == "")
                {
                    response.Success = false;
                    response.Message = "RefreshToken not found in Cookies";
                    return response;
                }
                RefreshToken refreshToken =_IdentityContext.RefreshToken.Where(x => x.Token == request.Token).FirstOrDefault();
                if (refreshToken == null)
                {
                    response.Success = false;
                    response.Message = "RefreshToken not found in Database";
                    return response;
                }
                if (!refreshToken.IsActive)
                {
                    response.Success = false;
                    response.Message = "RefreshToken not active";
                    return response;
                }
                AppUser user = await _userManager.FindByIdAsync(refreshToken.UserId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                if (!user.EmailConfirmed)
                {
                    response.Success = false;
                    response.Message = "Please confirm email first.We sent you a confirmation email";
                    return response;
                }
                
                response.Success = true;
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id),
                        new Claim("DisplayName", user.FullName),
                        new Claim("Email", user.Email),
                        new Claim("UserName",user.UserName),
                        new Claim("CompanyId", Guid.NewGuid().ToString()),
                    };
                response.Expiration = DateTime.UtcNow.AddHours(7);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokn = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    null,
                    claims,
                    expires: response.Expiration,
                    signingCredentials: signIn);
                response.AccessToken = new JwtSecurityTokenHandler().WriteToken(tokn);
                refreshToken.Revoked = DateTime.UtcNow;
                var newRefreshToken = tokenTools.CreateRefreshToken();
                response.RefreshToken = newRefreshToken.Token;
                response.RefreshTokenExpiration = newRefreshToken.Expires;
                newRefreshToken.Id = Guid.NewGuid();
                newRefreshToken.UserId = user.Id;
                _IdentityContext.RefreshToken.Add(refreshToken);
                _IdentityContext.Update(refreshToken);
                _IdentityContext.SaveChanges();
                
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;

            }
            return response;
        }
    }
}
