

namespace MWSApp.LogServices.Features.CompanyLogs.Commands
{
    public class CreateCompanyLogCommand : IRequest<ActionResponse>
    {
        public DateTime ActionDate { get; set; }
        public string UserName { get; set; } = "";
        public string ProjectName { get; set; } = "";
        public string TableName { get; set; } = "";
        public string FieldName { get; set; } = "";
        public string OldValue { get; set; } = "";
        public string NewValue { get; set; } = "";
        public Guid CompanyId { get; set; }
        public Guid TableId { get; set; }
    }
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyLogCommand, ActionResponse>
    {
        IRepository<CompanyLog> _repository;
        IMapper _mapper;
        public CreateCompanyHandler(IRepository<CompanyLog> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResponse> Handle(CreateCompanyLogCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            CompanyLog log = _mapper.Map<CompanyLog>(request);
            log.Id =Guid.NewGuid();
            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
            response.Id = log.Id.ToString();
            return response;
        }
    }
}
