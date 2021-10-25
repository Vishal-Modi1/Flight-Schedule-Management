using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IAirCraftRepository
    {

        AirCraft Create(AirCraft airCraft);

        List<AirCraftVM> List(DatatableParams datatableParams);

        AirCraft Edit(AirCraft airCraft);

        AirCraft FindByCondition(Expression<Func<AirCraft, bool>> predicate);

        void Delete(int id);
    }
}
