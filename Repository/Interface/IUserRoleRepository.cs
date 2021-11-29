using System.Collections.Generic;
using ViewModels.VM.UserRole;

namespace Repository.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRoleVM> List();
    }
}
