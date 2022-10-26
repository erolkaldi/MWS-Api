

namespace MWSApp.CompanyServices.Features.Companies.Queries
{
    public class GetCompaniesQuery : IRequest<ActionResponse<List<CompanyDto>>>
    {
    }
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, ActionResponse<List<CompanyDto>>>
    {
        IRepository<Company> _repository;
        IUserRepository _userRepository;
        public GetCompaniesHandler(IRepository<Company> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse<List<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var response =new ActionResponse<List<CompanyDto>>();

            string query = "select Id,Name,Email from Companies With(Nolock) order by Name";
            response.Data =await _repository.GetListSqlAsync<CompanyDto>(query);

            return response;
        }
    }
}
