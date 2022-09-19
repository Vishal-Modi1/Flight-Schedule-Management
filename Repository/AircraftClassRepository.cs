﻿using DataModels.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class AircraftClassRepository : IAircraftClassRepository
    {
        private MyContext _myContext;

        public List<AircraftClass> List()
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AircraftClasses.ToList();
            }
        }

        public AircraftClass Create(AircraftClass aircraftClass)
        {
            using (_myContext = new MyContext())
            {
                _myContext.AircraftClasses.Add(aircraftClass);

                return aircraftClass;
            }
        }

        public AircraftClass FindByCondition(Expression<Func<AircraftClass, bool>> predicate)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AircraftClasses.Where(predicate).FirstOrDefault();
            }

        }
    }
}
