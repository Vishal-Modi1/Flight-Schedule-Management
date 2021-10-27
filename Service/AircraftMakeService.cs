using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class AircraftMakeService : BaseService, IAircraftMakeService
    {
        private readonly IAircraftMakeRepository _aircraftMakeRepository;

        public AircraftMakeService(IAircraftMakeRepository aircraftMakeRepository)
        {
            _aircraftMakeRepository = aircraftMakeRepository;
        }

        public CurrentResponse Create(AircraftMake aircraftMake)
        {
            try
            {
                bool isAircraftMakeExist = IsAircraftMakeExist(aircraftMake);

                if (isAircraftMakeExist)
                {
                    CreateResponse(aircraftMake, HttpStatusCode.Ambiguous, "Aircraft make is already exist");
                }
                else
                {
                    aircraftMake = _aircraftMakeRepository.Create(aircraftMake);
                    CreateResponse(aircraftMake, HttpStatusCode.OK, "Aircraft make added successfully");
                }

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        private bool IsAircraftMakeExist(AircraftMake aircraftMake)
        {
            AircraftMake aircraftMakeInfo = _aircraftMakeRepository.FindByCondition(p => p.Name == aircraftMake.Name && p.Id != aircraftMake.Id);

            if (aircraftMakeInfo == null)
            {
                return false;
            }

            return true;
        }
    }
}
