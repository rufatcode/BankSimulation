using System;
using System.Collections.Generic;
using DataContext.Interfaces;
using Entities.Models;

namespace DataContext.Repository
{
	public class UserRepository:IRepository<User>
	{
		public UserRepository()
		{
		}

        public bool Create(User entity)
        {
            try
            {
                DbContext.Users.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool Delete(User entity)
        {
            try
            {
                DbContext.Users.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Get(Predicate<User> filter)
        {
            return DbContext.Users.Find(filter);
        }

        public List<User> GetAll(Predicate<User> filter = null)
        {
            if (filter != null)
            {
                return DbContext.Users.FindAll(filter);
            }
            return DbContext.Users;
        }

        public bool Update(User entity)
        {
            try
            {
                User existBank = Get(x => x.Id == entity.Id);
                if (existBank != null)
                {
                    existBank = entity;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

