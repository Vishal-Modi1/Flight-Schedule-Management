using ViewModels.VM;

namespace Service.Interface
{
    public interface IAirCraftService
    {
        CurrentResponse Create(AirCraftVM airCraftVM);
        CurrentResponse List(DatatableParams datatableParams);
        CurrentResponse Edit(AirCraftVM airCraftVM);
        CurrentResponse GetDetails(int id);
        CurrentResponse Delete(int id);

    }
}
