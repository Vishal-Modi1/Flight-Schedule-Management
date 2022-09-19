﻿using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class AircraftService : BaseService, IAircraftService
    {
        private readonly IAircraftRepository _airCraftRepository;
        private readonly IAircraftCategoryRepository _aircraftCategoryRepository;
        private readonly IAircraftClassRepository _aircraftClassRepository;
        private readonly IAircraftMakeRepository _aircraftMakeRepository;
        private readonly IAircraftModelRepository _aircraftModelRepository;

        public AircraftService(IAircraftRepository airCraftRepository, IAircraftCategoryRepository aircraftCategoryRepository,
                               IAircraftClassRepository aircraftClassRepository, IAircraftMakeRepository aircraftMakeRepository,
                               IAircraftModelRepository aircraftModelRepository)
        {
            _airCraftRepository = airCraftRepository;
            _aircraftCategoryRepository = aircraftCategoryRepository;
            _aircraftClassRepository = aircraftClassRepository;
            _aircraftMakeRepository = aircraftMakeRepository;
            _aircraftModelRepository = aircraftModelRepository;
        }

        public CurrentResponse Create(AirCraftVM airCraftVM)
        {
            AirCraft airCraft = ToDataObject(airCraftVM);

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
            AirCraft airCraft = _airCraftRepository.FindByCondition(p => p.TailNo == airCraftVM.TailNo && p.Id != airCraftVM.Id);

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
            AirCraft airCraft = ToDataObject(airCraftVM);

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

            if (airCraft != null)
            {
                airCraftVM = ToBusinessObject(airCraft);
            }

            airCraftVM.AircraftCategoryList = _aircraftCategoryRepository.List();
            airCraftVM.AircraftClassList = _aircraftClassRepository.List();
            airCraftVM.AircraftMakeList = _aircraftMakeRepository.List();
            airCraftVM.AircraftModelList = _aircraftModelRepository.List();
            airCraftVM.FlightSimulatorClassList = _airCraftRepository.FlightSimulatorClassList();

            CreateResponse(airCraftVM, HttpStatusCode.OK, "");

            return _currentResponse;
        }

        private AirCraftVM ToBusinessObject(AirCraft airCraft)
        {
            AirCraftVM airCraftVM = new AirCraftVM();

            airCraftVM.Id = airCraft.Id;
            airCraftVM.TailNo = airCraft.TailNo;
            airCraftVM.ImageName = airCraft.ImageName;
            airCraftVM.ImagePath = $"https://localhost:44321/Uploads/AircraftImages/" + airCraftVM.ImageName;

            if (string.IsNullOrWhiteSpace(airCraftVM.ImageName))
            {
                airCraftVM.ImagePath = $"https://localhost:44321/no-image-available.jpeg";
            }

            airCraftVM.Year = airCraft.Year;
            airCraftVM.AircraftMakeId = airCraft.AircraftMakeId;
            airCraftVM.AircraftModelId = airCraft.AircraftModelId;
            airCraftVM.AircraftCategoryId = airCraft.AircraftCategoryId;
            airCraftVM.AircraftClassId = airCraft.AircraftClassId;
            airCraftVM.FlightSimulatorClassId = airCraft.FlightSimulatorClassId;
            airCraftVM.NoofEngines = airCraft.NoofEngines;
            airCraftVM.IsEngineshavePropellers = airCraft.IsEngineshavePropellers;
            airCraftVM.IsEnginesareTurbines = airCraft.IsEnginesareTurbines;
            airCraftVM.TrackOilandFuel = airCraft.TrackOilandFuel;
            airCraftVM.IsIdentifyMeterMismatch = airCraft.IsIdentifyMeterMismatch;

            airCraftVM.IsActive = airCraft.IsActive;
            airCraftVM.IsDeleted = airCraft.IsDeleted;
            airCraftVM.CreatedBy = airCraft.CreatedBy;
            airCraftVM.UpdatedBy = airCraft.UpdatedBy;
            airCraftVM.DeletedBy = airCraft.DeletedBy;
            airCraftVM.CreatedOn = airCraft.CreatedOn;
            airCraftVM.UpdatedOn = airCraft.UpdatedOn;
            airCraftVM.DeletedOn = airCraft.DeletedOn;

            return airCraftVM;
        }

        public List<AirCraftVM> ToBusinessObjectList(List<AirCraft> aircraftList)
        {
            List<AirCraftVM> airCraftVMList = new List<AirCraftVM>();

            foreach (AirCraft aircraft in aircraftList)
            {
                AirCraftVM airCraftVM = ToBusinessObject(aircraft);

                airCraftVMList.Add(airCraftVM);
            }

            return airCraftVMList;
        }

        public AirCraft ToDataObject(AirCraftVM airCraftVM)
        {
            AirCraft airCraft = new AirCraft();

            airCraft.Id = airCraftVM.Id;
            airCraft.TailNo = airCraftVM.TailNo;

            airCraft.Year = airCraftVM.Year;
            airCraft.AircraftMakeId = airCraftVM.AircraftMakeId;
            airCraft.AircraftModelId = airCraftVM.AircraftModelId;
            airCraft.AircraftCategoryId = airCraftVM.AircraftCategoryId;
            airCraft.AircraftClassId = airCraftVM.AircraftClassId;
            airCraft.FlightSimulatorClassId = airCraftVM.FlightSimulatorClassId;
            airCraft.NoofEngines = airCraftVM.NoofEngines;
            airCraft.IsEngineshavePropellers = airCraftVM.IsEngineshavePropellers;
            airCraft.IsEnginesareTurbines = airCraftVM.IsEnginesareTurbines;
            airCraft.TrackOilandFuel = airCraftVM.TrackOilandFuel;
            airCraft.IsIdentifyMeterMismatch = airCraftVM.IsIdentifyMeterMismatch;
            airCraft.IsActive = true;

            airCraft.CreatedBy = airCraftVM.CreatedBy;
            airCraft.UpdatedBy = airCraftVM.UpdatedBy;

            if (airCraftVM.Id == 0)
            {
                airCraft.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                airCraft.UpdatedOn = DateTime.UtcNow;
            }

            return airCraft;
        }

        public CurrentResponse List(AircraftFilterVM aircraftFilterVM)
        {
            try
            {
                List<AirCraft> airCraftList = _airCraftRepository.List(aircraftFilterVM);
                List<AirCraftVM> airCraftVMList = ToBusinessObjectList(airCraftList);

                CreateResponse(airCraftVMList, HttpStatusCode.OK, "");

                return _currentResponse;
            }

            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        public CurrentResponse UpdateImageName(int id, string imageName)
        {
            try
            {
                bool isImageNameUpdated = _airCraftRepository.UpdateImageName(id, imageName);

                CreateResponse(isImageNameUpdated, HttpStatusCode.OK, "");

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
