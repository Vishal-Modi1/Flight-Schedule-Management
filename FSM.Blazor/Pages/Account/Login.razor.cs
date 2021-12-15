using Microsoft.JSInterop;
using Radzen;

namespace FSM.Blazor.Pages.Account
{
    public partial class Login
    {
        async Task OnLoginAsync(LoginArgs args, string name)
        {
            await btnLogin_Click();

            _loginService.LoginAsync(args.Username, args.Password);
            // console.Log($"{name} -> Username: {args.Username} and password: {args.Password}");
        }

        void OnRegister(string name)
        {
            //  console.Log($"{name} -> Register");
        }

        void OnResetPassword(string value, string name)
        {
            // console.Log($"{name} -> ResetPassword for user: {value}");
        }
    }
}
