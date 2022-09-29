

namespace MWSApp.CompanyServices.Features.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<ActionResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, ActionResponse>
    {
        IRepository<Company> _repository;
        public CreateCompanyHandler(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            Company company = new Company(request.Name,request.Email);
            await _repository.AddAsync(company);
            await _repository.SaveChangesAsync();
            response.Id = company.Id.ToString();
            return response;
        }
    }
}
