using DataModels.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

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
