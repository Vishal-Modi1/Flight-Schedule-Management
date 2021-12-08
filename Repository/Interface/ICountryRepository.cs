using DataModels.Entities;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface ICountryRepository
    {
        List<Country> List();
    }
}
