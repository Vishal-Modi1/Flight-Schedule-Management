using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface IAircraftModelRepository
    {
        AircraftModel Create(AircraftModel aircraftModel);

        List<AircraftModel> List();

        AircraftModel FindByCondition(Expression<Func<AircraftModel, bool>> predicate);

    }
}
