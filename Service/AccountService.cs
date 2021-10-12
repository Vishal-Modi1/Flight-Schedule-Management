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

                if (user == null)
                {
                    CreateResponse(null, HttpStatusCode.NotFound, "Invalid Credentials");
                }
                else if (!user.IsActive)
                {
                    CreateResponse(null, HttpStatusCode.NotFound, "Your account is not activated");
                }
                else if (user.IsDeleted)
                {
                    CreateResponse(null, HttpStatusCode.NotFound, "Your account has been deleted");
                }
                else
                {
                    CreateResponse(user, HttpStatusCode.OK, "User is valid");
                }

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());
                return _currentResponse;
            }
        }

        public CurrentResponse IsValidToken(string token)
        {
            bool isValidToken = _accountRepository.IsValidToken(token);

            if (isValidToken)
            {
                CreateResponse(isValidToken, HttpStatusCode.OK, "Valid Token");
            }
            else
            {
                CreateResponse(isValidToken, HttpStatusCode.OK, "Token is expired");
            }

            return _currentResponse;
        }

        public CurrentResponse ActivateAccount(string token)
        {
            bool isActivated = _accountRepository.ActivateAccount(token);

            if (isActivated)
            {
                CreateResponse(isActivated, HttpStatusCode.OK, "Account activated");
            }
            else
            {
                CreateResponse(isActivated, HttpStatusCode.OK, "Account activation failed");
            }

            return _currentResponse;
        }
    }
}
