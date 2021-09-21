using DataModels.Models;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User Create(User user);

        bool IsEmailExist(string email);

        User FindById(int id);

        User Edit(User user);
    }
}
