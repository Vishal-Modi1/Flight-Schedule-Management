using DataModels.Models;
using System.Collections.Generic;
using ViewModels.VM;
using ViewModels.VM.UserRolePermission;


namespace Repository.Interface
{
    public interface IUserRolePermissionRepository
    {
        List<UserRolePermissionDataVM> GetByRoleId(int roleId);

        UserRolePermission Update(UserRolePermissionDataVM  userRolePermissionVM);

        List<UserRolePermissionDataVM> List(UserRolePermissionDatatableParams datatableParams);

        void UpdatePermission(int id, bool isAllow);

        void UpdateFullPermission(int id, bool isAllow);
    }
}
