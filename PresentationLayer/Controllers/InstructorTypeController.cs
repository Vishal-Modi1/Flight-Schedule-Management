using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.VM;

namespace PresentationLayer.Controllers
{
    public class InstructorTypeController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public InstructorTypeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            InstructorTypeVM instructorTypeVM = await GetDetailsAsync(id);
            return PartialView("Create", instructorTypeVM);
        }

        private async Task<InstructorTypeVM> GetDetailsAsync(int id)
        {
            var response = await _httpCaller.GetAsync($"instructortype/getDetails?id={id}");

            InstructorTypeVM instructorTypeVM = new InstructorTypeVM();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                instructorTypeVM = JsonConvert.DeserializeObject<InstructorTypeVM>(response.Data);
            }

            return instructorTypeVM;
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string url = $"instructortype/delete?id={id}";
            CurrentResponse response = await _httpCaller.GetAsync(url);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(InstructorTypeVM instructorTypeVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(instructorTypeVM);
            }

            string url = "instructortype/create";

            if (instructorTypeVM.Id > 0)
            {
                url = "instructortype/edit";
            }

            string jsonData = JsonConvert.SerializeObject(instructorTypeVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            return Json(response);
        }

        [HttpGet]
        public async Task<ActionResult> ListAsync()
        {
            IQueryCollection query = HttpContext.Request.Query;

            DatatableParams datatableParams = new DatatableParams();

            var draw = query["draw"].FirstOrDefault();
            datatableParams.Start = Convert.ToInt32(query["start"]);
            datatableParams.Length = Convert.ToInt32(query["length"]);
            datatableParams.SearchText = query["search[value]"];

            datatableParams.SortOrderColumn = "Name";

            datatableParams.OrderType = query["order[0][dir]"].ToString();

            string url = "instructortype/list";

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<InstructorTypeVM> instructorTypeList = JsonConvert.DeserializeObject<List<InstructorTypeVM>>(response.Data);

            int recordsTotal = instructorTypeList.Count() > 0 ? instructorTypeList[0].TotalRecords : 0;

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = instructorTypeList
            });

        }
    }
}
