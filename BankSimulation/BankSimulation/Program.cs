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
            while (true)
            {
                Helper.SetMessageAndColor(@"
                                            |*|--*---*---*---*---*---*---*---*--|*|-*---*---*---*---*---*---*---*---*---*----|*|
                                            |*|     Bank Options                |*|           User Options                   |*|
                                            |*|--*---*---*---*---*--*---*---*---|*|--*---*---*---*---*---*---*---*---*---*---|*|
                                            |*|   1-Create Bank                 |*|            10-CreateUser                 |*|
                                            |*|   2-Delete Bank                 |*|            11-DeleteUser                 |*|
                                            |*|   3-Update Bank                 |*|            12-UpdateUser                 |*|
                                            |*|   4-GetAllBank                  |*|            13-GetAllUser                 |*|
                                            |*|   5-GetBankById                 |*|            14-GetUserById                |*|    
                                            |*|   6-GetBankByName               |*|            15-GetUserByName              |*|
                                            |*|   7-GetAllMemberByName          |*|            16-CashIn                     |*|
                                            |*|   8-GetAllBanksAndMembersAdmin  |*|            17-CachOut                    |*|
                                            |*|   9-GetAllMembersAdmin          |*|            18-SendMoneyToUser            |*|
                                            |*|   0-exit                        |*|            19-GetUserByCartNumbers       |*|
                                            |*|   -----------                   |*|            20-PinOpenBlock               |*|
                                            |*|   -----------                   |*|            21-Deposite                   |*|
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
                    if (selection > 0 && selection < 10)
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
                                break;
                        }
                    }
                    else if (selection>9&&selection<19)
                    {
                        switch (selection)
                        {
                            case (int)UserChoice.CreateUser:
                                //create user
                                break;
                            case (int)UserChoice.DeleteUser:
                                //delete user
                                break;
                            case (int)UserChoice.UpdateUser:
                                //update user
                                break;
                            case (int)UserChoice.GetAllUser:
                                //get all user
                                break;
                            case (int)UserChoice.GetUserById:
                                //get user by name
                                break;
                            case (int)UserChoice.GetUserByName:
                                //get user by name
                                break;
                            case (int)UserChoice.CashIn:
                                //cash in
                                break;
                            case (int)UserChoice.CachOut:
                                //cash out
                                break;
                            case (int)UserChoice.SendMoneyToUser:
                                //cart to cart proses
                                break;
                            case (int)UserChoice.GetUserByCartNumbers:
                                //get user by 16 digits cart numbers
                                break;
                            default:
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

