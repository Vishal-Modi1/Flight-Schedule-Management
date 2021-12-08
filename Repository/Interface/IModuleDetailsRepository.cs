using System.Collections.Generic;
using DataModels.VM.UserRolePermission;

namespace Repository.Interface
{
    public interface IModuleDetailsRepository
    {
        List<ModuleDetailsVM> List();
    }
}
