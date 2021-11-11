using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository
{
    public class StatusRepository : IStatusRepository
    {
        private MyContext _myContext;
 
        public List<Status> List()
        {
            return _myContext.Statuses.Where(c=> c.IsActive == true).ToList();
        }
    }
}
