using DataModels.Models;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRole> List();
    }
}
