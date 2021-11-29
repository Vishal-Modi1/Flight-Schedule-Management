using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using ViewModels.VM.Common;
using ViewModels.VM.UserRolePermission;

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
        public IActionResult List(UserRolePermissionDatatableParams datatableParams)
        {
            CurrentResponse response = _userRolePermissionService.List(datatableParams);

            return Ok(response);
        }

        [HttpGet]
        [Route("getfilters")]
        public IActionResult GetFilters()
        {
            CurrentResponse response = _userRolePermissionService.GetFiltersValue();

            return Ok(response);
        }

        [HttpGet]
        [Route("updatepermission")]
        public IActionResult UpdateCreatePermission(int id, bool isAllow)
        {
            CurrentResponse response = _userRolePermissionService.UpdatePermission(id, isAllow);

            return Ok(response);
        }
    }
}
