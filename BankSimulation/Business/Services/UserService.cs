using System;
using System.Collections.Generic;
using Business.Interfaces;
using DataContext.Repository;
using Entities.Models;

namespace Business.Services
{
	public class UserService:IUser
	{
        private readonly UserRepository userRepository;
        public UserService()
		{
            userRepository = new UserRepository();
		}
        public User Create(User user)
        {

            throw new NotImplementedException();
        }


        public int CashIn(User user, int ammount)
        {
            try
            {
                user.Balance += ammount;
                return ammount;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool CashOut(User user, int ammount)
        {
           
            try
            {
                if (user.Balance < ammount)
                {
                    return false;
                }
                user.Balance -= ammount;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Deposite(User user, int deposite)
        {
           
            try
            {
                if (deposite > user.Balance)
                {
                    return false;
                }
                user.Balance -= deposite;
                user.Depposite += deposite;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public User GetById(int id)
        {
            try
            {
                return userRepository.Get(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public User GetByName(string name)
        {
            try
            {
                return userRepository.Get(x => x.Name.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public User GetUserByCartNumbers(string cartNumbers)
        {
            try
            {
                return userRepository.Get(x => x.cartNumbers == cartNumbers);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool PinOpenBlock(User user, int newPin)
        {
            try
            {
                user.PinBlocked = true;
                user.Pin = newPin;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SendMoneyToUser(User from, User to, int ammount)
        {
            try
            {
                if (ammount>from.Balance)
                {
                    return false;
                }
                to.Balance += from.Balance + ammount;
                from.Balance -= ammount;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}

