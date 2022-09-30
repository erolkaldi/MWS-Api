

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

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
