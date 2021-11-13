using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Service.Interface
{
    public interface IAirCraftEquipmentService
    {
        CurrentResponse Create(AirCraftEquipmentsVM airCraftEquipmentsVM);
        CurrentResponse Edit(AirCraftEquipmentsVM airCraftEquipmentsVM);
        CurrentResponse List(int airCraftId);
        CurrentResponse Delete(int id);
        CurrentResponse Fetch(int id);

    }
}
