

namespace MWSApp.CompanyServices.Features.Companies.Queries
{
    public class GetCompaniesQuery : IRequest<ActionResponse<List<CompanyDto>>>
    {
    }
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, ActionResponse<List<CompanyDto>>>
    {
        IRepository<Company> _repository;
        public GetCompaniesHandler(IRepository<Company> repository)
        {
            _repository = repository;
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
