﻿using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ViewModels.VM;

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

        public InstructorType Create(InstructorType instructorType)
        {
            using (_myContext = new MyContext())
            {
                _myContext.InstructorTypes.Add(instructorType);
                _myContext.SaveChanges();

                return instructorType;
            }
        }

        public InstructorType Edit(InstructorType instructorType)
        {
            using (_myContext = new MyContext())
            {
                InstructorType existingInstructorType = _myContext.InstructorTypes.Where(p=>p.Id == instructorType.Id).FirstOrDefault();
               
                if(existingInstructorType != null)
                {
                    existingInstructorType.Name = instructorType.Name;
                }
                
                _myContext.SaveChanges();

                return instructorType;
            }
        }

        public void Delete(int id)
        {
            using (_myContext = new MyContext())
            {
                InstructorType instructorType = _myContext.InstructorTypes.Where(p => p.Id == id).FirstOrDefault();

                if (instructorType != null)
                {
                    _myContext.InstructorTypes.Remove(instructorType);
                    _myContext.SaveChanges();
                }
            }
        }

        public List<InstructorTypeVM> List(DatatableParams datatableParams)
        {
            using (_myContext = new MyContext())
            {
                int pageNo = datatableParams.Start > 10 ? (datatableParams.Start / datatableParams.Length) : 1;
                List<InstructorTypeVM> list;

                string sql = $"EXEC dbo.GetInstructorTypeList '{ datatableParams.SearchText }', { pageNo }, {datatableParams.Length},'{datatableParams.SortOrderColumn}','{datatableParams.OrderType}'";

                list = _myContext.InstructorTypeVM.FromSqlRaw<InstructorTypeVM>(sql).ToList();

                return list;
            }
        }

        public InstructorType FindByCondition(Expression<Func<InstructorType, bool>> predicate)
        {
            using (_myContext = new MyContext())
            {
                return _myContext.InstructorTypes.Where(predicate).FirstOrDefault();
            }
        }
    }
}
