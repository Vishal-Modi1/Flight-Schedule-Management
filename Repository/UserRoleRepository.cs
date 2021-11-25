using DataModels.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private MyContext _myContext;

        public List<UserRoleVM> List()
        {
            using (_myContext = new MyContext())
            {
                List<UserRoleVM> userRoleList = (from userRole in _myContext.UserRoles
                                                 select new UserRoleVM()
                                                 {
                                                     Id = userRole.Id,
                                                     Name = userRole.Name
                                                 }).ToList();

                return userRoleList;
            }
        }
    }
}
