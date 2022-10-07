using MWSApp.IdentityContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class RevokeTokenCommand : IRequest<ActionResponse>
    {
        public string Token { get; set; } = "";
    }

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, ActionResponse>
    {
        IdentityContext _IdentityContext;
        public RevokeTokenCommandHandler(IdentityContext context)
        {
            _IdentityContext = context;
        }

        public async Task<ActionResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            ActionResponse response = new();
            try
            {
                RefreshToken refreshToken = _IdentityContext.RefreshToken.SingleOrDefault(p => p.Token == request.Token && p.IsActive);
                if (refreshToken == null)
                {
                    response.ResponseType = ResponseType.Warning;
                    response.Message = "Active refreshtoken not found"; 
                    return response;
                }
                refreshToken.Revoked = DateTime.UtcNow;
                _IdentityContext.Update(refreshToken);
                _IdentityContext.SaveChanges();
                response.ResponseType = ResponseType.OK;
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.ResponseType=ResponseType.Error;

            }
            return response;
        }
    }
}
