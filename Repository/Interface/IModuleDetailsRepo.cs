using System.Collections.Generic;
using ViewModels.VM.UserRolePermission;

namespace Repository.Interface
{
    public interface IModuleDetailsRepo
    {
        List<ModuleDetailsVM> List();
    }
}
