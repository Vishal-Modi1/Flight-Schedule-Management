using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public CurrentResponse GetValidUser(LoginVM loginVM)
        {
            try
            {
                User user = _accountRepository.GetValidUser(loginVM.Email, loginVM.Password);
                if (user != null && user.Id > 0)
                    CreateResponse(user, HttpStatusCode.OK, "User is valid");
                else
                    CreateResponse(null, HttpStatusCode.NotFound, "User is not valid");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());
                return _currentResponse;
            }
        }
    }
}
