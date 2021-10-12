using ViewModels.VM;

namespace Service.Interface
{
    public interface IAccountService
    {
        CurrentResponse GetValidUser(LoginVM loginVM);

        CurrentResponse IsValidToken(string token);

        CurrentResponse ActivateAccount(string token);
    }
}
