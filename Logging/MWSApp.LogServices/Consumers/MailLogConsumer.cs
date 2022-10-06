

namespace MWSApp.LogServices.Consumers
{
    public class MailLogConsumer : IConsumer<MailLogQueueMessage>
    {
        IRepository<MailLog> _repository;
        IMapper _mapper;
        public MailLogConsumer(IRepository<MailLog> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<MailLogQueueMessage> context)
        {
            MailLog log = new MailLog();
            log.Id = Guid.NewGuid();
            log.ActionDate = log.ActionDate.ToLocalTime();
            log.Body = context.Message.Body;
            log.Subject = context.Message.Subject;
            log.Email = context.Message.To;
            log.Error = context.Message.Error;
            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
        }
    }
}
