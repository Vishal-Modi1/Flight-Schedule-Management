using ViewModels.VM;

namespace Service.Interface
{
    public interface ISendMailService
    {
        bool NewUserAccountActivation(UserVM userVM, string token);
        CurrentResponse PasswordReset(string email, string url, string token);
    }
}
