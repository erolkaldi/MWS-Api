using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWSApp.LogServices.Features.CompanyLogs.Queries;

namespace MWSApp.LogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
