using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM.Common;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EquipmentStatusController : Controller
    {
        private readonly IEquipmentStatusService _equipmentStatusService;

        public EquipmentStatusController(IEquipmentStatusService equipmentStatusService)
        {
            _equipmentStatusService = equipmentStatusService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            CurrentResponse response = _equipmentStatusService.List();

            return Ok(response);
        }

    }
}
