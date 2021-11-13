using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
using Service.Interface;
using System;
using ViewModels.VM;
using API.Utilities;
using DataModels.Constants;
using DataModels.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.VM;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AirCraftEquipmentController : Controller
    {
        private readonly IAirCraftEquipmentService _airCraftEquipmentService;
        private readonly JWTTokenGenerator _jWTTokenGenerator;

        public AirCraftEquipmentController(IAirCraftEquipmentService airCraftEquipmentService, IHttpContextAccessor httpContextAccessor)
        {
            _airCraftEquipmentService = airCraftEquipmentService;
            _jWTTokenGenerator = new JWTTokenGenerator(httpContextAccessor.HttpContext);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AirCraftEquipmentsVM airCraftEquipmentsVM)
        {
            airCraftEquipmentsVM.CreatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));
            CurrentResponse response = _airCraftEquipmentService.Create(airCraftEquipmentsVM);

            return Ok(response);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(AirCraftEquipmentsVM airCraftEquipmentsVM)
        {
            airCraftEquipmentsVM.UpdatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));
            CurrentResponse response = _airCraftEquipmentService.Edit(airCraftEquipmentsVM);

            return Ok(response);
        }

        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            CurrentResponse response = _airCraftEquipmentService.Delete(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List(int airCraftId)
        {
            CurrentResponse response = _airCraftEquipmentService.List(airCraftId);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetchbyid")]
        public IActionResult fetchbyid(int id)
        {
            CurrentResponse response = _airCraftEquipmentService.Fetch(id);
            return Ok(response);
        }
    }
}
