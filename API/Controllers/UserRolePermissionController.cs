using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRolePermissionController : ControllerBase
    {
        private readonly IUserRolePermissionService _userRolePermissionService;

        public UserRolePermissionController(IUserRolePermissionService userRolePermissionService)
        {
            _userRolePermissionService = userRolePermissionService;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult List(DatatableParams datatableParams)
        {
            CurrentResponse response = _userRolePermissionService.List(datatableParams);

            return Ok(response);
        }


        [HttpGet]
        [Route("updatecreatepermission")]
        public IActionResult UpdateCreatePermission(int id, bool isAllow)
        {
            CurrentResponse response = _userRolePermissionService.UpdateCreatePermission(id, isAllow);

            return Ok(response);
        }

        [HttpGet]
        [Route("updateeditepermission")]
        public IActionResult UpdateEditPermission(int id, bool isAllow)
        {
            CurrentResponse response = _userRolePermissionService.UpdateEditPermission(id, isAllow);

            return Ok(response);
        }

        [HttpGet]
        [Route("updateviewpermission")]
        public IActionResult UpdateViewPermission(int id, bool isAllow)
        {
            CurrentResponse response = _userRolePermissionService.UpdateViewPermission(id, isAllow);

            return Ok(response);
        }

        [HttpGet]
        [Route("updatedeletepermission")]
        public IActionResult UpdateDeletePermission(int id, bool isAllow)
        {
            CurrentResponse response = _userRolePermissionService.UpdateDeletePermission(id, isAllow);

            return Ok(response);
        }

    }
}
