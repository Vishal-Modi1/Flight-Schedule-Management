using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using ViewModels.VM.UserRolePermission;

namespace Repository
{
    public class UserRolePermissionRepository : IUserRolePermissionRepository
    {
        private MyContext _myContext;

        public List<UserRolePermissionDataVM> GetByRoleId(int roleId)
        {
            using (_myContext = new MyContext())
            {
                List<UserRolePermissionDataVM> userRolePermissionsList = (from userRolePermission in _myContext.UserRolePermissions
                                                                      join userRole in _myContext.UserRoles
                                                                      on userRolePermission.RoleId equals userRole.Id
                                                                      join permission in _myContext.Permissions
                                                                      on userRolePermission.PermissionId equals permission.Id
                                                                      join module in _myContext.ModuleDetails
                                                                      on userRolePermission.ModuleId equals module.Id
                                                                      where userRolePermission.RoleId == roleId
                                                                      select new UserRolePermissionDataVM()
                                                                      {
                                                                          Id = userRolePermission.Id,
                                                                          ModuleName = module.Name,
                                                                          RoleId = userRolePermission.RoleId,
                                                                          RoleName = userRole.Name,
                                                                          ActionName = module.ActionName,
                                                                          ControllerName = module.ControllerName,
                                                                          Icon = module.Icon,
                                                                          DisplayName = module.DisplayName,
                                                                          ModuleId = module.Id,
                                                                          PermissionId = permission.Id,
                                                                          PermissionType = permission.PermissionType,
                                                                          IsAllowed = userRolePermission.IsAllowed
                                                                      }).ToList();

                return userRolePermissionsList;
            }
        }

        public List<UserRolePermissionDataVM> List(UserRolePermissionDatatableParams datatableParams)
        {
            using (_myContext = new MyContext())
            {
                int pageNo = datatableParams.Start > 10 ? (datatableParams.Start / datatableParams.Length) : 1;
                List<UserRolePermissionDataVM> list;
                string sql = $"EXEC dbo.GetUserRolePermissionList '{ datatableParams.SearchText }', { pageNo }, " +
                    $"{datatableParams.Length},'{datatableParams.SortOrderColumn}','{datatableParams.OrderType}','{datatableParams.ModuleId}','{datatableParams.RoleId}'";

                list = _myContext.UserRolePermissionList.FromSqlRaw<UserRolePermissionDataVM>(sql).ToList();

                return list;

              }
        }

        public UserRolePermission Update(UserRolePermissionDataVM userRolePermissionVM)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    userRolePermission.IsAllowed = isAllow;
                    _myContext.SaveChanges();
                }
            }
        }

        public void UpdateFullPermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    // userRolePermission.CanCreate = isAllow;
                    //  userRolePermission.CanView = isAllow;
                    //   userRolePermission.CanUpdate = isAllow;
                    //   userRolePermission.CanDelete = isAllow;

                    _myContext.SaveChanges();
                }
            }
        }
    }
}
