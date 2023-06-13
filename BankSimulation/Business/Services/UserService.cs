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
        private readonly BankService bankService;
        private static int _count = 1;
        public UserService()
		{
            bankService = new BankService();
            userRepository = new UserRepository();
		}
        public User Create(User user)
        {
            try
            {
                user.Id = _count;
                var users = GetAll();
                Random random = new Random();


                while (user.cartNumbers == null)
                {
                    int cartNumberpart1 = random.Next(1000, 10000);
                    int cartNumberpart2 = random.Next(1000, 10000);
                    user.cartNumbers += user.Bank.Signature + " " + cartNumberpart1 + " " + cartNumberpart2;
                    foreach (var item in users)
                    {
                        if (user.cartNumbers == item.cartNumbers)
                        {
                            user.cartNumbers = null;
                            break;
                        }
                    }
                }
                while (user.Cvv == null)
                {
                    user.Cvv = Convert.ToString(random.Next(100, 1000));
                    foreach (var item in users)
                    {
                        if (user.Cvv == item.Cvv)
                        {
                            user.Cvv = null;
                            break;
                        }
                    }
                }
                user.ActivityDate = DateTime.Now.AddYears(5);
                if (GetById(user.Id) == null)
                {
                    userRepository.Create(user);
                    bankService.GetById(user.Bank.Id).Users.Add(user);
                    _count++;
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            
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
            try
            {
                var deletedUser = GetById(id);
                if (deletedUser == null)
                {
                    return null;
                }
                bankService.GetById(deletedUser.Bank.Id).Users.Remove(deletedUser);
                userRepository.Delete(deletedUser);
                return deletedUser;
                
            }
            catch (Exception ex)
            {
                throw;
            }
            
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

        public bool PinOpenBlock(User user, string newPin)
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

        public bool Get(User user)
        {
            throw new NotImplementedException();
        }
    }
}

