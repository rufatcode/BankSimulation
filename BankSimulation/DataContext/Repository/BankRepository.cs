using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataContext.Interfaces;
using Entities.Interfaces;
using Entities.Models;

namespace DataContext.Repository
{
	public class BankRepository:IRepository<Bank>
	{
		public BankRepository()
		{

		}

        public bool Create(Bank entity)
        {
            try
            {
                DbContext.Banks.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool Delete(Bank entity)
        {
            try
            {
                DbContext.Banks.Remove(entity);

                DbContext.DeletingBanks.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Bank Get(Predicate<Bank> filter)
        {
            return DbContext.Banks.Find(filter);
        }

        public List<Bank> GetAll(Predicate<Bank> filter = null)
        {
            if (filter!=null)
            {
                return DbContext.Banks.FindAll(filter);
            }
            return DbContext.Banks;
        }

        public bool Update(Bank entity)
        {
            try
            {
                Bank existBank = Get(x => x.Id == entity.Id);
                existBank = entity;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public List<Bank> GetAllDeleting(Predicate<Bank> filter = null)
        {
            if (filter!=null)
            {
                return DbContext.DeletingBanks.FindAll(filter);
            }
            return DbContext.DeletingBanks;
        }

    }
}

