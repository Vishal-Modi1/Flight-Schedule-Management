using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Filters;
using PresentationLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.VM.Common;
using DataModels.VM.Company;

namespace PresentationLayer.Controllers
{
    [TypeFilter(typeof(CustomAuthorization))]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public CompanyController(IHttpContextAccessor httpContextAccessor)
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
            CompanyVM companyVM = await GetDetailsAsync(id);
            return PartialView("Create", companyVM);
        }

        private async Task<CompanyVM> GetDetailsAsync(int id)
        {
            var response = await _httpCaller.GetAsync($"company/getDetails?id={id}");

            CompanyVM companyVM = new CompanyVM();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                companyVM = JsonConvert.DeserializeObject<CompanyVM>(response.Data);
            }

            return companyVM;
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string url = $"company/delete?id={id}";
            CurrentResponse response = await _httpCaller.GetAsync(url);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompanyVM companyVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(companyVM);
            }

            string url = "company/create";

            if (companyVM.Id > 0)
            {
                url = "company/edit";
            }

            string jsonData = JsonConvert.SerializeObject(companyVM);
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

            string url = "company/list";

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<CompanyVM> companiesList = JsonConvert.DeserializeObject<List<CompanyVM>>(response.Data);

            int recordsTotal = companiesList.Count() > 0 ? companiesList[0].TotalRecords : 0;

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = companiesList
            });

        }
    }
}
