using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CompanyServices.Features.Companies.Queries
{
    public class GetUserCompanyQuery : IRequest<ActionResponse<UserCompanyInfo>>
    {
        public string Id { get; set; } = "";
    }
    public class GetUserCompanyQueryHandler : IRequestHandler<GetUserCompanyQuery, ActionResponse<UserCompanyInfo>>
    {
        IRepository<CompanyUser> _repository;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public GetUserCompanyQueryHandler(IRepository<CompanyUser> repository,IMediator mediator, IUserRepository userRepository)
        {
            _repository = repository;
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse<UserCompanyInfo>> Handle(GetUserCompanyQuery request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse<UserCompanyInfo>();

            if (string.IsNullOrEmpty(request.Id)) request.Id = _userRepository.User.UserId.ToString();
            var userCompany=await _repository.FirstOrDefaultAsync(x => x.UserId ==Guid.Parse( request.Id));
            if (userCompany == null)
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "Company not found";
                return response;
            }
            if (userCompany.Confirmed == false)
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "Company not confirmed";
                return response;
            }
            var company = await _mediator.Send(new GetCompanyQuery() { Id=userCompany.CompanyId.ToString()});
            response.Data = new () { CompanyId=userCompany.CompanyId,CompanyName=company.Data.Name,Creator=userCompany.Creator};
           
            return response;
        }
    }
}
