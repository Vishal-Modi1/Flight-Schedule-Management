using DataModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Filters;
using PresentationLayer.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataModels.VM.Aircraft;
using DataModels.VM.AircraftEquipment;
using DataModels.VM.Common;

namespace PresentationLayer.Controllers
{
    [TypeFilter(typeof(CustomAuthorization))]
    [Authorize]
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

        public async Task<IActionResult> UpdateAircraftAsync(int aircraftAsync = 0)
        {
            AirCraftVM airCraftVM = await GetDetailsAsync(aircraftAsync);

            return PartialView("Create",airCraftVM);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            AirCraftVM airCraftVM = await GetDetailsAsync(id);
            airCraftVM.AirCraftEquipmentList = new List<AirCraftEquipment>();

            return View("Edit", airCraftVM);
        }

        public async Task<IActionResult> EditPartialAsync(int id)
        {
            AirCraftVM airCraftVM = await GetDetailsAsync(id);
            airCraftVM.AirCraftEquipmentList = new List<AirCraftEquipment>();
            airCraftVM.AirCraftEquipmentList = await GetAirCraftEquipmentById(id);

            return PartialView("_edit", airCraftVM);
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

            //Need to confirm this logic with client
            if (aircraft.Id > 0)
            {
                string equipment_url = "aircraft/createaircraftequipment";
                airCraftVM.AircraftEquipmentTimesList.ForEach(z => z.AircraftId = aircraft.Id);

                string equipment_jsonData = JsonConvert.SerializeObject(airCraftVM.AircraftEquipmentTimesList);
                CurrentResponse equipment_response = await _httpCaller.PostAsync(equipment_url, equipment_jsonData);
            }

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
        private async Task<List<StatusVM>> GetStatusListAsync()
        {
            var response = await _httpCaller.GetAsync($"equipmentstatus/list");
            List<StatusVM> statusVMList = new List<StatusVM>();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                statusVMList = JsonConvert.DeserializeObject<List<StatusVM>>(response.Data);
            }
            return statusVMList;
        }
        private async Task<List<EquipmentClassificationVM>> GetClassificationListAsync()
        {
            var response = await _httpCaller.GetAsync($"equipmentclassification/list");
            List<EquipmentClassificationVM> ClassificationVMList = new List<EquipmentClassificationVM>();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                ClassificationVMList = JsonConvert.DeserializeObject<List<EquipmentClassificationVM>>(response.Data);
            }
            return ClassificationVMList;
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

