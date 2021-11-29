using ViewModels.VM.MyAccount;
using ViewModels.VM.Common;

namespace Service.Interface
{
    public interface IMyAccountService
    {
        CurrentResponse ChangePassword(ChangePasswordVM changePasswordVM);
    }
}
