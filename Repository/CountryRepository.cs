using DataModels.Models;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CountryRepository : ICountryRepository
    {
        private MyContext _myContext;

        public List<Country> List()
        {
            using (_myContext = new MyContext())
            {
                return _myContext.Countries.ToList();
            }
        }
    }
}
