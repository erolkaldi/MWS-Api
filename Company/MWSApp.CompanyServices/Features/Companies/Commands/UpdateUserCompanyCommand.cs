

namespace MWSApp.CompanyServices.Features.Companies.Commands
{
    public class UpdateUserCompanyCommand : IRequest<ActionResponse>
    {
        public Guid CompanyId { get; set; }
    }
    public class UpdateUserCompanyHandler : IRequestHandler<UpdateUserCompanyCommand, ActionResponse>
    {
        IRepository<Company> _repository;
        IRepository<CompanyUser> _companyUserRepository;
        IUserRepository _userRepository;
        public UpdateUserCompanyHandler(IRepository<Company> repository,IRepository<CompanyUser> companyUserRepository,IUserRepository userRepository)
        {
            _repository = repository;
            _companyUserRepository = companyUserRepository;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse> Handle(UpdateUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            Company company = await _repository.GetByIdAsync(request.CompanyId);
            CompanyUser user = await _companyUserRepository.FirstOrDefaultAsync(p=> p.UserId==_userRepository.User.UserId);
            if (user == null)
            {
                user=new CompanyUser();
                user.Creator = false;
                user.UserId = _userRepository.User.UserId;
                user.Id=Guid.NewGuid();
                user.CompanyId =request.CompanyId;
                await _companyUserRepository.AddAsync(user);
                await _companyUserRepository.SaveChangesAsync();
                //TODO : Send Mail To Company For Confirmaiton
            }
            else
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "You joined a company before.Company Name : " + company.Name;
            }
            return response;
        }
    }
}
