using MWSApp.IdentityContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.IdentityServices.Features.Users.Queries
{
   
    public class GetRefreshTokensQuery : IRequest<ActionResponse<List<RefreshToken>>>
    {
        public string UserId { get; set; } = "";
    }
    public class GetRefreshTokensQueryHandler : IRequestHandler<GetRefreshTokensQuery, ActionResponse<List<RefreshToken>>>
    {
        IdentityContext _IdentityContext;
        public GetRefreshTokensQueryHandler(IdentityContext context)
        {
            _IdentityContext = context;
        }

        public async Task<ActionResponse<List<RefreshToken>>> Handle(GetRefreshTokensQuery request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse<List<RefreshToken>>();

            response.Data = _IdentityContext.RefreshToken.Where(p=> p.UserId==request.UserId).ToList();

            return response;
        }
    }
}
