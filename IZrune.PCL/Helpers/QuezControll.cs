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


        public async Task<bool> IsExamActivated()
        {
            var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(Enum.QuezCategory.QuezExam);

            return !Result.Any(i => i.ExamDate.Year == DateTime.Now.Year&&i.ExamDate.DayOfYear==DateTime.Now.DayOfYear);

        }

        public async Task<TimeSpan> GetExamDate()
        {
           return await MpdcContainer.Instance.Get<IQuezServices>().GetExamDate(Enum.QuezCategory.QuezExam);
        }


        public void AddQuestion(int QuestionId,int AnswerId,int Duration)
        {
           
            QuezQuestion quez = new QuezQuestion() {AnswerId=AnswerId,Duration=Duration,QuestionId=QuestionId };
            
            MpdcContainer.Instance.Get<IQuezServices>().GetQuezResultAsync(quez);

        }


        

    }

   
   

}
