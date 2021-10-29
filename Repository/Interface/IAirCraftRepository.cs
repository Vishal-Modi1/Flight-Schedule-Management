using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IAircraftRepository
    {

        AirCraft Create(AirCraft airCraft);

        List<AirCraft> List(DatatableParams datatableParams);

        AirCraft Edit(AirCraft airCraft);

        AirCraft FindByCondition(Expression<Func<AirCraft, bool>> predicate);

        void Delete(int id);

        List<FlightSimulatorClass> FlightSimulatorClassList();

        bool UpdateImageName(int id, string imageName);
    }
}
