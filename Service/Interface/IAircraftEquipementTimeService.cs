using ViewModels.VM;
using ViewModels.VM.AircraftEquipment;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IAircraftEquipementTimeService
    {
        CurrentResponse Create(AircraftEquipmentTimeVM aircraftEquipmentTimeVM);
        bool DeleteAllEquipmentTimeByAirCraftId(int AirCraftId,int UpdatedBy);
    }
}
