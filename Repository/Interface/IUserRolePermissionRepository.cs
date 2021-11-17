using DataModels.Models;
using System.Collections.Generic;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IUserRolePermissionRepository
    {
        List<UserRolePermissionVM> GetByRoleId(int roleId);

        UserRolePermission Update(UserRolePermissionVM  userRolePermissionVM);

        List<UserRolePermissionVM> List(DatatableParams datatableParams);

        void UpdateCreatePermission(int id, bool isAllow);

        void UpdateEditPermission(int id, bool isAllow);

        void UpdateViewPermission(int id, bool isAllow);

        void UpdateDeletePermission(int id, bool isAllow);

        void UpdateFullPermission(int id, bool isAllow);
    }
}
