using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM.Common;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            CurrentResponse response = _statusService.List();

            return Ok(response);
        }

    }
}
