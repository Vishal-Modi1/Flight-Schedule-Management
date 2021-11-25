using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IModuleDetailsRepo
    {
        List<ModuleDetailsVM> List();
    }
}
