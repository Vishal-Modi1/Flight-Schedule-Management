using ViewModels.VM;

namespace Repository.Interface
{
    public interface IMyAccountRepository
    {
        bool ChangePassword(ChangePasswordVM changePasswordVM);

        bool IsValidOldPassword(string password, int userId);
    }
}
