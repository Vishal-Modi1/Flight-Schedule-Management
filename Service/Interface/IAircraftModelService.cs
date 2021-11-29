using DataModels.Models;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IAircraftModelService
    {
        CurrentResponse Create(AircraftModel aircraftModel);

        CurrentResponse List();
    }
}
