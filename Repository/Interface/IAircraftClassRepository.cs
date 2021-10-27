using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface IAircraftClassRepository
    {
        AircraftClass Create(AircraftClass aircraftClass);

        List<AircraftClass> List();

        AircraftClass FindByCondition(Expression<Func<AircraftClass, bool>> predicate);
    }
}
