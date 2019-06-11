using IZrune.PCL;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Services;
using System;
using System.Threading.Tasks;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace ConsoleApp2
{
    class Program
    {


        static  void Main(string[] args)
        {
            AppCore.Instance.InitServices();
            //var res = UserControl.Instance.LogInUser("irakli123", "123456789").Result;
            //UserControl.Instance.SeTSelectedStudent(1);
            //StatisticServices stt = new StatisticServices();
            // var rrrrr=stt.GetStudentStatisticsAsync(1).Result;

            //var rr = MpdcContainer.Instance.Get<IQuezServices>().GetQuestionsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest).Result;
           var res= QuezControll.Instance.GetExamDate(IZrune.PCL.Enum.QuezCategory.QuezTest).Result;
            var Res = MpdcContainer.Instance.Get<IPaymentService>().GetPaymentUrlsAsync(1, 2, 10).Result;
            Console.ReadKey();
        }
    }
}
