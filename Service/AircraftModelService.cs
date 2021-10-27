﻿using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Net;
using ViewModels.VM;

namespace Service
{
    public class AircraftModelService : BaseService, IAircraftModelService
    {
        private readonly IAircraftModelRepository _aircraftModelRepository;

        public AircraftModelService(IAircraftModelRepository aircraftModelRepository)
        {
            _aircraftModelRepository = aircraftModelRepository;
        }

        public CurrentResponse Create(AircraftModel aircraftModel)
        {
            try
            {
                bool isAircraftModelExist = IsAircraftModelExist(aircraftModel);

                if (isAircraftModelExist)
                {
                    CreateResponse(aircraftModel, HttpStatusCode.Ambiguous, "Aircraft model is already exist");
                }
                else
                {
                    aircraftModel = _aircraftModelRepository.Create(aircraftModel);
                    CreateResponse(aircraftModel, HttpStatusCode.OK, "Aircraft model added successfully");
                }

                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.InternalServerError, exc.ToString());

                return _currentResponse;
            }
        }

        private bool IsAircraftModelExist(AircraftModel aircraftModel)
        {
            AircraftModel aircraftModelInfo = _aircraftModelRepository.FindByCondition(p => p.Name == aircraftModel.Name && p.Id != aircraftModel.Id);

            if (aircraftModelInfo == null)
            {
                return false;
            }

            return true;
        }

        public CurrentResponse List()
        {
            throw new NotImplementedException();
        }
    }
}
