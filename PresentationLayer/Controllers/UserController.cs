using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UserController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public UserController()
        {
            _httpCaller = new HttpCaller();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            UserVM userVM = await GetDetailsAsync(0);
            return PartialView(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            UserVM userVM = await GetDetailsAsync(id);
            return PartialView("Create", userVM);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string url = $"user/delete?id={id}";
            CurrentResponse response = await _httpCaller.GetAsync(url);

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
            
            int sortColumn = Convert.ToInt32(query["order[0][column]"]);

            if (sortColumn == 0)
            {
                datatableParams.SortOrderColumn = "FirstName";
            }
            else if (sortColumn == 1)
            {
                datatableParams.SortOrderColumn = "LastName";
            }
            else if (sortColumn == 2)
            {
                datatableParams.SortOrderColumn = "Email";
            }
            else if (sortColumn == 3)
            {
                datatableParams.SortOrderColumn = "UserRole";
            }
            else if (sortColumn == 4)
            {
                datatableParams.SortOrderColumn = "IsInstructor";
            }
            else if (sortColumn == 5)
            {
                datatableParams.SortOrderColumn = "IsActive";
            }

            datatableParams.OrderType = query["order[0][dir]"].ToString();

            string url = "user/list";

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<UserSearchList> userList = JsonConvert.DeserializeObject<List<UserSearchList>>(response.Data);

            int recordsTotal = userList.Count() > 0 ? userList[0].TotalRecords : 0;
            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = userList
            });

        }

        private async Task<UserVM> GetDetailsAsync(int id)
        {
            var response = await _httpCaller.GetAsync($"user/getDetails?id={id}");

            UserVM userVM = new UserVM();

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                userVM = JsonConvert.DeserializeObject<UserVM>(response.Data);
            }

            return userVM;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserVM userVM)
        {

            if (userVM.IsInstructor != null && !(bool)userVM.IsInstructor)
            {
                ModelState["InstructorTypeId"].Errors.Clear();
            }

            if (!ModelState.IsValid)
            {
                userVM = await GetDetailsAsync(0);
                return PartialView(userVM);
            }

            if (userVM.Id == 0)
            {
                string emailExist = await IsEmailExistAsync(userVM.Email);

                if (emailExist == "true")
                {
                    userVM = await GetDetailsAsync(0);
                    ModelState.AddModelError("Email", "Email Already Exist");
                    return PartialView(userVM);
                }
            }

            string url = "user/create";

            if (userVM.Id > 0)
            {
                url = "user/edit";
            }

            string jsonData = JsonConvert.SerializeObject(userVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                return Json(response);
            }

            userVM = await GetDetailsAsync(0);

            return PartialView(userVM);
        }

        [HttpGet]
        public async Task<string> IsEmailExistAsync(string email)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"user/isemailexist?email={email}");

            return response.Data;
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStatus(int id, bool isActive)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"user/updatestatus?id={id}&isActive={isActive}");

            return Json(response);
        }
    }
}