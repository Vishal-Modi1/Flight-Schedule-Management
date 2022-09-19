using DataModels.Models;
using ViewModels.VM;

namespace Service.Interface
{
    public interface IAircraftCategoryService
    {
        CurrentResponse Create(AircraftCategory  aircraftCategory);

        CurrentResponse List();
    }
}
