using FlightScheduleManagement.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.VM;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json;

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

        private async Task<UserVM> GetDetailsAsync(int id)
        {
            var response = await _httpCaller.GetAsync($"user/getDetails?id={id}");

            UserVM userVM = new UserVM();

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<CurrentResponse>(jsonContent);
                userVM = JsonConvert.DeserializeObject<UserVM>(data.Data.ToString());
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
            HttpResponseMessage httpResponseMessage = await _httpCaller.PostAsync(url, jsonData);
            var data = JsonConvert.DeserializeObject<CurrentResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

            if(data.Status == System.Net.HttpStatusCode.OK)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }


            userVM = await GetDetailsAsync(0);

            return PartialView(userVM);
        }

        [HttpGet]
        public async Task<string> IsEmailExistAsync(string email)
        {
            HttpResponseMessage httpResponseMessage = await _httpCaller.GetAsync($"user/isemailexist?email={email}");
            string jsonContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            CurrentResponse response = new JavaScriptSerializer().Deserialize<CurrentResponse>(jsonContent);

            return response.Data;
        }
    }
}