using System.Collections.Generic;
using DataModels.VM.UserRole;

namespace Repository.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRoleVM> List();
    }
}
