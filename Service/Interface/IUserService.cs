using ViewModels.VM;

namespace Service.Interface
{
    public interface IUserService
    {
        UserVM Create(UserVM userVM);
    }
}
