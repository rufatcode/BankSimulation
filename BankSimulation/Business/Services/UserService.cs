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
                    if (users!=null)
                    {
                        foreach (var item in users)
                        {
                            if (user.cartNumbers == item.cartNumbers)
                            {
                                user.cartNumbers = null;
                                break;
                            }
                        }
                    }
                    
                }
                while (user.Cvv == null)
                {
                    user.Cvv = Convert.ToString(random.Next(100, 1000));
                    if (users!=null)
                    {
                        foreach (var item in users)
                        {
                            if (user.Cvv == item.Cvv)
                            {
                                user.Cvv = null;
                                break;
                            }
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


        public bool CashIn(User user, double ammount)
        {
            try
            {
                if (ammount<=0)
                {
                    return false;
                }
                user.Balance += ammount;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool CashOut(User user, double ammount)
        {
           
            try
            {
                if (user.Balance < ammount||ammount<=0)
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
                if (userRepository.GetAll().Count==0)
                {
                    return null;
                }
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
                var user= userRepository.Get(x => x.Id == id);
                if (user==null)
                {
                    return null;
                }
                return user;
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
                var user= userRepository.Get(x => x.Name.ToLower() == name.ToLower());
                if (user==null)
                {
                    return null;
                }
                return user;
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
                var user= userRepository.Get(x => x.cartNumbers == cartNumbers);
                if (user==null)
                {
                    return null;
                }
                return user;
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
                user.PinBlocked = false;
                user.Pin = newPin;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<User> GetAllBlocked()
        {
            try
            {
                if (userRepository.GetAllDeleting().Count==0)
                {
                    return null;
                }
                return userRepository.GetAllDeleting();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool SendMoneyToUser(User from, User to, double ammount)
        {
            try
            {
                if (ammount>from.Balance||ammount<=0)
                {
                    return false;
                }
                to.Balance += ammount;
                from.Balance -= ammount;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Update(User user)
        {
            var existUser = userRepository.Get(x => x.Id == user.Id);
            if (existUser != null)
            {
                userRepository.Update(user);

                return user;
            }
            return null;
        }

    }
}

