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
using PresentationLayer.Utilities;

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

            if (actionName == "Index" && !PermissionManager.IsAllowed(PermissionType.View, controllerName.ToLower()))
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Create" && !PermissionManager.IsAllowed(PermissionType.Create, controllerName.ToLower()) && context.HttpContext.Request.Method == "GET" )
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Delete" && !PermissionManager.IsAllowed(PermissionType.Delete, controllerName.ToLower()))
            {
                SetErrorRoute(context);
                return;
            }

            if (actionName == "Edit" && !PermissionManager.IsAllowed(PermissionType.Edit, controllerName.ToLower()))
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
