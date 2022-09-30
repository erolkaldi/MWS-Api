
namespace MWSApp.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command) => Ok(await _mediator.Send(command));
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] GetTokenCommand command) => Ok(await _mediator.Send(command));
    }
}
