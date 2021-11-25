using ViewModels.VM;

namespace Service.Interface
{
    public interface IUserRolePermissionService
    {
        CurrentResponse GetByRoleId(int roleId);

        CurrentResponse List(UserRolePermissionDatatableParams datatableParams);

        CurrentResponse UpdatePermission(int id, bool isAllow);

        CurrentResponse UpdateFullPermission(int id, bool isAllow);

        CurrentResponse GetFiltersValue();
    }
}
