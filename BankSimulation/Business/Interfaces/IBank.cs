using System;
using System.Collections.Generic;
using Entities.Models;

namespace Business.Interfaces
{
	public interface IBank
	{
		public Bank Create(Bank bank);
		public Bank Delete(int id);
		public Bank Update(int id);
		public Bank GetById(int id);
		public Bank GetByName(string name);
		public List<Bank> GetAll();
	}
}

