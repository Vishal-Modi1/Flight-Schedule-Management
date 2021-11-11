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
    public class StatusService : BaseService, IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public CurrentResponse List()
        {
            try
            {
                List<Status> statuses = new List<Status>();
                statuses = _statusRepository.List();
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
