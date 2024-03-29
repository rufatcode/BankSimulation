﻿using System;
using Entities.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{
	public interface IUser
	{
        public User Create(User user);
        public User Delete(int id);
        public User Update(User user);
        public User GetById(int id);
        public User GetByName(string name);
        public List<User> GetAll();
        public bool CashIn(User user, double ammount);
        public bool CashOut(User user, double ammount);
        public bool SendMoneyToUser(User from, User to, double ammount);
        public User GetUserByCartNumbers(string cartNumbers);
        public bool Deposite(User user, int deposite);
        public bool PinOpenBlock(User user, string newPin);
        public List<User> GetAllBlocked();


    }
}

