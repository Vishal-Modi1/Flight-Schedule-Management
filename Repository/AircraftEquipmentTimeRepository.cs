using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository
{
    public class AircraftEquipmentTimeRepository : IAircraftEquipmentTimeRepository
    {
        private MyContext _myContext;
        public AircraftEquipmentTime Create(AircraftEquipmentTime aircraftEquipmentTime)
        {
            using (_myContext = new MyContext())
            {
                _myContext.AircraftEquipmentTimes.Add(aircraftEquipmentTime);
                _myContext.SaveChanges();

                return aircraftEquipmentTime;
            }
        }

        public AircraftEquipmentTime Edit(AircraftEquipmentTime aircraftEquipmentTime)
        {
            using (_myContext = new MyContext())
            {
                AircraftEquipmentTime existingAircraftEquipmentTime = _myContext.AircraftEquipmentTimes.Where(p => p.Id == aircraftEquipmentTime.Id).FirstOrDefault();

                if (existingAircraftEquipmentTime != null)
                {
                    existingAircraftEquipmentTime.EquipmentName = aircraftEquipmentTime.EquipmentName;
                    existingAircraftEquipmentTime.Hours = aircraftEquipmentTime.Hours;
                    existingAircraftEquipmentTime.AircraftId = aircraftEquipmentTime.AircraftId;
                }

                _myContext.SaveChanges();

                return aircraftEquipmentTime;
            }
        }
        public void Delete(int id)
        {
            using (_myContext = new MyContext())
            {
                AircraftEquipmentTime aircraftEquipmentTime = _myContext.AircraftEquipmentTimes.Where(p => p.Id == id).FirstOrDefault();

                if (aircraftEquipmentTime != null)
                {
                    _myContext.AircraftEquipmentTimes.Remove(aircraftEquipmentTime);
                    _myContext.SaveChanges();
                }
            }
        }

        public AircraftEquipmentTime FindByCondition(Expression<Func<AircraftEquipmentTime, bool>> predicate)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AircraftEquipmentTimes.Where(predicate).FirstOrDefault();
            }
        }
    }
}
