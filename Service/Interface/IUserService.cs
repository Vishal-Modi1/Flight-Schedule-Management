using ViewModels.VM;

namespace Service.Interface
{
    public interface IUserService
    {
        CurrentResponse Create(UserVM userVM);

        CurrentResponse GetDetails(int id);

        CurrentResponse IsEmailExist(string email);

        CurrentResponse Edit(UserVM userVM);
    }
}
