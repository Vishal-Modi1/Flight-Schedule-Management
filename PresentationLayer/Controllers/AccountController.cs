﻿using Microsoft.AspNetCore.Authentication;
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
            return RedirectToAction("Login", "Account");
        }

        private async Task AddCookieAsync(string response)
        {
            LoginResponseVM loginResponse = JsonConvert.DeserializeObject<LoginResponseVM>(response);

            var userClaims = new List<Claim>()
             {
                  new Claim(ClaimTypes.Name, loginResponse.FirstName),
                  new Claim(ClaimTypes.Email, loginResponse.Email),
                  new Claim("AcessToken", loginResponse.AccessToken),
                  new Claim("Id", loginResponse.Id.ToString()),
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
                    return RedirectToAction("Login", "Account");
                }

                ViewBag.response = response;
            }

            return View(forgotPasswordVM);
        }


        [HttpGet]
        public async Task<IActionResult> ResetPasswordAsync([FromQuery(Name = "Token")] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            ResetPasswordVM resetPasswordVM = new ResetPasswordVM();

            CurrentResponse response = await _httpCaller.GetAsync($"Account/validatetoken?token={token}");

            if (response != null && response.Status == System.Net.HttpStatusCode.OK && Convert.ToBoolean(response.Data) == false)
            {
                ViewBag.response = JsonConvert.SerializeObject(Convert.ToBoolean(response.Data));
            }
            else
            {
                resetPasswordVM.Token = token;
            }

            return View(resetPasswordVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                CurrentResponse response = await _httpCaller.GetAsync($"Account/validatetoken?token={resetPasswordVM.Token}");

                if (response != null && response.Status == System.Net.HttpStatusCode.OK)
                {
                    ViewBag.response = JsonConvert.SerializeObject(Convert.ToBoolean(response.Data));

                    if (response.Data == "false")
                    {
                        return View(resetPasswordVM);
                    }
                }

                string jsonData = JsonConvert.SerializeObject(resetPasswordVM);
                response = await _httpCaller.PostAsync("Account/resetpassword", jsonData);

                if (response != null && response.Status == System.Net.HttpStatusCode.OK && Convert.ToBoolean(response.Data) == true)
                {
                    TempData["response"] = JsonConvert.SerializeObject(response);
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(resetPasswordVM);
        }

        [HttpGet]
        public async Task<IActionResult> ActivateAccountAsync(string token)
        {
            CurrentResponse response = await _httpCaller.GetAsync($"account/activateaccount?token={token}");

            ViewBag.response = response.Data;

            return View();
        }
    }
}
