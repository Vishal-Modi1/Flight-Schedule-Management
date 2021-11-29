using ViewModels.VM.UserRole;
using System.Collections.Generic;

namespace ViewModels.VM.UserRolePermission
{
    public class UserRolePermissionFilterVM
    {
        public List<ModuleDetailsVM> ModuleList { get; set; }
        public List<UserRoleVM> UserRoleList{ get; set; }
        public int ModuleId { get; set; }
        public int UserRoleId { get; set; }
    }
}
