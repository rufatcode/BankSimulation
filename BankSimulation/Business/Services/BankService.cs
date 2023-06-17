using System;
using System.Collections.Generic;
using Business.Interfaces;
using DataContext.Repository;
using Entities.Models;

namespace Business.Services
{
	public class BankService:IBank
	{
        public static int _count = 1;
        private readonly BankRepository bankRepository;
        private readonly UserRepository userRepository;
        public BankService()
		{
            bankRepository = new BankRepository();
            userRepository = new UserRepository();
		}

        public Bank Create(Bank bank)
        {
            try
            {
                bank.Id = _count;
                var existBank = bankRepository.Get(x => x.Id == bank.Id);
                if (existBank == null)
                {
                    bankRepository.Create(bank);
                    _count++;
                    return bank;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public Bank Delete(int id)
        {
            try
            {
                var bank = bankRepository.Get(x => x.Id == id);
                if (bank == null)
                {
                    return null;
                }
                var users = userRepository.GetAll(x => x.Bank.Name == bank.Name);
                foreach (var item in users)
                {
                    userRepository.Delete(item);
                }
                bankRepository.Delete(bank);
                return bank;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public List<Bank> GetAll()
        {
            try
            {
                if (bankRepository.GetAll().Count == 0)
                {
                    return null;
                }
                return bankRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public List<Bank> GetAllBanksAndMembersAdmin()
        {
            try
            {
                var banks = GetAll();
                if (banks==null)
                {
                    return null;
                }
                return banks;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public List<User> GetAllMemberByName(string bankName)
        {
            try
            {
                var bank = GetByName(bankName);
                if (bank == null)
                {
                    return null;
                }
                return bank.Users;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public List<Bank> GetAllMembersAdmin()
        {
            try
            {
                var banks = GetAll();
                if (banks==null)
                {
                    return null;
                }
                return banks;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public List<Bank> GetAllDeletingByAdmin()
        {
            try
            {
                if (bankRepository.GetAllDeleting().Count == 0)
                {
                    return null;
                }
                return bankRepository.GetAllDeleting();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Bank GetById(int id)
        {
            try
            {
                var bank = bankRepository.Get(x => x.Id == id);
                if (bank == null)
                {
                    return null;
                }
                return bank;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public Bank GetByName(string name)
        {
            try
            {
                var bank = bankRepository.Get(x => x.Name.ToLower() == name.ToLower());
                if (bank == null)
                {
                    return null;
                }
                return bank;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public Bank Update(Bank bank)
        {
            var existBank = bankRepository.Get(x => x.Id == bank.Id);
            if (existBank != null)
            {
                bankRepository.Update(bank);

                return bank;
            }
            return null;

        }
        
    }
}

