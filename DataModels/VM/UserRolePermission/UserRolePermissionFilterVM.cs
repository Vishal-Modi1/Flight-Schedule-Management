using DataModels.VM.UserRole;
using System.Collections.Generic;
using DataModels.VM.Company;

namespace DataModels.VM.UserRolePermission
{
    public class UserRolePermissionFilterVM : CompanyFilterVM
    {
        public List<ModuleDetailsVM> ModuleList { get; set; }
        public List<UserRoleVM> UserRoleList{ get; set; }
        public int ModuleId { get; set; }
        public int UserRoleId { get; set; }
    }
}
