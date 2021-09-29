using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Utilities;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                string jsonData = JsonConvert.SerializeObject(loginVM);
                var response = await _httpCaller.PostAsync("Account/login", jsonData);

                if (response.Status == System.Net.HttpStatusCode.OK)
                {
                    await AddCookieAsync(response.Data);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(loginVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
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
    }
}
