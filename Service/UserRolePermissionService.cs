using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class UserRolePermissionService : BaseService, IUserRolePermissionService
    {
        private readonly IUserRolePermissionRepository _userRolePermissionRepository;

        public UserRolePermissionService(IUserRolePermissionRepository userRolePermissionRepository)
        {
            _userRolePermissionRepository = userRolePermissionRepository;
        }

        public CurrentResponse GetByRoleId(int roleId)
        {
            try
            {
                List<UserRolePermissionVM> userRolePermissionsList = _userRolePermissionRepository.GetByRoleId(roleId);
                CreateResponse(userRolePermissionsList, HttpStatusCode.OK, "");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse List(DatatableParams datatableParams)
        {
            try
            {
                List<UserRolePermissionVM> userRolePermissionList = _userRolePermissionRepository.List(datatableParams);
                CreateResponse(userRolePermissionList, HttpStatusCode.OK, "");

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse UpdateCreatePermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdateCreatePermission(id, isAllow);

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

        public CurrentResponse UpdateDeletePermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdateDeletePermission(id, isAllow);

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

        public CurrentResponse UpdateEditPermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdateEditPermission(id, isAllow);

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
       
        public CurrentResponse UpdateViewPermission(int id, bool isAllow)
        {
            try
            {
                _userRolePermissionRepository.UpdateViewPermission(id, isAllow);

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

    }
}
