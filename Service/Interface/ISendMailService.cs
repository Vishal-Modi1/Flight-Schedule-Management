using ViewModels.VM.Common;
using ViewModels.VM.User;

namespace Service.Interface
{
    public interface ISendMailService
    {
        bool NewUserAccountActivation(UserVM userVM, string token);
        CurrentResponse PasswordReset(string email, string url, string token);
    }
}
