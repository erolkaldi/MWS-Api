
using MWSApp.IdentityServices.Features.Users.Queries;

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
        public async Task<IActionResult> GetToken([FromBody] GetTokenCommand command) 
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result); 
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _mediator.Send(new RefreshTokenCommand() { Token= refreshToken ?? "" });
            if (result.Success)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeToken([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                token = Request.Cookies["refreshToken"] ?? ""; 
            }
            return Ok(await _mediator.Send(new RevokeTokenCommand() { Token = token }));
        }
        [HttpGet]
        public async Task<IActionResult> GetRefreshTokens([FromQuery] string userId)
        {
            return Ok(await _mediator.Send(new GetRefreshTokensQuery() { UserId = userId ?? "" }));
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
