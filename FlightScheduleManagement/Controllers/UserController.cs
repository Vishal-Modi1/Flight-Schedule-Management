using FlightScheduleManagement.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.VM;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Collections.Generic;

namespace FlightScheduleManagement.Controllers
{
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
        public async Task<ActionResult> CreateAsync()
        {
            UserVM userVM = await GetDetailsAsync(0);
            return PartialView(userVM);
        }

        [HttpGet]
        public async Task<ActionResult> EditAsync(int id)
        {
            UserVM userVM = await GetDetailsAsync(id);
            return PartialView("CreateAsync", userVM);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string url = $"user/delete?id={id}";
            CurrentResponse response = await _httpCaller.GetAsync(url);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ListAsync()
        { 
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);

            DatatableParams datatableParams = new DatatableParams();

            datatableParams.SearchText = nvc["search[value]"];
            datatableParams.Start = Convert.ToInt32(nvc["start"]);
            datatableParams.Length = Convert.ToInt32(nvc["length"]);
            int sortColumn = Convert.ToInt32(nvc["order[0][column]"]);

            if(sortColumn == 0)
            {
                datatableParams.SortOrderColumn = "FirstName";
            }
            else if(sortColumn == 1)
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

            datatableParams.OrderType = nvc["order[0][dir]"].ToString();

            string url = "user/list"; 

            string jsonData = JsonConvert.SerializeObject(datatableParams);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);
            List<UserSearchList> userList = JsonConvert.DeserializeObject<List<UserSearchList>>(response.Data);

            int totalRecords = userList.Count() > 0 ? userList[0].TotalRecords : 0;
            return Json( new {  recordsFiltered = userList.Count() , recordsTotal = totalRecords, data = userList }, JsonRequestBehavior.AllowGet);

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
        public async Task<ActionResult> CreateAsync(UserVM userVM)
        {

            if(userVM.IsInstructor != null && !(bool) userVM.IsInstructor)
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

            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                return Json(response, JsonRequestBehavior.AllowGet);
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

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}