using DataModels.Models;
using Repository.Interface;
using System.Linq;

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

        public User Edit(User user)
        {
            using (_myContext = new MyContext())
            {
                User existingDetails = _myContext.Users.Where(p => p.Id == user.Id).FirstOrDefault();

                if (existingDetails == null)
                    return user;

                existingDetails.CompanyName = user.CompanyName;
                existingDetails.DateofBirth = user.DateofBirth;
                existingDetails.Email = user.Email;
                existingDetails.FirstName = user.FirstName;
                existingDetails.LastName = user.LastName;
                existingDetails.Password = user.Password;
                existingDetails.Phone = user.Phone;
                existingDetails.RoleId = user.RoleId;
                existingDetails.IsIntructor = user.IsIntructor;
                existingDetails.InstructorTypeId = user.InstructorTypeId;
                existingDetails.Gender = user.Gender;
                existingDetails.ExternalId = user.ExternalId;
                existingDetails.CountryId = user.CountryId;
                existingDetails.ExternalId = user.ExternalId;
                existingDetails.IsSendEmailInvite = user.IsSendEmailInvite;
                existingDetails.Gender = user.Gender;

                _myContext.SaveChanges();

                return user;
            }
        }

        public bool IsEmailExist(string email)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.Users.Where(p => p.Email == email && p.IsDeleted != true).Count() > 0;
            }
        }

        public User FindById(int id)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.Users.Where(p => p.Id == id && p.IsDeleted != true).FirstOrDefault();
            }
        }
    }
}
