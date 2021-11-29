using DataModels.Models;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IAircraftCategoryService
    {
        CurrentResponse Create(AircraftCategory  aircraftCategory);

        CurrentResponse List();
    }
}