        [HttpPost]
        public async Task<IActionResult> IsAircraftExistAsync(AirCraftVM airCraftVM)
        {
            string url = "aircraft/isaircraftexist";

            string jsonData = JsonConvert.SerializeObject(airCraftVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            return Json(response);
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
        public IActionResult AircraftEquipmentTimesListForm(int noOfEngines, int noOfPropellers)
        {
            AirCraftVM airCraftVM = new AirCraftVM();
            airCraftVM.NoofEngines = noOfEngines;
            airCraftVM.NoofPropellers = noOfPropellers;
            return PartialView("_aircraftEquipmentTimesListForm", airCraftVM);
        }

        #endregion

        #region AirCraftEquipment

        [HttpGet]
        public async Task<IActionResult> AddUpdateEquipment(int id = 0, int aircraftId = 0, string actionbtn = "view")
        {
            AirCraftEquipmentsVM airCraftEquipmentsVM = await GetEquipmentDetailsAsync(id);
            airCraftEquipmentsVM.AirCraftId = aircraftId;

            return PartialView("_aircraftAddEquipment", Tuple.Create(airCraftEquipmentsVM, actionbtn));
        }

        public async Task<IActionResult> ViewEquipmentDetails(int id)
        {
            AirCraftEquipmentsVM airCraftEquipmentsVM = await GetEquipmentDetailsAsync(id);

            return View("_viewEquipmentDetails", Tuple.Create(airCraftEquipmentsVM, "view"));
        }

        private async Task<AirCraftEquipmentsVM> GetEquipmentDetailsAsync(int id)
        {
            AirCraftEquipmentsVM airCraftEquipmentsVM = new AirCraftEquipmentsVM();
            airCraftEquipmentsVM.Id = id;
            airCraftEquipmentsVM.statusList = new List<StatusVM>();
            airCraftEquipmentsVM.classificationList = new List<EquipmentClassificationVM>();

            if (id > 0)
            {
                airCraftEquipmentsVM = await GetAirCraftEquipmentAsync(id);
            }

            airCraftEquipmentsVM.statusList = await GetStatusListAsync();
            airCraftEquipmentsVM.classificationList = await GetClassificationListAsync();

            return airCraftEquipmentsVM;
        }

        [HttpGet]
        public async Task<IActionResult> AirCraftEquipmentListing(int id = 0)
        {
            List<AirCraftEquipment> airCraftEquipment = new List<AirCraftEquipment>();
            airCraftEquipment = await GetAirCraftEquipmentById(id);
            return PartialView("_airCraftEquipmentListing", airCraftEquipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUpdateEquipmentAsync(AirCraftEquipmentsVM airCraftEquipmentsVM)
        {
            if (ModelState.IsValid)
            {
                string url;
                if (airCraftEquipmentsVM.Id > 0)
                {
                    url = "aircraftequipment/edit";
                }
                else {
                    url = "aircraftequipment/create";
                }

                string jsonData = JsonConvert.SerializeObject(airCraftEquipmentsVM);
                CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    return Json(response);
                }

            }

            airCraftEquipmentsVM.statusList = new List<StatusVM>();
            airCraftEquipmentsVM.classificationList = new List<EquipmentClassificationVM>();
            airCraftEquipmentsVM.statusList = await GetStatusListAsync();
            airCraftEquipmentsVM.classificationList = await GetClassificationListAsync();
            
            return RedirectToAction("Edit", airCraftEquipmentsVM.Id);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEquipmentAsync(int airCraftEquipmentid)
        {
            string url;
            url = "aircraftequipment/delete?id=" + airCraftEquipmentid;

                CurrentResponse response = await _httpCaller.DeleteAsync(url);
                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    return Json(response);
                }
            return Json(response);
        }
        #endregion

        private async Task<List<AirCraftEquipment>> GetAirCraftEquipmentById(int id)
        {
            string url = "aircraftequipment/list?airCraftId=" + Convert.ToString(id);
            CurrentResponse response = await _httpCaller.GetAsync(url);
            return JsonConvert.DeserializeObject<List<AirCraftEquipment>>(response.Data);
        }

        // With server side pagination
        [HttpGet]
        public async Task<ActionResult> ListAirCraftEquipmentAsync()
        {
            IQueryCollection query = HttpContext.Request.Query;

            AircraftEquipmentDatatableParams datatableParams = new AircraftEquipmentDatatableParams();

            var draw = query["draw"].FirstOrDefault();
            datatableParams.Start = Convert.ToInt32(query["start"]);
            datatableParams.Length = Convert.ToInt32(query["length"]);
            datatableParams.SearchText = query["search[value]"];

            int sortColumn = Convert.ToInt32(query["order[0][column]"]);

            if (sortColumn == 0)
            {
                datatableParams.SortOrderColumn = "Status";
            }
            else if (sortColumn == 1)
            {
                datatableParams.SortOrderColumn = "Item";
            }
            else if (sortColumn == 2)
            {
                datatableParams.SortOrderColumn = "Classification";
            }
            else if (sortColumn == 3)
            {
                datatableParams.SortOrderColumn = "Make";
            }
            else if (sortColumn == 4)
            {
                datatableParams.SortOrderColumn = "Model";
            }

            datatableParams.OrderType = query["order[0][dir]"].ToString();
            datatableParams.AircraftId = Convert.ToInt32(query["aircraftid"].ToString());

            string url = "aircraftequipment/list";

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<AircraftEquipmentDataVM> aircraftEquipmentDataList = JsonConvert.DeserializeObject<List<AircraftEquipmentDataVM>>(response.Data);

            int recordsTotal = aircraftEquipmentDataList.Count() > 0 ? aircraftEquipmentDataList[0].TotalRecords : 0;

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = aircraftEquipmentDataList
            });
        }

        private async Task<AirCraftEquipmentsVM> GetAirCraftEquipmentAsync(int id)
        {
            string url = "aircraftequipment/fetchbyid?id=" + Convert.ToString(id);
            CurrentResponse response = await _httpCaller.GetAsync(url);
            return JsonConvert.DeserializeObject<AirCraftEquipmentsVM>(response.Data);
        }
    }
}
