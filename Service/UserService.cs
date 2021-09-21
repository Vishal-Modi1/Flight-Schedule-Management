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

        public CurrentResponse GetDetails(int id)
        {
            User user = _userRepository.FindById(id);
            UserVM userVM = new UserVM();

            if (user != null)
            {
                userVM = ToBusinessObject(user);
            }
            
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
                User user = ToDataObject(userVM);
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

        public CurrentResponse Edit(UserVM userVM)
        {
            try
            {
                User user = ToDataObject(userVM);
                user = _userRepository.Edit(user);

                CreateResponse(user, HttpStatusCode.OK, "User updated successfully");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse IsEmailExist(string email)
        {
            try
            {
                bool isEmailExist = _userRepository.IsEmailExist(email);

                CreateResponse(isEmailExist, HttpStatusCode.OK, "");

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

        #region Object Mapping

        public User ToDataObject(UserVM userVM)
        {
            User user = new User();

            user.Id = userVM.Id;
            user.CompanyName = userVM.CompanyName;
            user.DateofBirth = userVM.DateofBirth;
            user.Email = userVM.Email;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Password = userVM.Password;
            user.Phone = userVM.Phone;
            user.RoleId = userVM.RoleId;
            user.IsIntructor = userVM.IsInstructor;
            user.InstructorTypeId = (bool) userVM.IsInstructor ? userVM.InstructorTypeId : null ;
            user.Gender = userVM.Gender;
            user.ExternalId = userVM.ExternalId;
            user.CountryId = userVM.CountryId;
            user.ExternalId = userVM.ExternalId;
            user.IsSendEmailInvite = userVM.IsSendEmailInvite;
            user.Gender = userVM.Gender;

            if (userVM.Id == 0)
            {
                user.CreatedOn = DateTime.UtcNow;
            }

            user.UpdatedOn = DateTime.UtcNow;

            return user;
        }

        public UserVM ToBusinessObject(User user)
        {
            UserVM userDetails = new UserVM();

            userDetails.Id = user.Id;
            userDetails.CompanyName = user.CompanyName;
            userDetails.DateofBirth = user.DateofBirth;
            userDetails.Email = user.Email;
            userDetails.FirstName = user.FirstName;
            userDetails.LastName = user.LastName;
            userDetails.Password = user.Password;
            userDetails.Phone = user.Phone;
            userDetails.RoleId = user.RoleId;
            userDetails.IsInstructor = user.IsIntructor;
            userDetails.InstructorTypeId = user.InstructorTypeId == null ? 0 : (int)user.InstructorTypeId;
            userDetails.Gender = user.Gender;
            userDetails.ExternalId = user.ExternalId;
            userDetails.CountryId = user.CountryId;
            userDetails.ExternalId = user.ExternalId;
            userDetails.IsSendEmailInvite = user.IsSendEmailInvite;
            userDetails.Gender = user.Gender;


            return userDetails;
        }

        #endregion

    }
}
