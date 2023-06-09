using System;
using Entities.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{
	public interface IUser
	{
        public User Create(User user);
        public User Delete(int id);
        public User Update(int id);
        public User GetById(int id);
        public User GetByName(string name);
        public List<User> GetAll();
    }
}

