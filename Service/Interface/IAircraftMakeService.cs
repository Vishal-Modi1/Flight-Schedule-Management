using DataModels.Models;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IAircraftMakeService
    {
        CurrentResponse Create(AircraftMake aircraftMake);

        CurrentResponse List();
    }
}
