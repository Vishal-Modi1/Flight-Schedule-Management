using DataModels.Models;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IAircraftClassService
    {
        CurrentResponse Create(AircraftClass aircraftClass);

        CurrentResponse List();
    }
}
