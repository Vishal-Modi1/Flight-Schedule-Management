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
using ViewModels.VM;

namespace PresentationLayer.Controllers
{
    [TypeFilter(typeof(CustomAuthorization))]
    [Authorize]
    public class UserRolePermissionController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public UserRolePermissionController(IHttpContextAccessor httpContextAccessor)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListAsync()
        {
            IQueryCollection query = HttpContext.Request.Query;
            
            DatatableParams datatableParams = new DatatableParams();

            var draw = query["draw"].FirstOrDefault();
            datatableParams.Start = Convert.ToInt32(query["start"]);
            datatableParams.Length = Convert.ToInt32(query["length"]);
            datatableParams.SearchText = query["search[value]"];

            int sortColumn = Convert.ToInt32(query["order[0][column]"]);

            if (sortColumn == 0)
            {
                datatableParams.SortOrderColumn = "RoleName";
            }
            else if (sortColumn == 1)
            {
                datatableParams.SortOrderColumn = "ModuleName";
            }
            
            datatableParams.OrderType = query["order[0][dir]"].ToString();

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync($"UserRolePermission/list", jsonData);
            List<UserRolePermissionVM> userRolePermissionList = JsonConvert.DeserializeObject<List<UserRolePermissionVM>>(response.Data);
            int recordsTotal = userRolePermissionList.Count() > 0 ? userRolePermissionList[0].TotalRecords : 0;

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = userRolePermissionList
            });
        }

        [HttpGet]
        public async Task<ActionResult> UpdateCreatePermissionAsync(int id, bool isAllow)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"userrolepermission/updatecreatepermission?id={id}&isAllow={isAllow}");

            return Json(response);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateEditPermissionAsync(int id, bool isAllow)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"userrolepermission/updateeditepermission?id={id}&isAllow={isAllow}");

            return Json(response);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateViewPermissionAsync(int id, bool isAllow)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"userrolepermission/updateviewpermission?id={id}&isAllow={isAllow}");

            return Json(response);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateDeletePermissionAsync(int id, bool isAllow)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"userrolepermission/updatedeletepermission?id={id}&isAllow={isAllow}");

            return Json(response);
        }

    }
}
