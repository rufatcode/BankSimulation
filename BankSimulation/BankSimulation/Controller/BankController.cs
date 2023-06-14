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
			if (banks!=null)
			{
                foreach (var item in banks)
                {
                    if (bankSignature == item.Signature)
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
			if (banks==null)
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
        CheckId: Helper.SetMessageAndColor("enter id for show same company informations:", ConsoleColor.Blue);
			 string stringId = Console.ReadLine();
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
		public void GetAllBanksAndMembersAdmin()
		{
			CheckAdmin: Helper.SetMessageAndColor("Enter Admin User Name", ConsoleColor.Blue);
			string adminUserName = Console.ReadLine();
			Helper.SetMessageAndColor("enter admin password:", ConsoleColor.Blue);
			string adminPassword = Console.ReadLine();
			if (adminPassword!=Helper.Password||adminUserName!=Helper.User)
			{
				Helper.SetMessageAndColor("admin username or password is incorrect:", ConsoleColor.Red);
				goto CheckAdmin;
			}
			var banks = bankService.GetAllBanksAndMembersAdmin();
			if (banks==null)
			{
				Helper.SetMessageAndColor("There is not avaliable bank", ConsoleColor.Red);
				return;
			}
			foreach (var item in banks)
			{
				Helper.SetMessageAndColor($"\n{item.Id} {item.Name} {item.Signature} {item.OriginHistiry}", ConsoleColor.Yellow);
				if (item.Users.Count>0)
				{
                    foreach (var user in item.Users)
                    {
                        Helper.SetMessageAndColor($"\nId:{user.Id} {user.Name} {user.SureName}\n{user.cartNumbers} {user.ActivityDate} Cvv:{user.Cvv} Pin:{user.Pin}\nPhone:{user.Phone}", ConsoleColor.Green);
                    }
                    Console.WriteLine("\n");
                }
				else
				{
					Helper.SetMessageAndColor("There are no active users", ConsoleColor.Green);
				}
				
			}
		}
		public void GetAllMemberByName()
		{
			CheckName: Helper.SetMessageAndColor("enter bank Name for see all members:", ConsoleColor.Blue);
			string bankName = Console.ReadLine();
			var users = bankService.GetAllMemberByName(bankName);
			if (users==null)
			{
				Helper.SetMessageAndColor("bank not found", ConsoleColor.Red);
				goto CheckName;
			}
			else if (users.Count==0)
			{
				Helper.SetMessageAndColor("There are no active users:", ConsoleColor.Red);
				return;
			}
			foreach (var item in users)
			{
				Helper.SetMessageAndColor($"\n{item.Id} {item.Bank.Name} Bank Company: {item.Name} {item.SureName} {item.Phone}\n", ConsoleColor.Cyan);
			}
		}
		public void GetAllMembersAdmin()
		{
			CheckAdmin: Helper.SetMessageAndColor("enter admin user name:", ConsoleColor.Blue);
			string adminUserName = Console.ReadLine();
			Helper.SetMessageAndColor("enter admin upaswword:", ConsoleColor.Blue);
			string adminPassword = Console.ReadLine();
			if (adminUserName!=Helper.User||adminPassword!=Helper.Password)
			{
				Helper.SetMessageAndColor("admin user name or password is incorrect:", ConsoleColor.Red);
				goto CheckAdmin;
			}
			var banks = bankService.GetAllMembersAdmin();
			if (banks==null)
			{
				Helper.SetMessageAndColor("bank not found", ConsoleColor.Red);
				return;
			}
			foreach (var item in banks)
			{
				if (item.Users.Count>0)
				{
					foreach (var user in item.Users)
					{
						if (user.PinBlocked == true)
						{
							Helper.SetMessageAndColor($"\nDeactive: Id:{user.Id}  {user.Bank.Name} Bank Company {user.Name} {user.SureName}\n{user.cartNumbers} {user.ActivityDate} Cvv:{user.Cvv} Pin:{user.Pin}\nPhone:{user.Phone}\n", ConsoleColor.Cyan);
						}
						else
						{

							Helper.SetMessageAndColor($"\nActive: Id:{user.Id} {user.Bank.Name} Bank Company {user.Name} {user.SureName}\n{user.cartNumbers} {user.ActivityDate} Cvv:{user.Cvv} Pin:{user.Pin}\nPhone:{user.Phone}\n", ConsoleColor.Cyan);
						}
					}
				}
			}
		}
		public  void UpdateAdminPanel()
		{

			Helper.SetMessageAndColor(" Admin panel...", ConsoleColor.Green);
            CheckId: Helper.SetMessageAndColor("enter bank id:", ConsoleColor.Yellow);
            string stringId = Console.ReadLine();
            int id;
            bool isId = int.TryParse(stringId, out id);
            if (!isId)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckId;
            }
            var bank = bankService.GetById(id);

        CheckAdminOld: Helper.SetMessageAndColor("enter old username:", ConsoleColor.Blue);
            string userName = Console.ReadLine();
            Helper.SetMessageAndColor("enter old password:", ConsoleColor.Blue);
            string password = Console.ReadLine();
			if (userName!=bank.User||password!=bank.Password)
			{
				Helper.SetMessageAndColor("user name or password is incorrect:", ConsoleColor.Red);
				goto CheckAdminOld;
			}
        CheckAdminNew: Helper.SetMessageAndColor("enter new username:", ConsoleColor.Blue);
			string newUserName = Console.ReadLine();
            Helper.SetMessageAndColor("enter new password:", ConsoleColor.Blue);
			string newPassword = Console.ReadLine();
			if (newUserName.Length<7||newPassword.Length<7)
			{
				Helper.SetMessageAndColor("username and password must be at least 8 characters", ConsoleColor.Red);
				goto CheckAdminNew;
			}
			bank.User = userName;
			bank.Password = password;
			Helper.SetMessageAndColor("admin user name and password has been update", ConsoleColor.Cyan);
        }
		public void Update()
		{
			if (bankService.GetAll()==null)
			{
				Helper.SetMessageAndColor("no bank available", ConsoleColor.Red);
				return;
			}
			CheckId: Helper.SetMessageAndColor("enter bank id for update:", ConsoleColor.Blue);
			string stringId = Console.ReadLine();
			int id;
			if (!int.TryParse(stringId,out id))
			{
				Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
				goto CheckId;
			}
			var bank = bankService.GetById(id);
			if (bank==null)
			{
				Helper.SetMessageAndColor("bank not found:", ConsoleColor.Red);
				goto CheckId;
			}
        CheckName: Helper.SetMessageAndColor("enter new bank name:", ConsoleColor.Blue);
             string bankName = Console.ReadLine();
        Signature: Helper.SetMessageAndColor("enter bank signature Format:\"xxxx xxxx\"", ConsoleColor.Blue);
            string bankSignature = Console.ReadLine();

            var banks = bankService.GetAll();
            Regex regex = new Regex(bankName.ToLower());
        
            if (banks != null)
            {
                foreach (var item in banks)
                {
                    if (bankSignature == item.Signature&&bankSignature!=bank.Signature)
                    {
                        Helper.SetMessageAndColor($"this siganure was belog to {item.Name} company", ConsoleColor.Red);
                        Helper.SetMessageAndColor("please choose unique signature:", ConsoleColor.Blue);
                        goto Signature;
                    }
                    else if (regex.IsMatch(item.Name.ToLower())&&bankName!=bank.Name)
                    {
                        Helper.SetMessageAndColor($"this Name was belog to {item.Name} company", ConsoleColor.Red);
                        Helper.SetMessageAndColor("please choose unique signature:", ConsoleColor.Blue);
                        goto CheckName;
                    }
                }
            }

            if (bankSignature.Length != 9 || bankSignature[4] != ' ')
            {
                Helper.SetMessageAndColor("some thing went wrong correct format \"xxxx xxxx\"", ConsoleColor.Red);
                goto Signature;
            }
            for (int i = 0; i < 9; i++)
            {
                if (i != 4)
                {
                    if (!Char.IsDigit(bankSignature[i]))
                    {
                        Helper.SetMessageAndColor("something went wrong correct format \"xxxx xxxx\":", ConsoleColor.Red);
                        goto Signature;
                    }
                }
            }
			bank.Name = bankName;
			bank.Signature = bankSignature;
			foreach (var item in bank.Users)
			{
				string newCartNumber = "";
				for (int i = 10; i <= 18; i++)
				{
					newCartNumber += item.cartNumbers[i];
				}
				item.cartNumbers = bank.Signature+" "+ newCartNumber;
				item.Bank = bank;
			}
			var newBank=bankService.Update(bank);
			if (newBank==null)
			{
                Helper.SetMessageAndColor("bank not found:", ConsoleColor.Red);
				return;
            }
			Helper.SetMessageAndColor($"{bank.Name} bank company has done update:", ConsoleColor.Cyan);
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

