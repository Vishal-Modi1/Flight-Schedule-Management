using DataModels.Models;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IInstructorTypeRepository
    {
        List<InstructorType> List();
    }
}
