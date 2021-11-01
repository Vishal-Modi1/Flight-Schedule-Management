using DataModels.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModels.VM;

namespace PresentationLayer.Controllers
{
    public class AirCraftController : Controller
    {
        private readonly HttpCaller _httpCaller;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AirCraftController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateAsync()
        {
            AirCraftVM airCraftVM = await GetDetailsAsync(0);

            return PartialView(airCraftVM);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            AirCraftVM airCraftVM = await GetDetailsAsync(id);

            return PartialView("Create", airCraftVM);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string url = $"aircraft/delete?id={id}";
            CurrentResponse response = await _httpCaller.GetAsync(url);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AirCraftVM airCraftVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(airCraftVM);
            }

            string url = "aircraft/create";

            if (airCraftVM.Id > 0)
            {
                url = "aircraft/edit";
            }

            string jsonData = JsonConvert.SerializeObject(airCraftVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            AirCraftVM aircraft = JsonConvert.DeserializeObject<AirCraftVM>(response.Data);

            TempData["id"] = aircraft.Id;

            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> ListAsync(AircraftFilterVM aircraftFilterVM)
        {
            //IQueryCollection query = HttpContext.Request.Query;

       //     DatatableParams datatableParams = new DatatableParams();

            //var draw = query["draw"].FirstOrDefault();
            //datatableParams.Start = Convert.ToInt32(query["start"]);
            //datatableParams.Length = Convert.ToInt32(query["length"]);
            //datatableParams.SearchText = query["search[value]"];

            //datatableParams.SortOrderColumn = "Name";

            //datatableParams.OrderType = query["order[0][dir]"].ToString();

            string url = "aircraft/list";

            string jsonData = JsonConvert.SerializeObject(aircraftFilterVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<AirCraftVM> aircraftList = JsonConvert.DeserializeObject<List<AirCraftVM>>(response.Data);

            //int recordsTotal = aircraftList.Count() > 0 ? aircraftList[0].TotalRecords : 0;

            //return Json(new
            //{
            //    draw = draw,
            //    recordsFiltered = recordsTotal,
            //    recordsTotal = recordsTotal,
            //    data = aircraftList
            //});

            return PartialView("list", aircraftList);

        }

        private async Task<AirCraftVM> GetDetailsAsync(int id)
        {
            var response = await _httpCaller.GetAsync($"aircraft/getDetails?id={id}");

            AirCraftVM airCraftVM = new AirCraftVM();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                airCraftVM = JsonConvert.DeserializeObject<AirCraftVM>(response.Data);
            }

            return airCraftVM;
        }

        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            byte[] data;

            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);

            MultipartFormDataContent multiContent = new MultipartFormDataContent
            {
                { bytes, "file", TempData["id"].ToString() }
            };

            CurrentResponse response = await _httpCaller.PostFileAsync($"aircraft/uploadfile", multiContent);

            return Ok(response);
        }

        #region Aircraft Make

        public async Task<IActionResult> ListMakeAsync()
        {
            CurrentResponse response = await _httpCaller.GetAsync($"aircraft/makelist");

            List<AircraftMake> aircraftMakeList = JsonConvert.DeserializeObject<List<AircraftMake>>(response.Data);

            return PartialView("_aircraftMakeDropdown", aircraftMakeList);
        }

        public IActionResult CreateMake()
        {
            return PartialView("createmake");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMakeAsync(AircraftMake aircraftMake)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(aircraftMake);
            }

            string url = "aircraft/createmake";

            string jsonData = JsonConvert.SerializeObject(aircraftMake);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            aircraftMake = JsonConvert.DeserializeObject<AircraftMake>(response.Data);

            return Json(response);
        }

        #endregion

        #region Aircraft Model

        public async Task<IActionResult> ListModelAsync()
        {
            CurrentResponse response = await _httpCaller.GetAsync($"aircraft/modellist");

            List<AircraftModel> aircraftModelList = JsonConvert.DeserializeObject<List<AircraftModel>>(response.Data);

            return PartialView("_aircraftModelDropdown", aircraftModelList);
        }

        public IActionResult CreateModel()
        {
            return PartialView("createmodel");
        }

        [HttpPost]
        public async Task<IActionResult> CreateModelAsync(AircraftModel aircraftModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(aircraftModel);
            }

            string url = "aircraft/createmodel";

            string jsonData = JsonConvert.SerializeObject(aircraftModel);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            aircraftModel = JsonConvert.DeserializeObject<AircraftModel>(response.Data);

            return Json(response);
        }

        #endregion

        #region AirCraftEquipmentTime
        public async Task<IActionResult> AircraftEquipmentTimesListFormAsync()
        {
            //CurrentResponse response = await _httpCaller.GetAsync($"aircraft/modellist");

            //List<AircraftModel> aircraftModelList = JsonConvert.DeserializeObject<List<AircraftModel>>(response.Data);

            return PartialView("_aircraftEquipmentTimesListForm", new {  });
        }

        #endregion
    }
}
