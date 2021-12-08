using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Utilities;
using DataModels.VM.MyAccount;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using DataModels.VM.Common;
using DataModels.Constants;

namespace PresentationLayer.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public MyAccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ChangePasswordVM changePasswordVM = new ChangePasswordVM();
            changePasswordVM.UserId = Convert.ToInt32(_httpCaller.GetClaimValue(CustomClaimTypes.UserId));

            return View(changePasswordVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordVM changePasswordVM)
        {
            string url = "myaccount/changepassword";
            string jsonData = JsonConvert.SerializeObject(changePasswordVM);
            CurrentResponse response = await _httpCaller.PostAsync(url, jsonData);

            return Json(response);
        }
    }
}
