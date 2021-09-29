using DataModels.Models;
using Repository.Interface;
using System.Linq;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private MyContext _myContext;

        public User GetValidUser(string email, string password)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.Users.Where(p => p.Email == email && p.Password == password && p.IsActive == true && p.IsDeleted != true).FirstOrDefault();
            }
        }
    }
}
