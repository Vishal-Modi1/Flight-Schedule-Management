using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.VM;

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
