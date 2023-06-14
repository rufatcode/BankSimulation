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
        public void Update()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("First name, last name and cvv must be entered to safely update the card account", ConsoleColor.Yellow);
            Helper.SetMessageAndColor("enter  Name for update your personal card account", ConsoleColor.Blue);
            string name = Console.ReadLine();
            Helper.SetMessageAndColor("enter Surename for update your personal card account", ConsoleColor.Blue);
            string sureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter personal Cvv for update your personal card account", ConsoleColor.Blue);
            string cvv = Console.ReadLine();
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower() && x.Cvv == cvv);
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            var bank = bankService.GetById(user.Id);
            Helper.SetMessageAndColor("enter new Name", ConsoleColor.Blue);
            string newName = Console.ReadLine();
            Helper.SetMessageAndColor("enter new  Sure Name", ConsoleColor.Blue);
            string newSureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter Pin code:format: \"xxxx\":", ConsoleColor.Blue);
        CheckPin: string pin = Console.ReadLine();
            if (pin.Length != 4)
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
        CheckPhone: Helper.SetMessageAndColor("enter Phone number \"+994xxxxxxxxx\" format ", ConsoleColor.Blue);
            string phoneNumber = Console.ReadLine();
            Regex regex = new Regex(@"^\+994(50|51|55|77|70|99)\d{7}$");
            if (!regex.IsMatch(phoneNumber))
            {
                Helper.SetMessageAndColor("please enter correct phone number:", ConsoleColor.Red);
                goto CheckPhone;
            }
            user.Name = newName;
            user.SureName = sureName;
            user.Phone = phoneNumber;
            user.Pin = pin;
            userService.Update(user);
            Helper.SetMessageAndColor($"user  has  updated new infos:{user.Id} {user.Name} {user.SureName} {user.Pin} {user.Phone}", ConsoleColor.Cyan);
            for (int i = 0; i < bank.Users.Count; i++)
            {
                if (user.Id == bank.Users[i].Id)
                {
                    bank.Users[i] = user;
                }
            }
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
            Helper.SetMessageAndColor("First name, last name and pin code must be entered for customers safelty:", ConsoleColor.Yellow);
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
            CheckPin: Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
            string pin = Console.ReadLine();
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
                    return;
                }
            }
            pinAttemp = 0;
            if (user == null)
            {
                Helper.SetMessageAndColor("user is not belong to you:", ConsoleColor.Red);
                return;
            }
            if (!user.PinBlocked)
            {
                Helper.SetMessageAndColor($"\n{user.Bank.Name} Bank cart:\n Account is Active\n{user.Name} {user.SureName}\nBalance:{user.Balance}\n{user.cartNumbers}\nPin:{user.Pin} Cvv:{user.Cvv}\n{user.ActivityDate}\nPhone Number:{user.Phone}", ConsoleColor.Yellow);
                return;
            }
            Helper.SetMessageAndColor($"\n{user.Bank.Name} bank cart\nAccount is  Deactive\n {user.Name} {user.SureName}\nBalance:{user.Balance}\n{user.cartNumbers}\nPin:{user.Pin} Cvv:{user.Cvv}\n{user.ActivityDate}\nPhone Number:{user.Phone}", ConsoleColor.Yellow);
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
            Helper.SetMessageAndColor("First name, last name and pin code must be entered for customers safelty:", ConsoleColor.Yellow);
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
            else if (user.PinBlocked)
            {
                Helper.SetMessageAndColor("your cart account was blocked:", ConsoleColor.Red);
                return;
            }
        CheckPin:  Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
             string pin = Console.ReadLine();
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
            else if (user.PinBlocked)
            {
                Helper.SetMessageAndColor("Your pin code would be blocked",ConsoleColor.Red);
                Helper.SetMessageAndColor("It is recommended that you approach the service", ConsoleColor.Yellow);
                return;
            }
        CheckAmmount: Helper.SetMessageAndColor("enter ammount which you want to enter to card:", ConsoleColor.Blue);
            string stringAmmount = Console.ReadLine();
            double ammount;
            bool isAmmount = double.TryParse(stringAmmount, out ammount);
            if (!isAmmount)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckAmmount;
            }
        CheckCurrency: Helper.SetMessageAndColor("enter currency :format:\"1->AZN,2->EUR,3->USD,4->TRY\"", ConsoleColor.Blue);
            Helper.SetMessageAndColor("Service fee for other currencies is 2 Azn", ConsoleColor.Green);
             string stringChoice = Console.ReadLine();
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
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.EUR:
                    ammount = ammount*2-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.USD:
                    ammount = ammount*1.7-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                case (int)Currency.TRY:
                    ammount = ammount/20-2;
                    Helper.SetMessageAndColor($"{ammount} AZN added your balance:", ConsoleColor.Cyan);
                    break;
                default:
                    break;
            }
            bool isSended= userService.CashIn(user, ammount);
            if (!isSended)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                return;
            }

        }
        public void CashOut()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("First name, last name and pin code must be entered for customers safelty:", ConsoleColor.Yellow);
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
            else if (user.PinBlocked)
            {
                Helper.SetMessageAndColor("your cart account was blocked:", ConsoleColor.Red);
                return;
            }
        CheckPin: Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
             string pin = Console.ReadLine();
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
            else if (user.PinBlocked)
            {
                Helper.SetMessageAndColor("Your pin code would be blocked", ConsoleColor.Red);
                Helper.SetMessageAndColor("It is recommended that you approach the service", ConsoleColor.Yellow);
                return;
            }
            else if (user.Balance==0)
            {
                Helper.SetMessageAndColor($"your balance:{user.Balance} AZN", ConsoleColor.Yellow);
                return;
            }
        CheckAmmount: Helper.SetMessageAndColor("Enter the amount you want to withdraw", ConsoleColor.Blue);
             string stringAmmount = Console.ReadLine();
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
        public void GetUserByCartNumbers()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("enter user 16 digits cart number:Format:\"xxxx xxxx xxxx xxxx \" ", ConsoleColor.Blue);
            string cartNumbers = Console.ReadLine();
            var user = userService.GetUserByCartNumbers(cartNumbers);
            if (user == null)
            {
                Helper.SetMessageAndColor("user not fount:", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor($"{user.Name} {user.Bank.Name} \n{user.cartNumbers}", ConsoleColor.Cyan);
        }
        public void SendMoneyToUser()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("First name, last name and pin code must be entered for customers safelty:", ConsoleColor.Yellow);
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
            else if (user.PinBlocked )
            {
                Helper.SetMessageAndColor("your card account was blocked:", ConsoleColor.Red);
                return;
            }
        CheckPin: Helper.SetMessageAndColor("enter personal Pin ", ConsoleColor.Blue);
            string pin = Console.ReadLine();
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
                    Helper.SetMessageAndColor("your card account is blocked:", ConsoleColor.Red);
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
            else if (user.PinBlocked)
            {
                Helper.SetMessageAndColor("Your pin code would be blocked", ConsoleColor.Red);
                Helper.SetMessageAndColor("It is recommended that you approach the service", ConsoleColor.Yellow);
                return;
            }
            else if (user.Balance==0)
            {
                Helper.SetMessageAndColor($"your balance:{user.Balance} AZN:", ConsoleColor.Yellow);
                return;
            }
            Helper.SetMessageAndColor("Enter the 16-digit card number of the user you want to send money to:", ConsoleColor.Blue);
            Helper.SetMessageAndColor("correct 16 digits card number Format:\"xxxx xxxx xxxx xxxx \" ", ConsoleColor.Yellow);
            CheckCard: string cardNumber = Console.ReadLine();
            var toUser= userService.GetUserByCartNumbers(cardNumber);
            if (toUser==null)
            {
                Helper.SetMessageAndColor("user not found:", ConsoleColor.Red);
                Helper.SetMessageAndColor("press 1 for continue:", ConsoleColor.Green);
                Helper.SetMessageAndColor("press 0 for finshed", ConsoleColor.Green);
                CheckChoince: string stringChoice = Console.ReadLine();
                int choice;
                if (!int.TryParse(stringChoice,out choice)||choice!=0&&choice!=1)
                {
                    Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                    goto CheckChoince;
                }
                else if (choice==1)
                {
                    Helper.SetMessageAndColor("enter correct cart numbers:", ConsoleColor.Red);
                    goto CheckCard;
                    
                }
                Helper.SetMessageAndColor("proses has finshed:", ConsoleColor.Cyan);
                return;
            }
            else if (toUser.PinBlocked)
            {
                Helper.SetMessageAndColor("user card account had been blocked:", ConsoleColor.Red);
                return;
            }
            else if (toUser==user)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                return;
            }
        CheckAmmount: Helper.SetMessageAndColor($"enter ammount for send to {toUser.cartNumbers}", ConsoleColor.Green);
            string stringAmmount = Console.ReadLine();
            double ammount;
            if (!double.TryParse(stringAmmount,out ammount))
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckAmmount;
            }
            else if (user.Bank.Signature!=toUser.Bank.Signature)
            {
                Helper.SetMessageAndColor("service fee 1 azn", ConsoleColor.Yellow);
                ammount -= 1;
            }
            bool isSended=userService.SendMoneyToUser(user, toUser, ammount);
            if (isSended)
            {
                Helper.SetMessageAndColor($"{ammount} AZN has sended to {toUser.Name} {toUser.SureName}", ConsoleColor.Cyan);
                return;
            }
            Helper.SetMessageAndColor($"your balance is less then {ammount}", ConsoleColor.Red);
        }
        public void PinOpenBlock()
        {
            if (userService.GetAll().Count == 0)
            {
                Helper.SetMessageAndColor("No existing cart account", ConsoleColor.Red);
                return;
            }
            Helper.SetMessageAndColor("Only the admin can update the user's pin code.", ConsoleColor.Green);
            CheckUserName: Helper.SetMessageAndColor("enter admin user name:", ConsoleColor.Yellow);
            string adminUserName = Console.ReadLine();
            Helper.SetMessageAndColor("enter admin password:", ConsoleColor.Yellow);
            string adminPassword = Console.ReadLine();
            if (adminUserName!=Helper.User||adminPassword!=Helper.Password)
            {
                Helper.SetMessageAndColor("admin username or password is incorrect:", ConsoleColor.Red);
                goto CheckUserName;
            }
           
            Helper.SetMessageAndColor("hi welcome our service please enter your  Name:", ConsoleColor.Blue);
            string name = Console.ReadLine();
            Helper.SetMessageAndColor("hi welcome our service please enter your  SureName:", ConsoleColor.Blue);
            string sureName = Console.ReadLine();
            Helper.SetMessageAndColor("enter Pin code:", ConsoleColor.Blue);
            string pin = Console.ReadLine();
            var user = userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.SureName.ToLower() == sureName.ToLower() && x.Pin == pin);
            if (user==null)
            {
                Helper.SetMessageAndColor("user is not found:", ConsoleColor.Red);
                return;
            }
            else if (!user.PinBlocked)
            {
                Helper.SetMessageAndColor("your pin code is not blocked", ConsoleColor.Yellow);
                return;
            }
            else if (user.Bank.Rates.Count == 0)
            {
                Helper.SetMessageAndColor($"users' evaluations out of 5 points:5", ConsoleColor.Green);
            }
            else
            {
                double mark = 0;
                foreach (var item in user.Bank.Rates)
                {
                    mark += item;
                }
                mark /= user.Bank.Rates.Count;
                Helper.SetMessageAndColor($"users' evaluations out of 5 points:{mark}", ConsoleColor.Green);
            }
            var bank = bankService.GetById(user.Bank.Id);
            Helper.SetMessageAndColor("only the admin of the bank to which the user belongs can update", ConsoleColor.Green);
        CheckBankUserName: Helper.SetMessageAndColor($"enter {bank.Name} admin user name:", ConsoleColor.Yellow);
            string adminBankUserName = Console.ReadLine();
            Helper.SetMessageAndColor($"enter {bank.Name} admin password:", ConsoleColor.Yellow);
            string adminBankPassword = Console.ReadLine();
            if (adminUserName != bank.User || adminPassword != bank.Password)
            {
                Helper.SetMessageAndColor("admin username or password is incorrect:", ConsoleColor.Red);
                goto CheckBankUserName;
            }
            Helper.SetMessageAndColor("enter new Pin code", ConsoleColor.Blue);
            CheckNewPin: string stringNewPin = Console.ReadLine();
            int newPin;
            if (!int.TryParse(stringNewPin,out newPin)||stringNewPin.Length!=4)
            {
                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                goto CheckNewPin;
            }
            userService.PinOpenBlock(user, stringNewPin);
            Helper.SetMessageAndColor($"your pin code has oppened block :new Pin code:{user.Pin}", ConsoleColor.Cyan);
            Helper.SetMessageAndColor("please Rate our service out of 5 points:", ConsoleColor.Blue);
            CheckRate: string stringRate = Console.ReadLine();
            int rate;
            if (!int.TryParse(stringRate,out rate)||rate<0||rate>5)
            {
                Helper.SetMessageAndColor("please enter correct reate:", ConsoleColor.Red);
                goto CheckRate;
            }
            user.Bank.Rates.Add(rate);
            
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
        GetAccount,
    }
}

