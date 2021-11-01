using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Service.Interface
{
    public interface IAircraftEquipementTimeService
    {
        CurrentResponse Create(AircraftEquipmentTimeVM aircraftEquipmentTimeVM);
    }
}
