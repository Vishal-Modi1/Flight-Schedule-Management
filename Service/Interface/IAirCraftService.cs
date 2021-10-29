using ViewModels.VM;

namespace Service.Interface
{
    public interface IAircraftService
    {
        CurrentResponse Create(AirCraftVM airCraftVM);
        CurrentResponse List(DatatableParams datatableParams);
        CurrentResponse Edit(AirCraftVM airCraftVM);
        CurrentResponse GetDetails(int id);
        CurrentResponse Delete(int id);
        CurrentResponse UpdateImageName(int id, string imageName);
    }
}
