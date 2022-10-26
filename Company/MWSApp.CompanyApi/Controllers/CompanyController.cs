


namespace MWSApp.CompanyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies() => Ok(await _mediator.Send(new GetCompaniesQuery()));
        [HttpGet]
        public async Task<IActionResult> GetCompany([FromHeader]string Id) => Ok(await _mediator.Send(new GetCompanyQuery() { Id=Id}));
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command) => Ok(await _mediator.Send(command));
        [HttpPost]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand command) => Ok(await _mediator.Send(command));
        [HttpGet]
        public async Task<IActionResult> GetUserCompany() => Ok(await _mediator.Send(new GetUserCompanyQuery()));
        [HttpPost]
        public async Task<IActionResult> UpdateUserCompany([FromBody] UpdateUserCompanyCommand command) => Ok(await _mediator.Send(command));
    }
}
