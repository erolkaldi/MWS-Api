

namespace MWSApp.CompanyServices.Features.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<ActionResponse>
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, ActionResponse>
    {
        IRepository<Company> _repository;
        IRepository<CompanyUser> _companyUserRepository;
        private readonly IUserRepository _userRepository;
        public CreateCompanyHandler(IRepository<Company> repository, IRepository<CompanyUser> companyUserRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _companyUserRepository = companyUserRepository;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            if(_repository.Get(p=> p.Name==request.Name).FirstOrDefault() != null)
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "Company exists";
                return response;
            }
            if (_repository.Get(p => p.Email == request.Email).FirstOrDefault() != null)
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "Email allready used for another company";
                return response;
            }
            if(_companyUserRepository.Get(p=> p.UserId == _userRepository.User.UserId).FirstOrDefault() != null)
            {
                response.ResponseType = CommonModels.Enums.ResponseType.Warning;
                response.Message = "You are allready registered to a company";
                return response;
            }
            Company company = new Company(request.Name,request.Email);
            CompanyUser companyUser = new CompanyUser();
            companyUser.Confirmed = false;
            companyUser.UserId = _userRepository.User.UserId;
            companyUser.Id=Guid.NewGuid();
            companyUser.CompanyId = company.Id;
            companyUser.Creator = true;
            await _repository.AddAsync(company);
            await _companyUserRepository.AddAsync(companyUser);
            await _repository.SaveChangesAsync();
            response.Id = company.Id.ToString();
            return response;
        }
    }
}
