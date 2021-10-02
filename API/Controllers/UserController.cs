using API.Utilities;
using DataModels.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly string _fromMailId;
        public UserController(IUserService userService, ISendMailService sendMailService)
        {
            _userService = userService;
            _sendMailService = sendMailService;
            _randomPasswordGenerator = new RandomPasswordGenerator();
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
            userVM.Password = _randomPasswordGenerator.RandomPassword();
            //START Send Create Mail
            _sendMailService.SendCreateUserMail(userVM);
            //END Send Create Mail

            #region Encrypt password
            userVM.Password = CipherCode.EncodeBase64(userVM.Password);
            #endregion 
            CurrentResponse response = _userService.Create(userVM);
            
            return Ok(response);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(UserVM userVM)
        {
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
