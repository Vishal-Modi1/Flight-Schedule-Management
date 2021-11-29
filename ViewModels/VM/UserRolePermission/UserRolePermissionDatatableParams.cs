using ViewModels.VM.Common;

namespace ViewModels.VM.UserRolePermission
{
    public class UserRolePermissionDatatableParams : DatatableParams
    {
        public int ModuleId { get; set; }

        public int RoleId { get; set; }
    }
}
