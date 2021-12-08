using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DataModels.VM.Common;

namespace PresentationLayer.Utilities
{
    public class DropDownListGenerator
    {

        public List<SelectListItem> Generate<T>(List<T> list, string optionalText) where T : DropDownListValues
        {
            List<SelectListItem> userRoleList = new List<SelectListItem>();

            userRoleList.Add(new SelectListItem { Text = optionalText, Value = "" });

            userRoleList.AddRange(list.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            }));

            return userRoleList;
        }
    }
}
