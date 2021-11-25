using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.VM;
using DataModels.Enums ;


namespace PresentationLayer.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.User.Claims.Where(c => c.Type == "Permissions")
                              .Select(c => c.Value).SingleOrDefault();


            context.Result = new UnauthorizedResult();

            return;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string userPermissionJson = context.HttpContext.User.Claims.Where(c => c.Type == "Permissions")
                              .Select(c => c.Value).SingleOrDefault();

            List<UserRolePermissionVM> userPermissionList = JsonConvert.DeserializeObject<List<UserRolePermissionVM>>(userPermissionJson);
            string controllerName = (string)context.RouteData.Values["Controller"];
            string actionName = (string)context.RouteData.Values["Action"];

            userPermissionList = userPermissionList.Where(p => p.ModuleName.ToLower() == controllerName.ToLower() 
                                && p.IsAllowed == true ).ToList();

            if (userPermissionList.Count() == 0)
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Index" && userPermissionList.Where(p=>p.PermissionType == PermissionType.View.ToString()).Count() == 0)
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Create" && userPermissionList.Where(p => p.PermissionType == PermissionType.Create.ToString()).Count() == 0)
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Delete" && userPermissionList.Where(p => p.PermissionType == PermissionType.Delete.ToString()).Count() == 0)
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Edit" && userPermissionList.Where(p => p.PermissionType == PermissionType.Edit.ToString()).Count() == 0)
            {
                SetErrorRoute(context);
                return;
            }

        }

        private void SetErrorRoute(AuthorizationFilterContext context)
        {
            context.Result = new RedirectToRouteResult
            (
            new RouteValueDictionary(new
            {
                action = "Index",
                controller = "Home"
            }));
        }
    }
}
