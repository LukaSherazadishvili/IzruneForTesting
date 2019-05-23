using IZrune.PCL.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Timers;
using MpdcContainer = ServiceContainer.ServiceContainer;
using System.Threading;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Implementation.Models;

namespace IZrune.PCL.Helpers
{
   public class QuezControll
    {
        private static QuezControll instance = null;
        private static readonly object padlock = new object();

        QuezControll()
        {
        }

        public static QuezControll Instance {
            get {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            AppCore.Instance.InitServices();
                            instance = new QuezControll();
                        }
                    }
                }
                return instance;
            }
        }

        private static System.Timers.Timer Tmer;

        public async Task<bool> IsExamActivated(int studentID)
        {
            var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(studentID,Enum.QuezCategory.QuezExam);

            return !Result.Any(i => i.ExamDate.Year == DateTime.Now.Year&&i.ExamDate.DayOfYear==DateTime.Now.DayOfYear);

        }
        private ITestControler controller=new TestController();

        public void AddQuestion(int QuestionId,int AnswerId)
        {
            controller.Questions?.ToList().Add(new QuezQuestion() { AnswerId = AnswerId, QuestionId = QuestionId });


        }


        int FullTimeInSecond = 0;

        bool EndTime = true;

        public void StartQuezTime()
        {

            Task.Run( async() =>
            {
                while (EndTime)
                {
                    FullTimeInSecond++;
                    await Task.Delay(1000);
                }

            });
            

        }

        public void EndQuezTime()
        {
            EndTime = false;
            if (controller?.Questions?.Count() == 20 && controller.Duration > 0)
            {
                MpdcContainer.Instance.Get<IQuezServices>().GetQuezResultAsync(controller);

            }
            FullTimeInSecond = 0;
        }

       


    }

   
   

}
