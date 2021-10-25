using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using ViewModels.VM;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;
        private readonly RandomPasswordGenerator _randomPasswordGenerator;
        private readonly RandomTextGenerator _randomTextGenerator;
        private readonly JWTTokenGenerator _jWTTokenGenerator;

        public UserController(IUserService userService, ISendMailService sendMailService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _sendMailService = sendMailService;
            _randomPasswordGenerator = new RandomPasswordGenerator();
            _randomTextGenerator = new RandomTextGenerator();
            _jWTTokenGenerator = new JWTTokenGenerator(httpContextAccessor.HttpContext);
        }

        [HttpGet]
        [Route("getDetails")]
        public IActionResult GetDetails(int id)
        {
            CurrentResponse response = _userService.GetDetails(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(UserVM userVM)
        {
            string password = _randomPasswordGenerator.Generate();

            userVM.CreatedBy = userVM.UpdatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));

            userVM.Password = password.Encrypt();
            CurrentResponse response = _userService.Create(userVM);

            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                string tokenString = _randomTextGenerator.Generate();
                string token = tokenString.Encrypt().Replace("=", "-");

                userVM.Password = password;

                _sendMailService.NewUserAccountActivation(userVM, token);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(UserVM userVM)
        {
            userVM.UpdatedBy = Convert.ToInt32(_jWTTokenGenerator.GetClaimValue("Id"));

            CurrentResponse response = _userService.Edit(userVM);
            return Ok(response);
        }

        [HttpGet]
        [Route("isemailexist")]
        public IActionResult IsEmailExist(string email)
        {
            CurrentResponse response = _userService.IsEmailExist(email);

            return Ok(response);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            CurrentResponse response = _userService.Delete(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("updatestatus")]
        public IActionResult UpdateStatus(int id, bool isActive)
        {
            CurrentResponse response = _userService.UpdateActiveStatus(id, isActive);

            return Ok(response);
        }

        [HttpPost]
        [Route("list")]
        public IActionResult List(DatatableParams datatableParams)
        {
            CurrentResponse response = _userService.List(datatableParams);

            return Ok(response);
        }
    }
}
