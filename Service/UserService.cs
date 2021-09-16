using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CurrentResponse Create(UserVM userVM)
        {
            try
            {
                User user = new User();
                user = _userRepository.Create(user);

                CreateResponse(user, (int)HttpStatusCode.OK, "User is added successfully");
                
                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, (int)HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }
    }
}
