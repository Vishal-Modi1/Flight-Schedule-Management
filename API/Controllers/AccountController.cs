using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using ViewModels.VM;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;
        private readonly JWTTokenGenerator _jWTTokenGenerator;
        private readonly RandomTextGenerator _randomTextGenerator;

        public AccountController(IAccountService accountService, IUserService userService,
            ISendMailService sendMailService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _userService = userService;
            _sendMailService = sendMailService;
            _jWTTokenGenerator = new JWTTokenGenerator(httpContextAccessor.HttpContext);
            _randomTextGenerator = new RandomTextGenerator();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            loginVM.Password = loginVM.Password.Encrypt();
            var dec = "VGVzdDE=".Decrypt();
            CurrentResponse response = _accountService.GetValidUser(loginVM);//try now 

            UserVM user = JsonConvert.DeserializeObject<UserVM>(response.Data);
            if (user != null)
            {
                List<string> getRoles(int RoleId)
                {
                    List<string> roles = new List<string>();
                    if (RoleId == 1)
                    {
                        roles.Add("Admin");
                    }
                    if (RoleId == 2)
                    {
                        roles.Add("OfficeStaff");
                    }
                    if (RoleId == 3)
                    {
                        roles.Add("Instructors");
                    }
                    if (RoleId == 4)
                    {
                        roles.Add("Rentors");
                    }
                    if (RoleId == 5)
                    {
                        roles.Add("Students");
                    }
                    if (RoleId == 6)
                    {
                        roles.Add("ReadOnly");
                    }

                    return roles;
                }

                string accessToken = _jWTTokenGenerator.Generate(user.Id, user.Email, getRoles(user.RoleId));

                LoginResponseVM loginResponseVM = new LoginResponseVM();

                response.Data = JsonConvert.SerializeObject(new LoginResponseVM
                {
                    AccessToken = accessToken,
                    CompanyName = user.CompanyName,
                    DateofBirth = user.DateofBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    RoleId = user.RoleId,
                    Id = user.Id
                });

                return Ok(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("forgotpassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            CurrentResponse response = new CurrentResponse();

            if (ModelState.IsValid)
            {
                string tokenString = _randomTextGenerator.Generate();
                string token = tokenString.Encrypt().Replace("=", "-");

                if (!string.IsNullOrEmpty(token))
                {
                    var url = forgotPasswordVM.ResetURL + token;
                    response = _sendMailService.PasswordReset(forgotPasswordVM.Email, url, token);
                    return Ok(response);
                }
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("resetpassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            CurrentResponse response = new CurrentResponse();

            if (ModelState.IsValid)
            {
                model.Token = model.Token;
                model.Password = model.Password.Encrypt();
                if (!string.IsNullOrEmpty(model.Token))
                {
                    response = _userService.ResetPassword(model);//try now 
                    return Ok(response);
                }
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("validatetoken")]
        [AllowAnonymous]
        public IActionResult ValidateToken(string token)
        {
            CurrentResponse response = new CurrentResponse();

            response = _accountService.IsValidToken(token);

            return Ok(response);
        }

        [HttpGet]
        [Route("activateaccount")]
        [AllowAnonymous]
        public IActionResult ActivateAccount(string token)
        {
            CurrentResponse response = new CurrentResponse();

            response = _accountService.ActivateAccount(token);

            return Ok(response);
        }
    }
}
