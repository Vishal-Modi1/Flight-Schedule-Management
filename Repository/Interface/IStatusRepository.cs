using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IStatusRepository
    {
       
        List<Status> List();

    }
}
