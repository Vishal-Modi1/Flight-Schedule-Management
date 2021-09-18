using DataModels.Models;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class InstructorTypeRepository : IInstructorTypeRepository
    {
        private MyContext _myContext;

        public List<InstructorType> List()
        {
            using (_myContext = new MyContext())
            {
                  return _myContext.InstructorTypes.ToList();
            }
        }
    }
}
