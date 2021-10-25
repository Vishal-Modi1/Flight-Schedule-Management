using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class AircraftService : BaseService, IAirCraftService
    {
        private readonly IAirCraftRepository _airCraftRepository;

        public AircraftService(IAirCraftRepository airCraftRepository)
        {
            _airCraftRepository = airCraftRepository;
        }

        public CurrentResponse Create(AirCraftVM airCraftVM)
        {
            AirCraft airCraft = new AirCraft() { };

            try
            {
                bool isAirCraftExist = IsAirCraftExist(airCraftVM);

                if (isAirCraftExist)
                {
                    CreateResponse(airCraft, HttpStatusCode.Ambiguous, "Aircraft is already exist");
                }
                else
                {
                    airCraft = _airCraftRepository.Create(airCraft);
                    CreateResponse(airCraft, HttpStatusCode.OK, "Aircraft added successfully");
                }

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        private bool IsAirCraftExist(AirCraftVM airCraftVM)
        {
            AirCraft airCraft = _airCraftRepository.FindByCondition(p => p.Name == airCraftVM.Name && p.Id != airCraftVM.Id);

            if (airCraft == null)
            {
                return false;
            }

            return true;
        }

        public CurrentResponse Delete(int id)
        {
            try
            {
                _airCraftRepository.Delete(id);
                CreateResponse(true, HttpStatusCode.OK, "Aircraft is deleted successfully.");

                return _currentResponse;
            }

            catch (Exception exc)
            {
                CreateResponse(false, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse Edit(AirCraftVM airCraftVM)
        {
            AirCraft airCraft= new AirCraft() { Id = airCraftVM.Id, Name = airCraftVM.Name };

            try
            {
                bool isInstructorTypeExist = IsAirCraftExist(airCraftVM);

                if (isInstructorTypeExist)
                {
                    CreateResponse(airCraft, HttpStatusCode.Ambiguous, "Aircraft is already exist");
                }

                else
                {
                    airCraft = _airCraftRepository.Edit(airCraft);
                    CreateResponse(airCraft, HttpStatusCode.OK, "Aircraft updated successfully");
                }

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse GetDetails(int id)
        {
            AirCraft airCraft = _airCraftRepository.FindByCondition(p => p.Id == id);
            AirCraftVM airCraftVM = new AirCraftVM();

            if (airCraftVM != null)
            {
                airCraftVM = ToBusinessObject(airCraft);
            }

            CreateResponse(airCraftVM, HttpStatusCode.OK, "");

            return _currentResponse;
        }

        private AirCraftVM ToBusinessObject(AirCraft airCraft)
        {
            throw new NotImplementedException();
        }

        public CurrentResponse List(DatatableParams datatableParams)
        {
            try
            {
                var data = _airCraftRepository.List(datatableParams);

                CreateResponse(data, HttpStatusCode.OK, "");

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
