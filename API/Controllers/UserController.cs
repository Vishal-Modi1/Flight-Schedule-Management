using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        [Route("Create")]
        public IActionResult Create(UserVM userVM)
        {
            CurrentResponse response = _userService.Create(userVM);

            if(response.Status == HttpStatusCode.OK)
                return Ok(response);
            else
                throw new RestException(response);
        }
    }
}
