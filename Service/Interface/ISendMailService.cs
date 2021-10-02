using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Service.Interface
{
    public interface ISendMailService
    {
        bool SendCreateUserMail(UserVM userVM);
        CurrentResponse PasswordReset(string Email, string Url);
    }
}
