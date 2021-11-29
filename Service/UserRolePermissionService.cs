using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModels.VM.Common;
using ViewModels.VM.UserRolePermission;

namespace Service
{
    public class UserRolePermissionService : BaseService, IUserRolePermissionService
    {
        private readonly IUserRolePermissionRepository _userRolePermissionRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IModuleDetailsRepo _moduleDetailsRepo;

        public UserRolePermissionService(IUserRolePermissionRepository userRolePermissionRepository, 
            IUserRoleRepository userRoleRepository, IModuleDetailsRepo moduleDetailsRepo)
        {
            _userRolePermissionRepository = userRolePermissionRepository;
            _userRoleRepository = userRoleRepository;
            _moduleDetailsRepo = moduleDetailsRepo;
        }

        public CurrentResponse GetByRoleId(int roleId)
        {
            try
            {
                List<UserRolePermissionDataVM> userRolePermissionsList = _userRolePermissionRepository.GetByRoleId(roleId);
                CreateResponse(userRolePermissionsList, HttpStatusCode.OK, "");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse List(UserRolePermissionDatatableParams datatableParams)
        {
            try
            {
                List<UserRolePermissionDataVM> userRolePermissionList = _userRolePermissionRepository.List(datatableParams);
                CreateResponse(userRolePermissionList, HttpStatusCode.OK, "");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse UpdatePermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdatePermission(id, isAllow);

                string message = "Permission granted successfully.";

                if (!isAllow)
                {
                    message = "Permission denied successfully.";

                }

                CreateResponse(true, HttpStatusCode.OK, message);

                return _currentResponse;
            }

            catch (Exception exc)
            {
                CreateResponse(false, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse UpdateFullPermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdateFullPermission(id, isAllow);

                string message = "Permission granted successfully.";

                if (!isAllow)
                {
                    message = "Permission denied successfully.";

                }

                CreateResponse(true, HttpStatusCode.OK, message);

                return _currentResponse;
            }

            catch (Exception exc)
            {
                CreateResponse(false, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse GetFiltersValue()
        {
            try
            {
                UserRolePermissionFilterVM userRolePermissionFilterVM = new UserRolePermissionFilterVM();

                userRolePermissionFilterVM.UserRoleList = _userRoleRepository.List();
                userRolePermissionFilterVM.ModuleList = _moduleDetailsRepo.List();

                CreateResponse(userRolePermissionFilterVM, HttpStatusCode.OK, "");

                return _currentResponse;
            }

            catch (Exception exc)
            {
                CreateResponse(new UserRolePermissionFilterVM(), HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

    }
}
