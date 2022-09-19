using ViewModels.VM;

namespace Service.Interface
{
    public interface IUserService
    {
        CurrentResponse Create(UserVM userVM);

        CurrentResponse GetDetails(int id);

        CurrentResponse IsEmailExist(string email);

        CurrentResponse Edit(UserVM userVM);

        CurrentResponse List(DatatableParams datatableParams);

        CurrentResponse Delete(int id);

        CurrentResponse UpdateActiveStatus(int id, bool isActive);
        
        CurrentResponse ResetPassword(ResetPasswordVM resetPasswordVM);
    }
}
