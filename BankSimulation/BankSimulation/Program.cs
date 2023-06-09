using System;
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
                                            1-Create Bank
                                            2-Delete Bank
                                            3-Update Bank
                                            4-GetAllBank
                                            5-GetBankById
                                            6-GetBankByName", ConsoleColor.Blue);
            }
        }
    }
}

