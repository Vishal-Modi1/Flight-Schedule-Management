using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("getDetails")]
        public IActionResult GetDetails()
        {
            CurrentResponse response = _userService.GetDetails();

            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(UserVM userVM)
        {
            CurrentResponse response = _userService.Create(userVM);

            return Ok(response);

        }
    }
}
