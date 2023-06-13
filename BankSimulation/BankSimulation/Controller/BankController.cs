using System;
using System.Text.RegularExpressions;
using Business.Interfaces;
using Business.Services;
using Entities.Models;
using Utilities;
namespace BankSimulation.Controller
{
	public class BankController
	{
		private readonly BankService bankService;
        public BankController()
		{
			bankService = new BankService();
		}
		public void Create()
		{
			Helper.SetMessageAndColor("enter Bank Name:", ConsoleColor.Blue);
            var banks = bankService.GetAll();
            CheckName: string bankName = Console.ReadLine();
			Regex regex = new Regex(bankName.ToLower());
			Helper.SetMessageAndColor("enter Bank signature \"xxxx xxxx\" format", ConsoleColor.Blue);
			Signature: string bankSignature = Console.ReadLine();
			
			foreach (var item in banks)
			{
				if (bankSignature==item.Signature)
				{
					Helper.SetMessageAndColor($"this siganure was belog to {item.Name} company", ConsoleColor.Red);
					Helper.SetMessageAndColor("please choose unique signature:", ConsoleColor.Blue);
					goto Signature;
				}
                else if (regex.IsMatch(item.Name.ToLower()))
                {
                    Helper.SetMessageAndColor($"this Name was belog to {item.Name} company", ConsoleColor.Red);
                    Helper.SetMessageAndColor("please choose unique signature:", ConsoleColor.Blue);
                    goto CheckName;
                }
            }
            if (bankSignature.Length != 9 || bankSignature[4]!=' ')
            {
                Helper.SetMessageAndColor("some thing went wrong correct format \"xxxx xxxx\"", ConsoleColor.Red);
                goto Signature;
            }
            for (int i = 0; i < 9; i++)
			{
				if (i!=4)
				{
					if (!Char.IsDigit(bankSignature[i]))
					{
						Helper.SetMessageAndColor("something went wrong correct format \"xxxx xxxx\":", ConsoleColor.Red);
						goto Signature;
					}
				}
			}
			Bank bank = new Bank();
			bank.Name = bankName;
			bank.Signature = bankSignature;
			var newBank= bankService.Create(bank);
			if (newBank==null)
			{
				Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
				return;
			}
			Helper.SetMessageAndColor($"{newBank.Name} succesfully created especially signature {newBank.Signature}", ConsoleColor.Cyan);
		}
		public void Delete()
		{
			Helper.SetMessageAndColor("enter id for delete bank company", ConsoleColor.Blue);
			CheckId: string stringId = Console.ReadLine();
			int id;
			bool isId = int.TryParse(stringId, out id);
			if (!isId)
			{
				Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
				goto CheckId;
			}
			var deletingBank= bankService.Delete(id);
            if (deletingBank == null)
            {
                Helper.SetMessageAndColor("this company is not valid", ConsoleColor.Red);
                return;
            }
			Helper.SetMessageAndColor($"{deletingBank.Name} company no longer available", ConsoleColor.Cyan);
        }
		public void GetAll()
		{
			var banks=bankService.GetAll();
			if (banks.Count==0)
			{
				Helper.SetMessageAndColor("there is not avaliable company", ConsoleColor.Red);
				return;
			}
			foreach (var item in banks)
			{
                Helper.SetMessageAndColor($"\nCompany Id:{item.Id} \nCompany Name:{item.Name}\nCOmpany Signature: {item.Signature} \nCreation date:{item.OriginHistiry}\n", ConsoleColor.Cyan);
            }
			
		}
		public void GetById()
		{
			Helper.SetMessageAndColor("enter id for show same company informations:", ConsoleColor.Blue);
			CheckId: string stringId = Console.ReadLine();
			int id;
			bool isId = int.TryParse(stringId, out id);
			if (!isId)
			{
				Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
				goto CheckId;
			}
			var bank = bankService.GetById(id);
			if (bank==null)
			{
				Helper.SetMessageAndColor("company is not valid:", ConsoleColor.Red);
				return;
			}
			Helper.SetMessageAndColor($"\nCompany  Id:{bank.Id}\nCompany Name:{bank.Name}\nCompany signature:{bank.Signature}\nCmpany was aveliable from  {bank.OriginHistiry}\n", ConsoleColor.Cyan);
		}
		public void GetByName()
		{
			Helper.SetMessageAndColor("enter Company name see company informations:", ConsoleColor.Blue);
			string bankName = Console.ReadLine();
			var bank = bankService.GetByName(bankName);
			if (bank==null)
			{
				Helper.SetMessageAndColor("Company is not valid", ConsoleColor.Red);
				return;
			}
            Helper.SetMessageAndColor($"\nCompany  Id:{bank.Id}\nCompany Name:{bank.Name}\nCompany signature:{bank.Signature}\nCmpany was aveliable from  {bank.OriginHistiry}\n", ConsoleColor.Cyan);
        }
		
	}
	enum BankChoice
	{
		CreateBank=1,
		DeleteBank,
		UpdateBank,
		GetAllBank,
		GetBankById,
		GetBankByName,
		GetAllMemberByName,
		GetAllBanksAndMembersAdmin,
		GetAllMembersAdmin,
		UpdateAdminProfile
	}
}

