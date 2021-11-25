using DataModels.Models;
using System.Collections.Generic;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IUserRolePermissionRepository
    {
        List<UserRolePermissionVM> GetByRoleId(int roleId);

        UserRolePermission Update(UserRolePermissionVM  userRolePermissionVM);

        List<UserRolePermissionVM> List(UserRolePermissionDatatableParams datatableParams);

        void UpdatePermission(int id, bool isAllow);

        void UpdateFullPermission(int id, bool isAllow);
    }
}
