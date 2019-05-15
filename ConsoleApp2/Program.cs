using IZrune.PCL;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Services;
using System;

namespace ConsoleApp2
{
    class Program
    {



        static  void Main(string[] args)
        {
            LoginServices serv = new LoginServices();
            var result =  serv.LoginUser("irakli123", "123456789").Result;

            var Person = new UserServices();
            var rrr = Person.GetUserAsync().Result;
            var DS = Person.GetPromoCodeAsync().Result;

            QuezServices Quesserv = new QuezServices();
            var res= Quesserv.GetQuestionsAsync(1,0).Result;


            StatisticServices stt = new StatisticServices();
             var rrrrr=stt.GetStudentStatisticsAsync(1).Result;

            foreach(var items in rrrrr)
            {
                Console.WriteLine(items.ExamDate.ToShortDateString());
                Console.WriteLine(items.CorrectAnswersCount);
                Console.WriteLine(items.IncorrectAnswersCount);
                Console.WriteLine(items.Point);
                Console.WriteLine(items.SkippedQuestionsCount);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

           
            Console.ReadKey();
        }
    }
}
