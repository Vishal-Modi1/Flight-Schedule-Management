using DataModels.Models;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using ViewModels.VM;

namespace Repository
{
    public class UserRolePermissionRepository : IUserRolePermissionRepository
    {
        private MyContext _myContext;

        public List<UserRolePermissionVM> GetByRoleId(int roleId)
        {
            using (_myContext = new MyContext())
            {
                List<UserRolePermissionVM> userRolePermissionsList = (from userRolePermission in _myContext.UserRolePermissions
                                                                      join userRole in _myContext.UserRoles
                                                                      on userRolePermission.RoleId equals userRole.Id
                                                                      join permission in _myContext.Permissions
                                                                      on userRolePermission.PermissionId equals permission.Id
                                                                      join module in _myContext.ModuleDetails
                                                                      on userRolePermission.ModuleId equals module.Id
                                                                      where userRolePermission.RoleId == roleId
                                                                      select new UserRolePermissionVM()
                                                                      {
                                                                          Id = userRolePermission.Id,
                                                                          ModuleName = module.Name,
                                                                          RoleId = userRolePermission.RoleId,
                                                                          RoleName = userRole.Name,
                                                                          ActionName = module.ActionName,
                                                                          ControlllerName = module.ControllerName,
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

        public List<UserRolePermissionVM> List(UserRolePermissionDatatableParams datatableParams)
        {
            using (_myContext = new MyContext())
            {
                IQueryable<UserRolePermissionVM> userRolePermissionsInfo = (from userRolePermission in _myContext.UserRolePermissions
                                                                          join userRole in _myContext.UserRoles
                                                                          on userRolePermission.RoleId equals userRole.Id
                                                                          join permission in _myContext.Permissions
                                                                          on userRolePermission.PermissionId equals permission.Id
                                                                          join module in _myContext.ModuleDetails
                                                                          on userRolePermission.ModuleId equals module.Id
                                                                          select new UserRolePermissionVM()
                                                                          {
                                                                              Id = userRolePermission.Id,
                                                                              ModuleName = module.Name,
                                                                              RoleId = userRolePermission.RoleId,
                                                                              RoleName = userRole.Name,
                                                                              ActionName = module.ActionName,
                                                                              ControlllerName = module.ControllerName,
                                                                              Icon = module.Icon,
                                                                              DisplayName = module.DisplayName,
                                                                              ModuleId = module.Id,
                                                                              PermissionId = permission.Id,
                                                                              PermissionType = permission.PermissionType,
                                                                              IsAllowed = userRolePermission.IsAllowed
                                                                          }).AsQueryable();


                if(datatableParams.ModuleId != 0)
                {
                    userRolePermissionsInfo = userRolePermissionsInfo.Where(p => p.ModuleId == datatableParams.ModuleId);
                }

                if (datatableParams.RoleId != 0)
                {
                    userRolePermissionsInfo = userRolePermissionsInfo.Where(p => p.RoleId == datatableParams.RoleId);
                }

                if (datatableParams.ModuleId != 0 && datatableParams.RoleId != 0)
                {
                    userRolePermissionsInfo = userRolePermissionsInfo.Where(p => p.ModuleId == datatableParams.ModuleId && p.RoleId == datatableParams.RoleId);
                }

                var userRolePermissionsInfoList = userRolePermissionsInfo.ToList();

                if (datatableParams.SortOrderColumn == "RoleName")
                {
                    userRolePermissionsInfoList = userRolePermissionsInfoList.OrderBy(p => p.RoleName).ToList();

                    if (datatableParams.OrderType == "desc")
                    {
                        userRolePermissionsInfoList = userRolePermissionsInfoList.OrderByDescending(p => p.RoleName).ToList();
                    }
                }
                else if (datatableParams.SortOrderColumn == "ModuleName")
                {
                    userRolePermissionsInfoList = userRolePermissionsInfoList.OrderBy(p => p.ModuleName).ToList();

                    if (datatableParams.OrderType == "desc")
                    {
                        userRolePermissionsInfoList = userRolePermissionsInfoList.OrderByDescending(p => p.ModuleName).ToList();
                    }
                }


                if (!string.IsNullOrWhiteSpace(datatableParams.SearchText))
                {
                    userRolePermissionsInfoList = userRolePermissionsInfoList.Where(p => p.PermissionType.Contains(datatableParams.SearchText, System.StringComparison.OrdinalIgnoreCase) || p.RoleName.Contains(datatableParams.SearchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }

                int totalRecords = userRolePermissionsInfoList.Count();

                List<UserRolePermissionVM> userRolePermissionsList = userRolePermissionsInfoList.Skip(datatableParams.Start).Take(datatableParams.Length).ToList();

                if (userRolePermissionsList.Count() > 0)
                {
                    userRolePermissionsList[0].TotalRecords = totalRecords;
                }

                return userRolePermissionsList;
            }
        }

        public UserRolePermission Update(UserRolePermissionVM userRolePermissionVM)
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
