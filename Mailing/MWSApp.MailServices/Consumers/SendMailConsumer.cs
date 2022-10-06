


namespace MWSApp.MailServices.Consumers
{
  
    public class SendMailConsumer : IConsumer<MailQueueMessage>
    {
        private readonly SmtpSetting smtpSetting=new SmtpSetting();
        private readonly RabbitMQSetting _rabbitMQSetting=new RabbitMQSetting();
        private IBus _bus;
        public SendMailConsumer(IOptions<RabbitMQSetting> rabbitMQSetting,IOptions<SmtpSetting> smtpOption, IBus bus,IConfiguration configuration)
        {
            configuration.GetSection("RabbitMQSetting").Bind(_rabbitMQSetting);
            configuration.GetSection("Smtp").Bind(smtpSetting);

            _bus =bus;
        }

        public async Task Consume(ConsumeContext<MailQueueMessage> context)
        {
            Chilkat.Email email =new Chilkat.Email();
            Chilkat.MailMan mailman = new Chilkat.MailMan();
            var success = mailman.UnlockComponent("KODYAZ.CB1102022_WsMgZPzt8Xpd");
            if (!success)
            {
                throw new Exception();
            }
            else
            {
                mailman.SmtpHost = smtpSetting.Host;
                mailman.SmtpUsername = smtpSetting.User;
                mailman.SmtpPassword = smtpSetting.Password;
                mailman.SmtpPort = smtpSetting.Port;
                mailman.SmtpSsl = smtpSetting.Ssl;
                mailman.StartTLS = true;

                email = new Chilkat.Email() { From = smtpSetting.User, Subject = context.Message.Subject, Body = context.Message.Body };
                email.AddTo(string.Empty, context.Message.To);
                email.UnSpamify();
                email.ReplyTo = smtpSetting.User;
                success = mailman.SendEmail(email);
            }
            if (!success)
            {
                MailLogQueueMessage mailLogQueueMessage = new MailLogQueueMessage();
                mailLogQueueMessage.Subject = context.Message.Subject;
                mailLogQueueMessage.Body = context.Message.Body;
                mailLogQueueMessage.To = context.Message.To;
                mailLogQueueMessage.Error = mailman.LastErrorText;
                Uri uri = new Uri(_rabbitMQSetting.RabbitUri + "/" + _rabbitMQSetting.MailLogQueue);
                var endPoint = await _bus.GetSendEndpoint(uri);
                endPoint.Send(mailLogQueueMessage);
            }
        }
    }
}
