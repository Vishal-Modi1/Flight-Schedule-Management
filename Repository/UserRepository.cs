using DataModels.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private MyContext _myContext;

        public User Create(User user)
        {
            using(_myContext = new MyContext())
            {
                _myContext.Users.Add(user);
                _myContext.SaveChanges();
                
                return user;
            }
        }


    }
}
