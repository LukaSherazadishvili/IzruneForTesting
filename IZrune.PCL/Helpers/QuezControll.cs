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
        public List<QuisSheduler> Sheduler;
        bool EndTime;
        int TimeInSecond ;

        private int Position = 0;
        public IQuestion GetCurrentQuestion()
        {
            try
            {



                EndTime = true;
                TimeInSecond = 0;
                if (Position < 20)
                {
                    Task.Run(async () =>
                    {
                        while (EndTime)
                        {
                            TimeInSecond++;
                            await Task.Delay(1000);
                        }

                    });

                    return Questions.ElementAt(Position);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IQuestion>> GetAllQuestion(QuezCategory TestType)
        {
            var Result =await MpdcContainer.Instance.Get<IQuezServices>().GetQuestionsAsync(TestType);
            Questions = Result.ToList();

            if (Questions.Count > 0&&Sheduler==null)
            {
                Sheduler = new List<QuisSheduler>();
                for(int i = 0; i < Questions?.Count(); i++)
                {
                    QuisSheduler sheduler = new QuisSheduler() { IsCurrent = false, AlreadeBe = false, Position = i };
                    Sheduler.Add(sheduler);
                }
                Sheduler.ElementAt(Position).IsCurrent = true;

            }
            return Result;
        }


        public async Task<TimeSpan> GetExamDate(Enum.QuezCategory categor)
        {
         
           return await MpdcContainer.Instance.Get<IQuezServices>().GetExamDate(categor);



        }

       
        public async Task AddQuestion(int AnswerId=0)
        {
            EndTime = false;
            QuezQuestion quez = new QuezQuestion() { AnswerId = AnswerId, Duration = TimeInSecond, QuestionId = Questions.ElementAt(Position).id };
            await MpdcContainer.Instance.Get<IQuezServices>().GetQuezResultAsync(quez);
            Position++;
            if (Position < 20)
            {
                if (Sheduler?.Count() > 0)
                {
                    foreach (var item in Sheduler)
                    {
                        item.IsCurrent = false;
                    }
                    Sheduler.ElementAt(Position).IsCurrent = true;
                    if (Position != 0)
                        Sheduler.ElementAt(Position - 1).AlreadeBe = true;
                }
            }
            var res = Sheduler;
           
            
         
            
           
        }

        QuisInfo quisInfo;
        public async  Task<IQuisInfo> GetExamInfoAsync()
        {
            quisInfo = new QuisInfo();

           
          var InfoResult = MpdcContainer.Instance.Get<IQuezServices>().GetQuisResult();
            var Diploma=  MpdcContainer.Instance.Get<IQuezServices>().GetDiploma();

           await Task.WhenAll(InfoResult, Diploma);

            var statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(InfoResult.Result.test_type);
            var AnswerResult = statistic.FirstOrDefault();

            
            quisInfo.QueisResult = InfoResult.Result;
            quisInfo.DiplomaURl = Diploma.Result;
            if (AnswerResult != null)
            {
                quisInfo.QueisResult.RightAnswer = AnswerResult.CorrectAnswersCount;
                quisInfo.QueisResult.WronAnswers = AnswerResult.IncorrectAnswersCount;
                quisInfo.QueisResult.SkipedAnswers = AnswerResult.SkippedQuestionsCount;
            }

            return quisInfo;

        }


        

    }

   
   

}
