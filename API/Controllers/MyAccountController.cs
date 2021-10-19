using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAccountController : ControllerBase
    {
        private readonly IMyAccountService _myAccountService;

        public MyAccountController(IMyAccountService myAccountService)
        {
            _myAccountService = myAccountService;
        }

        [HttpPost]
        [Route("changepassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            changePasswordVM.OldPassword = changePasswordVM.OldPassword.Encrypt();
            changePasswordVM.NewPassword = changePasswordVM.NewPassword.Encrypt();

            CurrentResponse response = _myAccountService.ChangePassword(changePasswordVM);

            return Ok(response);
        }
    }
}
