using ViewModels.VM.Common;
using ViewModels.VM.AircraftEquipment;


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
