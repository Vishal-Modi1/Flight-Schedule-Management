using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interface;
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

        public AccountController(IAccountService accountService, IUserService userService, ISendMailService sendMailService)
        {
            _accountService = accountService;
            _userService = userService;
            _sendMailService = sendMailService;
            _jWTTokenGenerator = new JWTTokenGenerator();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            loginVM.Password = CipherCode.EncodeBase64(loginVM.Password);
            CurrentResponse response = _accountService.GetValidUser(loginVM);//try now 

            UserVM user = JsonConvert.DeserializeObject<UserVM>(response.Data);
            if (user != null) {
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

                var accessToken = _jWTTokenGenerator.Generate(user.Email, getRoles(user.RoleId));

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
                    RoleId = user.RoleId
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
                var encryptMail = CipherCode.EncodeBase64(forgotPasswordVM.Email).Replace("=","-");
                if (!string.IsNullOrEmpty(encryptMail))
                {
                    var url = forgotPasswordVM.ResetURL + encryptMail;
                    response = _sendMailService.PasswordReset(forgotPasswordVM.Email, url);
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
                model.Token = CipherCode.DecodeBase64(model.Token.Replace("-", "="));
                model.Password = CipherCode.EncodeBase64(model.Password);
                if (!string.IsNullOrEmpty(model.Token))
                {
                    response = _userService.ResetPassword(model);//try now 
                    return Ok(response);
                }
            }
            return Ok(response);
        }
    }
}
