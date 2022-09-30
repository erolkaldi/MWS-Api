


namespace MWSApp.LogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> GetLogs([FromBody]GetCompanyLogsQuery query) => Ok(await _mediator.Send(query));
    }
}
