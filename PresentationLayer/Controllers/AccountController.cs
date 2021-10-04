using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ViewModels.VM;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
        }

        // GET: Account
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            string callBackResponse = TempData.Count > 0 && TempData["response"] != null ? TempData["response"].ToString() : "";
            
            if (!string.IsNullOrEmpty(callBackResponse)) {
                ViewBag.response = JsonConvert.DeserializeObject<CurrentResponse>(callBackResponse);
            }

            string userName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();

            if(!string.IsNullOrWhiteSpace(userName))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                string jsonData = JsonConvert.SerializeObject(loginVM);
                CurrentResponse response = await _httpCaller.PostAsync("Account/login", jsonData);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    await AddCookieAsync(response.Data);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.response = response;
            }

            return View(loginVM);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "/Account");
        }

        private async Task AddCookieAsync(string response)
        {
            LoginResponseVM loginResponse = JsonConvert.DeserializeObject<LoginResponseVM>(response);

            var userClaims = new List<Claim>()
             {
                  new Claim(ClaimTypes.Name, loginResponse.FirstName),
                  new Claim(ClaimTypes.Email, loginResponse.Email),
                  new Claim("AcessToken", loginResponse.AccessToken),
             };

            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            Thread.CurrentPrincipal = userPrincipal;

            await HttpContext.SignInAsync(userPrincipal);
       
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordVM forgotPasswordVM)
        {
            if (ModelState.IsValid)
            {
                forgotPasswordVM.ResetURL = Request.Scheme + "://" + Request.Host + "/Account/ResetPassword?Token=";
                string jsonData = JsonConvert.SerializeObject(forgotPasswordVM);
                CurrentResponse response = await _httpCaller.PostAsync("Account/forgotpassword", jsonData);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    TempData["response"] = JsonConvert.SerializeObject(response);
                    return RedirectToAction("Login", "/Account");
                }
            }

            return View(forgotPasswordVM);
        }


        [HttpGet]
        public IActionResult ResetPassword([FromQuery(Name = "Token")] string Token)
        {
            if (string.IsNullOrEmpty(Token)) {
                return RedirectToAction("Login", "/Account");
            }
            ResetPasswordVM ResetPasswordVM = new ResetPasswordVM();
            ResetPasswordVM.Token = Token;
            return View(ResetPasswordVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                string jsonData = JsonConvert.SerializeObject(resetPasswordVM);
                var response = await _httpCaller.PostAsync("Account/resetpassword", jsonData);

                if (response != null && response.Status == System.Net.HttpStatusCode.OK && Convert.ToBoolean(response.Data) == true)
                {
                    TempData["response"] = JsonConvert.SerializeObject(response);
                    return RedirectToAction("Login", "/Account");
                }
            }

            return View(resetPasswordVM);
        }
    }
}
