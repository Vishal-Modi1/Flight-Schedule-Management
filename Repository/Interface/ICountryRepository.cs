using DataModels.Models;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface ICountryRepository
    {
        List<Country> List();
    }
}
