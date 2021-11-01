using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAircraftEquipmentTimeRepository
    {
        AircraftEquipmentTime Create(AircraftEquipmentTime airCraft);

        AircraftEquipmentTime Edit(AircraftEquipmentTime airCraft);
        
        void Delete(int id);

        AircraftEquipmentTime FindByCondition(Expression<Func<AircraftEquipmentTime, bool>> predicate);
    }
}
