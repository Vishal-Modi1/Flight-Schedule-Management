using ViewModels.VM;

namespace Service.Interface
{
    public interface IUserRolePermissionService
    {
        CurrentResponse GetByRoleId(int roleId);

        CurrentResponse List(DatatableParams datatableParams);

        CurrentResponse UpdateCreatePermission(int id, bool isAllow);

        CurrentResponse UpdateEditPermission(int id, bool isAllow);

        CurrentResponse UpdateViewPermission(int id, bool isAllow);

        CurrentResponse UpdateDeletePermission(int id, bool isAllow);

        CurrentResponse UpdateFullPermission(int id, bool isAllow);
    }
}
