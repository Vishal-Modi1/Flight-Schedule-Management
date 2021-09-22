using DataModels.Models;
using System.Collections.Generic;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User Create(User user);

        bool IsEmailExist(string email);

        User FindById(int id);

        User Edit(User user);

        List<UserSearchList> List(DatatableParams datatableParams);

        void Delete(int id);

        void UpdateActiveStatus(int id, bool isActive);

    }
}
