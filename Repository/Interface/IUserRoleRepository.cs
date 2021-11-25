using DataModels.Models;
using System.Collections.Generic;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRoleVM> List();
    }
}
