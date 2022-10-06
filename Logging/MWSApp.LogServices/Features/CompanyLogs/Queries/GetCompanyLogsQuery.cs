



namespace MWSApp.LogServices.Features.CompanyLogs.Queries
{
    public class GetCompanyLogsQuery : IRequest<ActionResponse<List<LogDto>>>
    {
        public string TableName { get; set; }
        public string UserName { get; set; }
        public DateTime  DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid TableId { get; set; }
    }
    public class GetCompanyLogsHandler : IRequestHandler<GetCompanyLogsQuery, ActionResponse<List<LogDto>>>
    {
        IRepository<CompanyLog> _repository;
        private readonly IUserRepository _userRepository;
        public GetCompanyLogsHandler(IRepository<CompanyLog> repository,IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse<List<LogDto>>> Handle(GetCompanyLogsQuery request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse<List<LogDto>>();
            string query = "Set DateFormat dmy select ActionDate,UserName,TableName,FieldName,OldValue,NewValue from CompanyLogs With(Nolock) where CompanyId='"+_userRepository.User.CompanyId.ToString()+"' ";
            if(request.DateBegin!=new DateTime(1, 1, 1))
            {
                query+="and ActionDate>='"+request.DateBegin.ToString("dd.MM.yyyy HH:mm")+"' ";
            }
            if (request.DateEnd != new DateTime(1, 1, 1))
            {
                request.DateEnd = new DateTime(request.DateEnd.Year, request.DateEnd.Month, request.DateEnd.Day,23,59,59);
                query += "and ActionDate<='" + request.DateEnd.ToString("dd.MM.yyyy HH:mm") + "' ";
            }
            if (!string.IsNullOrEmpty(request.TableName)) query += " and TableName='" + request.TableName + "' ";
            if (!string.IsNullOrEmpty(request.UserName)) query += " and UserName='" + request.TableName + "' ";
            response.Data = await _repository.GetListSqlAsync<LogDto>(query);

            return response;
        }
    }
}
