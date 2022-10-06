
namespace MWSApp.IdentityServices.Features.Users.Commands
{
    public class ConfirmEmailCommand : IRequest<ActionResponse>
    {
        public string Id { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ActionResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            ActionResponse response = new();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.ResponseType = ResponseType.Error;
                    response.Message = "Id missing";
                    return response;
                }
                AppUser user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    response.ResponseType = ResponseType.Error;
                    response.Message = "User not found";
                    return response;
                }
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    var resp =await _userManager.UpdateAsync(user);
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
