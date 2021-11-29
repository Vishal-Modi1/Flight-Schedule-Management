using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModels.VM.Common;

namespace Service
{
    public class ClassificationService : BaseService, IClassificationService
    {
        private readonly IClassificationRepository _classificationServiceRepository;

        public ClassificationService(IClassificationRepository classificationServiceRepository)
        {
            _classificationServiceRepository = classificationServiceRepository;
        }
        public CurrentResponse List()
        {
            try
            {
                List<Classification> statuses = new List<Classification>();
                statuses = _classificationServiceRepository.List();
                CreateResponse(statuses, HttpStatusCode.OK, "");
                return _currentResponse;
            }
            catch (Exception exc)
            {
                CreateResponse(null, HttpStatusCode.BadRequest, exc.ToString());
                return _currentResponse;
            }
        }
    }
}
