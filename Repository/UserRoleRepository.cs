using DataModels.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private MyContext _myContext;

        public List<UserRole> List()
        {
            using (_myContext = new MyContext())
            {
                return _myContext.UserRoles.ToList();
            }
        }
    }
}
