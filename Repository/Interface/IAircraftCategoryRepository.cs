using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface IAircraftCategoryRepository
    {
        AircraftCategory Create(AircraftCategory aircraftCategory);

        List<AircraftCategory> List();

        AircraftCategory FindByCondition(Expression<Func<AircraftCategory, bool>> predicate);
    }
}
