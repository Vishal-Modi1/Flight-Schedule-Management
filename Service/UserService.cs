using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using ViewModels.VM;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserVM Create(UserVM userVM)
        {
            User user = new User();
            _userRepository.Create(user);

            return userVM;
        }
    }
}
