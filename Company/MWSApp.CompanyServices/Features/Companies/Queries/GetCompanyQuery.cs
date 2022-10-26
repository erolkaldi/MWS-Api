


namespace MWSApp.CompanyServices.Features.Companies.Queries
{
    public class GetCompanyQuery : IRequest<ActionResponse<CompanyDto>>
    {
        public string Id { get; set; } = "";
    }
    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, ActionResponse<CompanyDto>>
    {
        IRepository<Company> _repository;
        ICacheService _cacheService;
        public GetCompanyHandler(IRepository<Company> repository,ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<ActionResponse<CompanyDto>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse<CompanyDto>();
            response.Data = _cacheService.Get<CompanyDto>(request.Id);
            if (response.Data == null)
            {
                string query = "select Id,Name,Email from Companies With(Nolock) Where Id='" + request.Id + "'";
                response.Data = _repository.GetListSqlAsync<CompanyDto>(query).Result.FirstOrDefault();
                _cacheService.Set(request.Id, response.Data);
            }
            

            return response;
        }
    }
}
