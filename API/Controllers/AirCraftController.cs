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
    public class AirCraftController : ControllerBase
    {
        private readonly IAircraftService _airCraftService;
        private readonly IAircraftEquipementTimeService _aircraftEquipementTimeService;
        private readonly IAircraftMakeService _aircraftMakeService;
        private readonly IAircraftModelService _aircraftModelService;
        private readonly JWTTokenGenerator _jWTTokenGenerator;
        private readonly FileUploader _fileUploader;

        public AirCraftController(IAircraftService airCraftService, IAircraftMakeService aircraftMakeService,
            IAircraftModelService aircraftModelService, IAircraftEquipementTimeService aircraftEquipementTimeService,
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _airCraftService = airCraftService;
            _aircraftMakeService = aircraftMakeService;
            _aircraftModelService = aircraftModelService;
            _aircraftEquipementTimeService = aircraftEquipementTimeService;
            _jWTTokenGenerator = new JWTTokenGenerator(httpContextAccessor.HttpContext);
            _fileUploader = new FileUploader(webHostEnvironment);
        }

        [HttpPost]
        [Route("list")]
        public IActionResult List(AircraftFilterVM aircraftFilterVM)
        {
            CurrentResponse response = _airCraftService.List(aircraftFilterVM);

            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AirCraftVM airCraftVM)
        {
            airCraftVM.CreatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));
            CurrentResponse response = _airCraftService.Create(airCraftVM);

            return Ok(response);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(AirCraftVM airCraftVM)
        {
            airCraftVM.UpdatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));
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

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFileAsync()
        {
            if (!Request.HasFormContentType)
            {
                return Ok(false);
            }

            IFormCollection form = Request.Form;

            string fileName = $"{DateTime.Now.ToString("yyyyMMddHHMMss")}_{form.Files[0].FileName}.jpeg";
            bool isFileUploaded = await _fileUploader.UploadAsync(UploadDirectory.AircraftImage, form, fileName);

            CurrentResponse response = new CurrentResponse();
            response.Data = "false";

            if (isFileUploaded)
            {
                response = _airCraftService.UpdateImageName(Convert.ToInt32(form.Files[0].FileName), fileName);
            }

            return Ok(response);
        }

        #region Aircraft Make

        [HttpPost]
        [Route("createmake")]
        public IActionResult CreateMake(AircraftMake aircraftMake)
        {
            CurrentResponse response = _aircraftMakeService.Create(aircraftMake);

            return Ok(response);
        }

        [HttpGet]
        [Route("makelist")]
        public IActionResult MakeList()
        {
            CurrentResponse response = _aircraftMakeService.List();

            return Ok(response);
        }

        #endregion

        #region Aircraft Model

        [HttpPost]
        [Route("createmodel")]
        public IActionResult CreateModel(AircraftModel aircraftModel)
        {
            CurrentResponse response = _aircraftModelService.Create(aircraftModel);

            return Ok(response);
        }

        [HttpGet]
        [Route("modellist")]
        public IActionResult ModelList()
        {
            CurrentResponse response = _aircraftModelService.List();

            return Ok(response);
        }

        #endregion

        #region Aircraft Equipment
        [HttpPost]
        [Route("createaircraftequipment")]
        public IActionResult CreateAircraftEquipment(List<AircraftEquipmentTimeVM> aircraftEquipmentTimeVM)
        {
            int createdBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));
            CurrentResponse response = new CurrentResponse();
            aircraftEquipmentTimeVM.ForEach(item => {
                item.CreatedBy = createdBy;
                response = _aircraftEquipementTimeService.Create(item); 
            });
            return Ok(response);
        }
        #endregion
    }
}
