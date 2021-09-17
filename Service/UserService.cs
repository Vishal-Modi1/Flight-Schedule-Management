using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IInstructorTypeRepository _instructorTypeRepository;
        private readonly ICountryRepository _countryRepository;

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository
            , IInstructorTypeRepository instructorTypeRepository, ICountryRepository countryRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _instructorTypeRepository = instructorTypeRepository;
            _countryRepository = countryRepository;
        }

        public CurrentResponse GetDetails()
        {
            UserVM userVM = new UserVM();
            userVM.Countries = ListCountries();
            userVM.InstructorTypes = ListInstructorTypes();
            userVM.UserRoles = ListUserRoles();

            CreateResponse(userVM, HttpStatusCode.OK, "");

            return _currentResponse;
        }

        public CurrentResponse Create(UserVM userVM)
        {
            try
            {
                User user = new User();
                user = _userRepository.Create(user);

                CreateResponse(user, HttpStatusCode.OK, "User is added successfully");
                
                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        private List<InstructorTypeVM> ListInstructorTypes()
        {
            List<InstructorType> instructorTypesList =  _instructorTypeRepository.List();
            List<InstructorTypeVM> instructorTypesListVM = new List<InstructorTypeVM>();

            foreach (InstructorType instructorType in instructorTypesList)
            {
                instructorTypesListVM.Add(new InstructorTypeVM() { Id = instructorType.Id, Name = instructorType.Name });
            }

            return instructorTypesListVM;
        }

        private List<UserRoleVM> ListUserRoles()
        {
            List<UserRole> userRoles = _userRoleRepository.List();
            List<UserRoleVM> userRoleListVM = new List<UserRoleVM>();

            foreach (UserRole userRole in userRoles)
            {
                userRoleListVM.Add(new UserRoleVM() { Id = userRole.Id, Name = userRole.Name });
            }

            return userRoleListVM;
        }

        private List<CountryVM> ListCountries()
        {
            List<Country> countries = _countryRepository.List();
            List<CountryVM> countryListVM = new List<CountryVM>();

            foreach (Country country in countries)
            {
                countryListVM.Add(new CountryVM() { Id = country.Id, Name = country.Name });
            }

            return countryListVM;
        }
    }
}
