using DataModels.Models;
using ViewModels.VM;

namespace Service.Interface
{
    public interface IAircraftModelService
    {
        CurrentResponse Create(AircraftModel aircraftModel);

        CurrentResponse List();
    }
}
