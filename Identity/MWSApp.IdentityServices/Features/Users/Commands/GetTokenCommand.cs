

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class GetTokenCommand : IRequest<Token>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, Token>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IConfiguration _configuration;

        public GetTokenCommandHandler(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration=configuration;
        }

        public async Task<Token> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            Token response = new();
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                var sign =await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!sign.Succeeded)
                {
                    response.Success=false;
                    response.Message = "Password is wrong";
                    return response;
                }
                response.Success = true;
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id),
                        new Claim("DisplayName", user.FullName),
                        new Claim("Email", user.Email)
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
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;

            }
            return response;
        }
    }
}
