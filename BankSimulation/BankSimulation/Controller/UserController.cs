using System;
using System.Text.RegularExpressions;
using Business.Services;
using DataContext.Repository;
using Entities.Models;
using Utilities;

namespace BankSimulation.Controller
{
	public class UserController
	{
        private readonly UserService userService;
        private readonly BankController bankController;
        private readonly BankService bankService;
        private readonly UserRepository userRepository;
        public UserController()
		{
            userService = new UserService();
            bankController = new BankController();
            bankService = new BankService();
            userRepository = new UserRepository();
		}
        public void Create()
        {
            if (bankService.GetAll().Count==0)
            {
                Helper.SetMessageAndColor("no bank available", ConsoleColor.Red);
                return;
            }
            bankController.GetAll();
            Helper.SetMessageAndColor("these are exist bank Companies in our Service please choose one of them :", ConsoleColor.Blue);
            Helper.SetMessageAndColor("Enter your bank id to create a bank card for you:", ConsoleColor.Yellow);
            CheckId: string stringId = Console.ReadLine();
            int id;
            bool isId = int.TryParse(stringId, out id);
            if (!isId)
            {
                Helper.SetMessageAndColor("enter Bank id correct form:", ConsoleColor.Red);
                goto CheckId;
            }
            var bank = bankService.GetById(id);
            if (bank==null)
            {
                Helper.SetMessageAndColor("Banking Company does not exist:", ConsoleColor.Red);
                Helper.SetMessageAndColor("You must enter avaliable bank id:", ConsoleColor.Blue);
                goto CheckId;
            }
            Helper.SetMessageAndColor("enter Name", ConsoleColor.Blue);
            string name = Console.ReadLine();
            Helper.SetMessageAndColor("enter Sure Name", ConsoleColor.Blue);
            string sureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter Pin code:format: \"xxxx\":", ConsoleColor.Blue);
            CheckPin: string pin = Console.ReadLine();
            if (pin.Length!=4)
            {
                Helper.SetMessageAndColor("pin code must be 4 digits:", ConsoleColor.Red);
                goto CheckPin;
            }
            foreach (var item in pin)
            {
                if (!Char.IsDigit(item))
                {
                    Helper.SetMessageAndColor("pin must contain only digits", ConsoleColor.Red);
                    Helper.SetMessageAndColor("enter pin \"xxxx\" format", ConsoleColor.Yellow);
                    goto CheckPin;
                }
            }
            Helper.SetMessageAndColor("enter Phone number \"+994xxxxxxxxx\" format ", ConsoleColor.Blue);
            CheckPhone: string phoneNumber = Console.ReadLine();
            Regex regex = new Regex(@"^\+994(50|51|55|77|70|99)\d{7}$");
            if (!regex.IsMatch(phoneNumber))
            {
                Helper.SetMessageAndColor("please enter correct phone number:", ConsoleColor.Red);
                goto CheckPhone;
            }
            User user = new User();
            user.Bank = bank;
            user.Name = name;
            user.SureName = sureName;
            user.Pin = pin;
            user.Phone = phoneNumber;
            var newUser=userService.Create(user);
            if (newUser==null)
            {
                Helper.SetMessageAndColor("Something went wrong:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("cart succesfuuly created:", ConsoleColor.Cyan);
            Helper.SetMessageAndColor($"\n{newUser.Bank.Name} bank cart:\n{newUser.Name} {newUser.SureName}\nBalance:{newUser.Balance}\n{newUser.cartNumbers}\nPin:{newUser.Pin} Cvv:{newUser.Cvv}\n{newUser.ActivityDate}\n", ConsoleColor.Yellow);
        }
        public void Delete()
        {
            if (userService.GetAll().Count==0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("First name, last name and cvv must be entered to safely delete the cart account", ConsoleColor.Yellow);
            Helper.SetMessageAndColor("enter  Name for delete your personal cart account", ConsoleColor.Blue);
            string name = Console.ReadLine();
            Helper.SetMessageAndColor("enter Surename for delete your personal cart account", ConsoleColor.Blue);
            string sureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter personal Cvv for delete your personal cart account", ConsoleColor.Blue);
            string cvv = Console.ReadLine();
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower() && x.Cvv == cvv);
            if (user==null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            userService.Delete(user.Id);
            Helper.SetMessageAndColor($"{user.Name}'s account deleted:", ConsoleColor.Cyan);
        }
        public void GetAll()
        {
            var users = userService.GetAll();
            if (users.Count==0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            foreach (var item in users)
            {
                Helper.SetMessageAndColor($"{item.Id} {item.Name} {item.SureName}", ConsoleColor.Cyan);
            }
        }
        public void GetMyAccount()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("First name, last name and cvv must be entered for customers safelty:", ConsoleColor.Yellow);
            Helper.SetMessageAndColor("enter Name ", ConsoleColor.Blue);
            string name = Console.ReadLine();
            Helper.SetMessageAndColor("enter Surename ", ConsoleColor.Blue);
            string sureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
            string pin = Console.ReadLine();
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower() && x.Pin == pin);
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            if (user.PinBlocked==false)
            {
                Helper.SetMessageAndColor($"\n{user.Bank.Name} Bank cart:\n Account is Active\n{user.Name} {user.SureName}\nBalance:{user.Balance}\n{user.cartNumbers}\nPin:{user.Pin} Cvv:{user.Cvv}\n{user.ActivityDate}\nPhone Number:{user.Phone}", ConsoleColor.Yellow);
                return;
            }
            Helper.SetMessageAndColor($"\n{user.Bank.Name} bank cart\n Account is  Deactive\n {user.Name} {user.SureName}\nBalance:{user.Balance}\n{user.cartNumbers}\nPin:{user.Pin} Cvv:{user.Cvv}\n{user.ActivityDate}\nPhone Number:{user.Phone}", ConsoleColor.Yellow);
        }
        
	}
    enum UserChoice
    {
        CreateUser=11,
        DeleteUser,
        UpdateUser,
        GetAllUser,
        GetUserById,
        GetUserByName,
        CashIn,
        CachOut,
        SendMoneyToUser,
        GetUserByCartNumbers,
        PinOpenBlock,
        Deposite,
        GetAccount,
    }
}

