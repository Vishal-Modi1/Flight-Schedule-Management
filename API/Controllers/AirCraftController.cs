using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
        public AirCraftController()
        {

        }

        private readonly IAirCraftService _airCraftService;

        public AirCraftController(IAirCraftService airCraftService)
        {
            _airCraftService = airCraftService;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult List(DatatableParams datatableParams)
        {
            CurrentResponse response = _airCraftService.List(datatableParams);

            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AirCraftVM airCraftVM)
        {
            CurrentResponse response = _airCraftService.Create(airCraftVM);
            return Ok(response);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(AirCraftVM airCraftVM)
        {
            CurrentResponse response = _airCraftService.Edit(airCraftVM);
            return Ok(response);
        }

        [HttpGet]
        [Route("getDetails")]
        public IActionResult GetDetails(int id)
        {
            CurrentResponse response = _airCraftService.GetDetails(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            CurrentResponse response = _airCraftService.Delete(id);

            return Ok(response);
        }

    }
}
