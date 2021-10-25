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
    public class AirCraftRepository : IAirCraftRepository
    {
        private MyContext _myContext;

        public AirCraft Create(AirCraft airCraft)
        {
            using (_myContext = new MyContext())
            {
                _myContext.AirCrafts.Add(airCraft);
                _myContext.SaveChanges();

                return airCraft;
            }
        }

        public void Delete(int id)
        {
            using (_myContext = new MyContext())
            {
                AirCraft airCraft = _myContext.AirCrafts.Where(p => p.Id == id).FirstOrDefault();

                if (airCraft != null)
                {
                    _myContext.AirCrafts.Remove(airCraft);
                    _myContext.SaveChanges();
                }
            }
        }

        public AirCraft Edit(AirCraft airCraft)
        {
            using (_myContext = new MyContext())
            {
                AirCraft existingAirCraft = _myContext.AirCrafts.Where(p => p.Id == airCraft.Id).FirstOrDefault();

                if (existingAirCraft != null)
                {
                    //  existingAirCraft.Name = instructorType.Name;
                }

                _myContext.SaveChanges();

                return airCraft;
            }
        }

        public AirCraft FindByCondition(Expression<Func<AirCraft, bool>> predicate)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.AirCrafts.Where(predicate).FirstOrDefault();
            }
        }

        public List<AirCraftVM> List(DatatableParams datatableParams)
        {
            using (_myContext = new MyContext())
            {
                int pageNo = datatableParams.Start > 10 ? (datatableParams.Start / datatableParams.Length) : 1;
                List<AirCraftVM> list;

                string sql = $"EXEC dbo.GetAirCraftList '{ datatableParams.SearchText }', { pageNo }, {datatableParams.Length},'{datatableParams.SortOrderColumn}','{datatableParams.OrderType}'";

                list = _myContext.AirCraftVM.FromSqlRaw<AirCraftVM>(sql).ToList();

                return list;
            }
        }

    }
}
