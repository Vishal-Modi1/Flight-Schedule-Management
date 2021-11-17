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
                            where userRolePermission.RoleId == roleId
                            select new UserRolePermissionVM()
                            {
                                Id = userRolePermission.Id,
                                CanCreate = userRolePermission.CanCreate,
                                CanDelete = userRolePermission.CanDelete,
                                CanView = userRolePermission.CanView,
                                CanUpdate = userRolePermission.CanUpdate,
                                ModuleName = userRolePermission.ModuleName,
                                RoleId = userRolePermission.RoleId,
                                RoleName = userRole.Name
                            }).ToList();

                return userRolePermissionsList;
            }
        }

        public List<UserRolePermissionVM> List(DatatableParams datatableParams)
        {
            using (_myContext = new MyContext())
            {
                var data = (from userRolePermission in _myContext.UserRolePermissions
                            join userRole in _myContext.UserRoles
                            on userRolePermission.RoleId equals userRole.Id
                            select new UserRolePermissionVM()
                            {
                                Id = userRolePermission.Id,
                                CanCreate = userRolePermission.CanCreate,
                                CanDelete = userRolePermission.CanDelete,
                                CanView = userRolePermission.CanView,
                                CanUpdate = userRolePermission.CanUpdate,
                                ModuleName = userRolePermission.ModuleName,
                                RoleId = userRolePermission.RoleId,
                                RoleName = userRole.Name
                            }).ToList();

                if (datatableParams.SortOrderColumn == "RoleName")
                {
                    data = data.OrderBy(p => p.RoleName).ToList();

                    if (datatableParams.OrderType == "desc")
                    {
                        data = data.OrderByDescending(p => p.RoleName).ToList();
                    }
                }
                else if (datatableParams.SortOrderColumn == "ModuleName")
                {
                    data = data.OrderBy(p => p.ModuleName).ToList();

                    if (datatableParams.OrderType == "desc")
                    {
                        data = data.OrderByDescending(p => p.ModuleName).ToList();
                    }
                }


                if(!string.IsNullOrWhiteSpace(datatableParams.SearchText))
                {
                    data = data.Where(p => p.ModuleName.Contains(datatableParams.SearchText,System.StringComparison.OrdinalIgnoreCase) || p.RoleName.Contains(datatableParams.SearchText, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }

                int totalRecords = data.Count();

                List<UserRolePermissionVM> userRolePermissionsList = data.Skip(datatableParams.Start).Take(datatableParams.Length).ToList();
               
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

        public void UpdateCreatePermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    userRolePermission.CanCreate = isAllow;
                    _myContext.SaveChanges();
                }
            }
        }

        public void UpdateEditPermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    userRolePermission.CanUpdate = isAllow;
                    _myContext.SaveChanges();
                }
            }
        }

        public void UpdateViewPermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    userRolePermission.CanView = isAllow;
                    _myContext.SaveChanges();
                }
            }
        }

        public void UpdateDeletePermission(int id, bool isAllow)
        {
            using (_myContext = new MyContext())
            {
                UserRolePermission userRolePermission = _myContext.UserRolePermissions.Where(p => p.Id == id).FirstOrDefault();

                if (userRolePermission != null)
                {
                    userRolePermission.CanDelete = isAllow;
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
                    userRolePermission.CanCreate = isAllow;
                    userRolePermission.CanView = isAllow;
                    userRolePermission.CanUpdate = isAllow;
                    userRolePermission.CanDelete = isAllow;

                    _myContext.SaveChanges();
                }
            }
        }
    }
}
