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
        public int CashIn(User user, int ammount);
        public bool CashOut(User user, int ammount);
        public bool SendMoneyToUser(User from, User to, int ammount);
        public User GetUserByCartNumbers(string cartNumbers);
        public bool Deposite(User user, int deposite);
        public bool PinOpenBlock(User user, string newPin);
        public bool Get(User user);


    }
}

