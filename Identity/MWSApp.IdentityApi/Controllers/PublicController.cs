

namespace MWSApp.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PublicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string Id){
            //TODO : Must be handled by UI 
            var response = await _mediator.Send(new ConfirmEmailCommand() { Id = Id });
            if(response.ResponseType == CommonModels.Enums.ResponseType.OK)
            {
                return Ok("Thank you.Confirmation successfull");
            }
            return Ok(response.Message);
        }
    }
}
