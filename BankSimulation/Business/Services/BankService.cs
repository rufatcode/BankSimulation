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
        public BankService()
		{
            bankRepository = new BankRepository();
		}

        public Bank Create(Bank bank)
        {
            bank.Id = _count;
            var existBank= bankRepository.Get(x => x.Id == bank.Id);
            if (existBank==null)
            {
                bankRepository.Create(bank);
                _count++;
                return bank;
            }
            return null;
        }

        public Bank Delete(int id)
        {
            var bank = bankRepository.Get(x => x.Id == id);
            if (bank==null)
            {
                return null;
            }
            bankRepository.Delete(bank);
            return bank;
        }

        public List<Bank> GetAll()
        {
            return bankRepository.GetAll();
        }

        public List<Bank> GetAllBanksAndMembersAdmin()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllMemberByName(string bankName)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllMembersAdmin()
        {
            throw new NotImplementedException();
        }

        public Bank GetById(int id)
        {
            return bankRepository.Get(x => x.Id == id);
        }

        public Bank GetByName(string name)
        {
            return bankRepository.Get(x => x.Name.ToLower() == name.ToLower());
        }

        public Bank Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}

