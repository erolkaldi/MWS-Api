

namespace MWSApp.CompanyServices.Features.Companies.Queries
{
    public class GetCompanyQuery : IRequest<ActionResponse<CompanyDto>>
    {
        public string Id { get; set; } = "";
    }
    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, ActionResponse<CompanyDto>>
    {
        IRepository<Company> _repository;
        public GetCompanyHandler(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse<CompanyDto>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse<CompanyDto>();

            string query = "select Id,Name,Email from Companies With(Nolock) Where Id='"+request.Id+"' order by Name";
            response.Data = _repository.GetListSqlAsync<CompanyDto>(query).Result.FirstOrDefault();

            return response;
        }
    }
}
