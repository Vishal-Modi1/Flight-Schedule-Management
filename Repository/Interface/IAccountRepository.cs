using DataModels.Models;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        User GetValidUser(string email, string password);
    }
}
