using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAirCraftEquipmentRepository
    {
        AirCraftEquipment Create(AirCraftEquipment airCraft);

        AirCraftEquipment Edit(AirCraftEquipment airCraft);
        
        void Delete(int id);

        List<AirCraftEquipment> List();

        AirCraftEquipment FindByCondition(Expression<Func<AirCraftEquipment, bool>> predicate);

        List<AirCraftEquipment> FindListByCondition(Expression<Func<AirCraftEquipment, bool>> predicate);
    }
}
