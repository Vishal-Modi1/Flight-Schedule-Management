using FlightScheduleManagement.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            var data = await _httpCaller.GetAsync("user/getDetails");

            return PartialView();
        }
    }
}