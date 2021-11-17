using System;
using System.Collections.Generic;

namespace ViewModels.VM
{
    public class LoginResponseVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string CompanyName { get; set; }
        public string ExternalId { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string Gender { get; set; }
        public List<UserRolePermissionVM> UserPermissionList { get; set; }
    }
}
