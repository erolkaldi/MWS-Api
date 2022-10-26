

namespace MWSApp.CompanyServices.Features.Companies.Commands
{
    public class UpdateCompanyCommand :IRequest<ActionResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, ActionResponse>
    {
        IRepository<Company> _repository;
        ICacheService _cacheService;
        public UpdateCompanyHandler(IRepository<Company> repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;

        }

        public async Task<ActionResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            Company company = await _repository.GetByIdAsync(request.Id);
            company.Name = request.Name;
            company.Email = request.Email;
            _repository.Update(company);
            await _repository.SaveChangesAsync();
            response.Id = company.Id.ToString();
            _cacheService.Delete(response.Id);
            return response;
        }
    }
}
