using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using DataModels.VM.Common;
using DataModels.VM.UserRolePermission;
using System;
using DataModels.Constants;
using Microsoft.AspNetCore.Http;
using API.Utilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRolePermissionController : ControllerBase
    {
        private readonly IUserRolePermissionService _userRolePermissionService;
        private readonly JWTTokenGenerator _jWTTokenGenerator;

        public UserRolePermissionController(IUserRolePermissionService userRolePermissionService, IHttpContextAccessor httpContextAccessor)
        {
            _userRolePermissionService = userRolePermissionService;
            _jWTTokenGenerator = new JWTTokenGenerator(httpContextAccessor.HttpContext);
        }


        [HttpPost]
        [Route("list")]
        public IActionResult List(UserRolePermissionDatatableParams datatableParams)
        {
            if (datatableParams.CompanyId == 0)
            {
                string companyId = _jWTTokenGenerator.GetClaimValue(CustomClaimTypes.CompanyId);
                datatableParams.CompanyId = companyId == "" ? 0 : Convert.ToInt32(companyId);
            }

            CurrentResponse response = _userRolePermissionService.List(datatableParams);

            return Ok(response);
        }

        [HttpGet]
        [Route("listbyroleid")]
        public IActionResult ListByRoleId()
        {

            string companyIdClaim = _jWTTokenGenerator.GetClaimValue(CustomClaimTypes.CompanyId);
            string roleIdClaim = _jWTTokenGenerator.GetClaimValue(CustomClaimTypes.CompanyId);
            int companyId = companyIdClaim == "" ? 0 : Convert.ToInt32(companyIdClaim);
            int roleId = roleIdClaim == "" ? 0 : Convert.ToInt32(roleIdClaim);

            CurrentResponse response = _userRolePermissionService.GetByRoleId(roleId, companyId);

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
