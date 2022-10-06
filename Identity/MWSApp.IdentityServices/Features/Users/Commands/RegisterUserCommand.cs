

namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<ActionResponse>
    {
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public Guid? CompanyId { get; set; }
    }
  
    public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, ActionResponse>
    {
        readonly UserManager<AppUser> _userManager;
        private RabbitMQSetting _rabbitMQSetting = new RabbitMQSetting();

        private readonly IBus _bus;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, IBus bus)
        {
            _userManager = userManager;
            configuration.GetSection("RabbitMQSetting").Bind(_rabbitMQSetting);
            _bus = bus;
        }

        public async Task<ActionResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ActionResponse response = new();
            try
            {
                if (request.Password != request.ConfirmPassword)
                {
                    response.ResponseType = ResponseType.Error;
                    response.Message = "Confirm password fail";
                    return response;
                }
                AppUser user = new() { Id = Guid.NewGuid().ToString(), UserName = request.Name, FullName = request.FullName, Email = request.Email };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    response.ResponseType = ResponseType.OK;
                    MailQueueMessage message = new MailQueueMessage();
                    message.Subject = "Wellcome to MWS App world";
                    message.To = request.Email;
                    message.Body = "Merhaba " + request.FullName + "\r\n";
                    message.Body += "Lütfen alttaki linke tıklayarak aktivasyonunuzu tamamlayın.\r\n";
                    message.Body += "http://localhost:5555/api/public/confirmemail?Id=" + user.Id;
                    Uri uri = new Uri(_rabbitMQSetting.RabbitUri + "/" + _rabbitMQSetting.MailQueue);
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    endPoint.Send(message);
                }
                else
                {
                    response.ResponseType = ResponseType.Error;
                    response.Message = "";
                    foreach (var err in result.Errors)
                    {
                        response.Message += err.Code + "-" + err.Description + "\n";
                    }
                }
            }
            catch (Exception exc)
            {
                response.ResponseType = ResponseType.Exception;
                response.Message = exc.Message;

            }
            return response;
        }
    }
}
