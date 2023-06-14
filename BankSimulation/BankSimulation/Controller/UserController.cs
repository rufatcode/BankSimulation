using System;
using System.Text.RegularExpressions;
using Business.Services;
using DataContext.Repository;
using Entities.Models;
using Utilities;

namespace BankSimulation.Controller
{
    enum Currency
    {
        AZN=1,
        EUR,
        USD,
        TRY
    }

    public class UserController
	{
        private readonly UserService userService;
        private readonly BankController bankController;
        private readonly BankService bankService;
        private readonly UserRepository userRepository;
        private static int pinAttemp { get; set; } = 0;
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
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower());
            if (user==null)
            {
                Helper.SetMessageAndColor("invalid Name or Surename", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
            CheckPin: string pin = Console.ReadLine();
            if (user.Pin!=pin)
            {
                if (pinAttemp<3)
                {
                    pinAttemp++;
                    Helper.SetMessageAndColor("pin is not correct", ConsoleColor.Red);
                    goto CheckPin;
                }
                else if (pinAttemp==3)
                {
                    Helper.SetMessageAndColor("your cart account was blocked:", ConsoleColor.Red);
                    user.PinBlocked = true;
                }
            }
            pinAttemp = 0;
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
        public void GetById()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter id:", ConsoleColor.Blue);
            CheckId: string stringId = Console.ReadLine();
            int id;
            bool isId = int.TryParse(stringId, out id);
            if (!isId)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckId;
            }
            var user = userService.GetById(id);
            if (user==null)
            {
                Helper.SetMessageAndColor("user is not valid:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor($"{user.Id} {user.Name} {user.Bank.Name}", ConsoleColor.Cyan);
        }
        public void GetByName()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter Name:", ConsoleColor.Blue);
            string name = Console.ReadLine();
            var user = userService.GetByName(name);
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not valid:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor($"{user.Id} {user.Name} {user.Bank.Name}", ConsoleColor.Cyan);
        }
        public void CashIn()
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
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower());
            if (user == null)
            {
                Helper.SetMessageAndColor("invalid Name or Surename", ConsoleColor.Red);
                return;
            }
            else if (user.PinBlocked==true)
            {
                Helper.SetMessageAndColor("your cart account was blocked:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
        CheckPin: string pin = Console.ReadLine();
            if (user.Pin != pin)
            {
                if (pinAttemp < 3)
                {
                    pinAttemp++;
                    Helper.SetMessageAndColor("pin is not correct", ConsoleColor.Red);
                    goto CheckPin;
                }
                else if (pinAttemp == 3)
                {
                    Helper.SetMessageAndColor("your cart account is blocked:", ConsoleColor.Red);
                    user.PinBlocked = true;
                    return;
                }
            }
            pinAttemp = 0;
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            else if (user.PinBlocked==true)
            {
                Helper.SetMessageAndColor("Your pin code would be blocked",ConsoleColor.Red);
                Helper.SetMessageAndColor("It is recommended that you approach the service", ConsoleColor.Yellow);
                return;
            }
            Helper.SetMessageAndColor("enter ammount which you want to enter to card:", ConsoleColor.Blue);
            CheckAmmount: string stringAmmount = Console.ReadLine();
            double ammount;
            bool isAmmount = double.TryParse(stringAmmount, out ammount);
            if (!isAmmount)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckAmmount;
            }
            Helper.SetMessageAndColor("enter currency :format:\"1->AZN,2->EUR,3->USD,4->TRY\"", ConsoleColor.Blue);
            Helper.SetMessageAndColor("Service fee for other currencies is 2 Azn", ConsoleColor.Green);
            CheckCurrency: string stringChoice = Console.ReadLine();
            int choice;
            bool isChoice = int.TryParse(stringChoice, out choice);
            if (!isChoice)
            {
                Helper.SetMessageAndColor("something went wrong::", ConsoleColor.Red);
                goto CheckCurrency;
            }
            else if (choice>4||choice<0)
            {
                Helper.SetMessageAndColor("press 1 for AZN,press 2 for EUR,press 3 for USD,press 4 for TRY", ConsoleColor.Yellow);
                goto CheckCurrency;
            }
            switch (choice)
            {
                case (int)Currency.AZN:
                    user.Balance += ammount;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.EUR:
                    ammount += ammount*2-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.USD:
                    ammount += ammount*1.7-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.TRY:
                    ammount += ammount/20-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                default:
                    break;
            }
            userService.CashIn(user, ammount);

        }
        public void CashOut()
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
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower());
            if (user == null)
            {
                Helper.SetMessageAndColor("invalid Name or Surename", ConsoleColor.Red);
                return;
            }
            else if (user.PinBlocked == true)
            {
                Helper.SetMessageAndColor("your cart account was blocked:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
        CheckPin: string pin = Console.ReadLine();
            if (user.Pin != pin)
            {
                if (pinAttemp < 3)
                {
                    pinAttemp++;
                    Helper.SetMessageAndColor("pin is not correct", ConsoleColor.Red);
                    goto CheckPin;
                }
                else if (pinAttemp == 3)
                {
                    Helper.SetMessageAndColor("your cart account is blocked:", ConsoleColor.Red);
                    user.PinBlocked = true;
                    return;
                }
            }
            pinAttemp = 0;
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            else if (user.PinBlocked == true)
            {
                Helper.SetMessageAndColor("Your pin code would be blocked", ConsoleColor.Red);
                Helper.SetMessageAndColor("It is recommended that you approach the service", ConsoleColor.Yellow);
                return;
            }
            Helper.SetMessageAndColor("Enter the amount you want to withdraw", ConsoleColor.Blue);
            CheckAmmount: string stringAmmount = Console.ReadLine();
            double ammount;
            bool isAmmount = double.TryParse(stringAmmount, out ammount);
            if (!isAmmount)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckAmmount;
            }
            bool isCashOut= userService.CashOut(user, ammount);
            if (!isCashOut)
            {
                Helper.SetMessageAndColor($"your balance:{user.Balance} AZN", ConsoleColor.Red);
                Helper.SetMessageAndColor("enter correct ammount:", ConsoleColor.Yellow);
                goto CheckAmmount;
            }
            Helper.SetMessageAndColor($"{ammount}  has been withdrawn from your balance:", ConsoleColor.Cyan);
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

