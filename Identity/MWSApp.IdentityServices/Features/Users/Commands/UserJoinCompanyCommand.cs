

namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class UserJoinCompanyCommand : IRequest<ActionResponse>
    {
        public Guid CompanyId { get; set; }
    }

    public class UserJoinCompanyCommandHandler : IRequestHandler<UserJoinCompanyCommand, ActionResponse>
    {
        readonly UserManager<AppUser> _userManager;
        private RabbitMQSetting _rabbitMQSetting = new RabbitMQSetting();

        IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserJoinCompanyCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, IBus bus, IUserRepository userRepository)
        {
            _userManager = userManager;
            configuration.GetSection("RabbitMQSetting").Bind(_rabbitMQSetting);
            _bus = bus;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse> Handle(UserJoinCompanyCommand request, CancellationToken cancellationToken)
        {
            ActionResponse response = new();
            try
            {
                
                AppUser user = await _userManager.FindByIdAsync(_userRepository.User.UserId.ToString());

                if (user==null)
                {
                    response.ResponseType = ResponseType.Error;
                    response.Message = "User not found";
                    return response;
                }
                user.CompanyId = request.CompanyId;
                await _userManager.UpdateAsync(user);
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
