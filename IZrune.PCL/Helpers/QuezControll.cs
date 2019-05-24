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
using IZrune.PCL.Enum;

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

        List<IQuestion> Questions;
        bool EndTime;
        int TimeInSecond ;


        public IQuestion GetCurrentQuestion(int position)
        {
            EndTime = true;
            TimeInSecond = 0;
            Task.Run(async() =>
            {
                while (EndTime)
                {
                    TimeInSecond++;
                    await Task.Delay(1000);
                }
                
            });

           return Questions.ElementAt(position);
        }

        public async Task<IEnumerable<IQuestion>> GetAllQuestion(QuezCategory TestType)
        {
            var Result =await MpdcContainer.Instance.Get<IQuezServices>().GetQuestionsAsync(TestType);
            Questions = Result.ToList();

            return Result;
        }


        public async Task<TimeSpan> GetExamDate(Enum.QuezCategory categor)
        {
           return await MpdcContainer.Instance.Get<IQuezServices>().GetExamDate(categor);
        }

        private int Count = 0;
        public async Task AddQuestion(int QuestionId,int AnswerId)
        {
            Count++;
            EndTime = false;
            QuezQuestion quez = new QuezQuestion() {AnswerId=AnswerId,Duration= TimeInSecond, QuestionId=QuestionId };
            
          await  MpdcContainer.Instance.Get<IQuezServices>().GetQuezResultAsync(quez);
            
            if (Count == 20)
            {
                await MpdcContainer.Instance.Get<IQuezServices>().GetDiploma();
               
            }
        }


        

    }

   
   

}
