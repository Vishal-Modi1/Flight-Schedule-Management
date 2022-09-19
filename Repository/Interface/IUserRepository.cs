using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User Create(User user);

        bool IsEmailExist(string email);

        User FindByCondition(Expression<Func<User, bool>> predicate);

        User Edit(User user);

        List<UserSearchList> List(DatatableParams datatableParams);

        void Delete(int id);

        void UpdateActiveStatus(int id, bool isActive);
        bool ResetUserPassword(ResetPasswordVM resetPasswordVM);

    }
}
