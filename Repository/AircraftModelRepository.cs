using DataModels.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class AircraftModelRepository : IAircraftModelRepository
    {
        private MyContext _myContext;

        public List<AircraftModel> List()
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AircraftModels.ToList();
            }
        }

        public AircraftModel Create(AircraftModel aircraftModel)
        {
            using (_myContext = new MyContext())
            {
                _myContext.AircraftModels.Add(aircraftModel);

                return aircraftModel;
            }
        }

        public AircraftModel FindByCondition(Expression<Func<AircraftModel, bool>> predicate)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AircraftModels.Where(predicate).FirstOrDefault();
            }
        }
    }
}
