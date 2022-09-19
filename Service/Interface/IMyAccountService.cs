using ViewModels.VM;

namespace Service.Interface
{
    public interface IMyAccountService
    {
        CurrentResponse ChangePassword(ChangePasswordVM changePasswordVM);
    }
}
