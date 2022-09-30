

namespace MWSApp.LogServices.Consumers
{
    public class CreateLogConsumer :IConsumer<LogQueueMessage>
    {
        IRepository<CompanyLog> _repository;
        IMapper _mapper;
        public CreateLogConsumer(IRepository<CompanyLog> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<LogQueueMessage> context)
        {
            if (context.Message.ProjectName=="Company")
            {
                CompanyLog log = _mapper.Map<CompanyLog>(context.Message);
                log.Id = Guid.NewGuid();
                await _repository.AddAsync(log);
                await _repository.SaveChangesAsync(); 
            }
        }
    }
}
