using System;
using Business.Services;

namespace BankSimulation.Controller
{
	public class BankController
	{
		private readonly BankService bankService;
        public BankController()
		{
			bankService = new BankService();
		}
		
		
	}
	enum BankChoice
	{
		CreateBank,
		DeleteBank,
		UpdateBank,
		GetAllBank,
		GetBankById,
		GetBankByName,
	}
}

