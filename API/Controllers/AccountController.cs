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
        private readonly JWTTokenGenerator _jWTTokenGenerator;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            _jWTTokenGenerator = new JWTTokenGenerator();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            CurrentResponse response = _accountService.GetValidUser(loginVM);//try now 

            UserVM user = JsonConvert.DeserializeObject<UserVM>(response.Data);
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
    }
}
