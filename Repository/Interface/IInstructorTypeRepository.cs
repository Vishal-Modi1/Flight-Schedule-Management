using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViewModels.VM;

namespace Repository.Interface
{
    public interface IInstructorTypeRepository
    {
        List<InstructorType> List();

        InstructorType Create(InstructorType instructorType);

        List<InstructorTypeVM> List(DatatableParams datatableParams);

        InstructorType Edit(InstructorType instructorType);

        InstructorType FindByCondition(Expression<Func<InstructorType, bool>> predicate);

        void Delete(int id);
    }
}
