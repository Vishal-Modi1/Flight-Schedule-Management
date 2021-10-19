﻿using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Service
{
    public class MyAccountService : BaseService, IMyAccountService
    {
        private readonly IMyAccountRepository _myAccountRepository;

        public MyAccountService(IMyAccountRepository myAccountRepository)
        {
            _myAccountRepository = myAccountRepository;
        }

        public CurrentResponse ChangePassword(ChangePasswordVM changePasswordVM)
        {
            bool isValidOldPassword = _myAccountRepository.IsValidOldPassword(changePasswordVM.OldPassword, changePasswordVM.UserId);

            if(!isValidOldPassword)
            {
                CreateResponse(isValidOldPassword, HttpStatusCode.ExpectationFailed, "Old password is invalid");
                return _currentResponse;
            }

            bool isPasswordChange = _myAccountRepository.ChangePassword(changePasswordVM);

            CreateResponse(isPasswordChange, HttpStatusCode.OK, "Password has been updated successfully");

            return _currentResponse;
        }
    }
}
