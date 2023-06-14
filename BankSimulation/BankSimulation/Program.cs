using System;
using BankSimulation.Controller;
using Utilities;

namespace BankSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //4169 7388 -kapital
            //4098 5844 -leo bank
            //4127 2081--abb
            //5103 0714 kapital
            //4169 8000 -rabite
            //4185 4927-pasha
            /*
             ilk olaraq her hansisa banklar yaradiriq ve onlari datada saxliyiriq banklar kapital,pasa,Abb,Rabite,leo ola bilir ve her bir
            bankin mehsulunun ilk 8 reqemi sahib olduqu banki temsil edir isdifade cart sifaris verdikde yeni yaratdiqda 16 reqemli kod 8 i mexsus
            olduqu banka uygun olaraq random verilir cvv ve activity datede hemcinin isdifadeci kart uze muxtelif emeliyatlar ede bilir
            balansi artira bilir oz kart hesabindan bsaqa kart hesabina pul elave ede bilir cash in cash out prosesleri  ve s ferqli karta odenis
            edilse 1.5 azn komisiya cixacaq basqa valyutadan odenis edilse 2 azn xidmet haqqi tutulur usd,eur,azn mumkundu valyuta olaraq
             
             */
            Helper.SetMessageAndColor("Welcome", ConsoleColor.Yellow);
            BankController bankController = new BankController();
            UserController userController = new UserController();
            while (true)
            {
                Helper.SetMessageAndColor(@"
                                            |*|--*---*---*---*---*---*---*---*--|*|-*---*---*---*---*---*---*---*---*---*----|*|
                                            |*|     Bank Options                |*|           User Options                   |*|
                                            |*|--*---*---*---*---*--*---*---*---|*|--*---*---*---*---*---*---*---*---*---*---|*|
                                            |*|   1-Create Bank                 |*|            11-CreateUser                 |*|
                                            |*|   2-Delete Bank                 |*|            12-DeleteUser                 |*|
                                            |*|   3-Update Bank                 |*|            13-UpdateUser                 |*|
                                            |*|   4-GetAllBank                  |*|            14-GetAllUser                 |*|
                                            |*|   5-GetBankById                 |*|            15-GetUserById                |*|    
                                            |*|   6-GetBankByName               |*|            16-GetUserByName              |*|
                                            |*|   7-GetAllMemberByName          |*|            17-CashIn                     |*|
                                            |*|   8-GetAllBanksAndMembersAdmin  |*|            18-CachOut                    |*|
                                            |*|   9-GetAllMembersAdmin          |*|            19-SendMoneyToUser            |*|
                                            |*|   10-update admin infos         |*|            20-GetUserByCartNumbers       |*|
                                            |*|   0-Exit                        |*|            21-PinOpenBlock               |*|
                                            |*|   -----------                   |*|            22-Get your personal account  |*|
                                            |*|---*---*---*---*---*---*---*---*-|*|---*---*---*---*---*---*---*---*---*---*--|*|
                                            |*|--*---*---*---*---*---*---*---*--|*|-*---*---*---*---*---*---*---*---*---*---*|*|
                                             ", ConsoleColor.Blue);
                Helper.SetMessageAndColor("you can choose these options", ConsoleColor.Blue);
                Select: string stringSelection = Console.ReadLine();
                int selection;
                bool isSelected = int.TryParse(stringSelection, out selection);
                if (isSelected)
                {
                    if (selection==0)
                    {
                        Helper.SetMessageAndColor("proses is finshed", ConsoleColor.Blue);
                        break;
                    }
                    if (selection > 0 && selection < 11)
                    {


                        switch (selection)
                        {
                            case (int)BankChoice.CreateBank:
                                bankController.Create();
                                break;
                            case (int)BankChoice.DeleteBank:
                                bankController.Delete();
                                break;
                            case (int)BankChoice.UpdateBank:
                                //update
                                break;
                            case (int)BankChoice.GetAllBank:
                                bankController.GetAll();
                                break;
                            case (int)BankChoice.GetBankById:
                                bankController.GetById();
                                break;
                            case (int)BankChoice.GetBankByName:
                                bankController.GetByName();
                                break;
                            case (int)BankChoice.GetAllMemberByName:
                                //get all members by bank name
                                break;
                            case (int)BankChoice.GetAllBanksAndMembersAdmin:
                                //get all banks and members by admin
                                break;
                            case (int)BankChoice.GetAllMembersAdmin:
                                //get all members Admin
                                break;
                            default:
                                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                                break;
                        }
                    }
                    else if (selection>10&&selection<24)
                    {
                        switch (selection)
                        {
                            case (int)UserChoice.CreateUser:
                                userController.Create();
                                break;
                            case (int)UserChoice.DeleteUser:
                                userController.Delete();
                                break;
                            case (int)UserChoice.UpdateUser:
                                //update user
                                break;
                            case (int)UserChoice.GetAllUser:
                                userController.GetAll();
                                break;
                            case (int)UserChoice.GetUserById:
                                userController.GetById();
                                break;
                            case (int)UserChoice.GetUserByName:
                                userController.GetByName();
                                break;
                            case (int)UserChoice.CashIn:
                                userController.CashIn();
                                break;
                            case (int)UserChoice.CachOut:
                                userController.CashOut();
                                break;
                            case (int)UserChoice.SendMoneyToUser:
                                userController.SendMoneyToUser();
                                break;
                            case (int)UserChoice.GetUserByCartNumbers:
                                userController.GetUserByCartNumbers();
                                break;
                            case (int)UserChoice.PinOpenBlock:
                                userController.PinOpenBlock();
                                break;
                            case (int)UserChoice.GetAccount:
                                userController.GetMyAccount();
                                break;
                            default:
                                Helper.SetMessageAndColor("something went wrong:", ConsoleColor.Red);
                                break;
                        }
                    }
                    else
                    {
                        Helper.SetMessageAndColor("enter correct option:", ConsoleColor.Red);
                    }
                }
                else
                {
                    Helper.SetMessageAndColor("something went wrong", ConsoleColor.Red);
                    Helper.SetMessageAndColor("enter correct number:", ConsoleColor.Blue);
                    goto Select;
                }
            }
        }
    }
}

