using FlightScheduleManagement.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.VM;
using System.Web.Script.Serialization;

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
            UserVM userVM = await GetDetailsAsync();
            return PartialView(userVM);
        }

        private async Task<UserVM> GetDetailsAsync()
        {
            var response = await _httpCaller.GetAsync("user/getDetails");

            UserVM userVM = new UserVM();

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;
                var data = new JavaScriptSerializer().Deserialize<CurrentResponse>(jsonContent);

                userVM = new JavaScriptSerializer().Deserialize<UserVM>(data.Data.ToString());

            }

            return userVM;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                userVM = await GetDetailsAsync();
                return PartialView(userVM); 

            }

            string jsonData = new JavaScriptSerializer().Serialize(userVM);
            var data = await _httpCaller.PostAsync("user/create", jsonData);
            return PartialView(userVM);
        }
    }
}