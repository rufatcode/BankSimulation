﻿using System;
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
                                            |*|---*---*---*---*---*---*---*---*-|*|---*---*---*---*---*---*---*---*---*---*--|*|
                                            |*|--*---*---*---*---*---*---*---*--|*|-*---*---*---*---*---*---*---*---*---*---*|*|
                                             ", ConsoleColor.Blue);
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
                    switch (selection)
                    {
                        case (int)BankChoice.CreateBank:
                            //create bank
                            break;
                        case (int)BankChoice.DeleteBank:
                            //delete
                            break;
                        case (int)BankChoice.UpdateBank:
                            //update
                            break;
                        case (int)BankChoice.GetAllBank:
                            //get all bank
                            break;
                        case (int)BankChoice.GetBankById:
                            //get bank by id
                            break;
                        case (int)BankChoice.GetBankByName:
                            //get bank by name
                            break;
                        case (int)BankChoice.GetAllMemberByName:
                            //get all members
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Helper.SetMessageAndColor("something went wrong", ConsoleColor.Red);
                    goto Select;
                }
            }
        }
    }
}

