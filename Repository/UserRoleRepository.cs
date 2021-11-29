using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using ViewModels.VM.UserRole;

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
