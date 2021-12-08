using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using DataModels.VM.UserRolePermission;

namespace Repository
{
    public class ModuleDetailsRepository : IModuleDetailsRepository
    {
        private MyContext _myContext;

        public List<ModuleDetailsVM> List()
        {
            using (_myContext = new MyContext())
            {
                List<ModuleDetailsVM> moduleDetailsList = (from moduleDetail in _myContext.ModuleDetails
                                                           select new ModuleDetailsVM
                                                           {
                                                               Id = moduleDetail.Id,
                                                               Name = moduleDetail.Name

                                                           }).ToList();

                return moduleDetailsList;
            }
        }
    }
}
