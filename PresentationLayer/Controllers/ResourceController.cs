using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Utilities;
using System.Collections.Generic;
using System.Linq;
using DataModels.VM.Common;
using DataModels.VM.UserRolePermission;
using DataModels.Enums;
using DataModels.Constants;

namespace PresentationLayer.Controllers
{
    public class ResourceController : Controller
    {
        private readonly HttpCaller _httpCaller;

        public ResourceController(IHttpContextAccessor httpContextAccessor)
        {
            _httpCaller = new HttpCaller(httpContextAccessor.HttpContext);
        }


        public IActionResult GetMenuItems()
        {
            List<UserRolePermissionDataVM> userRolePermissionsList = CurrentUserPermissionManager.GetAsync().Result;

            if (userRolePermissionsList == null || userRolePermissionsList.Count() == 0)
                return PartialView("AdminPortal/_MainNavigation", new List<MenuItem>());

            userRolePermissionsList = userRolePermissionsList.Where(p => p.IsAllowed == true && p.PermissionType == PermissionType.View.ToString()).ToList();

            List<MenuItem> menuItemsList = new List<MenuItem>();


            foreach (UserRolePermissionDataVM userRolePermission in userRolePermissionsList)
            {
                MenuItem menuItem = new MenuItem();

                menuItem.Action = userRolePermission.ActionName;
                menuItem.Controller = userRolePermission.ControllerName;
                menuItem.DisplayName = userRolePermission.DisplayName;
                menuItem.FavIconStyle = userRolePermission.Icon;

                menuItemsList.Add(menuItem);
            }

            return PartialView("AdminPortal/_MainNavigation", menuItemsList);
        }
    }
}
