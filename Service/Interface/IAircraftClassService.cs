using DataModels.Models;
using ViewModels.VM;

namespace Service.Interface
{
    public interface IAircraftClassService
    {
        CurrentResponse Create(AircraftClass aircraftClass);

        CurrentResponse List();
    }
}
