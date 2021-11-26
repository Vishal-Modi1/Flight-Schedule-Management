using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ViewModels.VM;
using DataModels.Enums;

namespace PresentationLayer.Utilities
{
    public class PermissionManager
    {
        private static IHttpContextAccessor _accessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        private static HttpContext _httpContext => _accessor.HttpContext;

        public static List<UserRolePermissionVM> Get()
        {

            ClaimsPrincipal cp = _httpContext.User;

            string claimValue = cp.Claims.Where(c => c.Type == "Permissions")
                               .Select(c => c.Value).SingleOrDefault();


            List<UserRolePermissionVM> userRolePermissionsList = JsonConvert.DeserializeObject<List<UserRolePermissionVM>>(claimValue);

            return userRolePermissionsList;

        }

        public static bool IsAllowed(PermissionType permissionType, string moduleName)
        {
            List<UserRolePermissionVM> userRolePermissionsList = Get();

            bool isAllowed = userRolePermissionsList.Where(p => p.IsAllowed == true &&
                              p.ModuleName.ToLower() == moduleName && p.PermissionType == permissionType.ToString()).Count() > 0;

            return isAllowed;
        }
    }
}
