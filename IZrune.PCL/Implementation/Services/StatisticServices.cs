using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Enum;
using IZrune.PCL.extensions;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.WebUtils;
using IZrune.TransferModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Implementation.Services
{
    public class StatisticServices : IStatisticServices
    {
        public async Task<IEnumerable<IQuestion>> GetFinalQuestionResult()
        {
            try
            {
                IEnumerable<IFinalQuestion> FinalQuest;

                var FormContent = new FormUrlEncodedContent(new[]
                       {
                        new KeyValuePair<string,string>("student_id",UserControl.Instance.CurrentStudent?.id.ToString())
                     });
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getStatistics&hashcode=2eb56752d70e796575e4b70f88d07248", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<QuezStatisticRootDTO>(jsn);
                var Test = Result.tests.FirstOrDefault();
                FinalQuest = Test.questions.Select(i => new FinalQuestion()
                {
                    title = i.title,
                    images = i.images.Select(o => o.url),
                    StudentAnswerIndex = i.answers.IndexOf(i.answers.Where(x => x.student_answer == 1).FirstOrDefault()),

                    Answers = i.answers.Select(o => new Answer()
                    {                        
                        IsRight = o.right == "1" ? true : false,                                       
                        title = o.title
                    })

                });

                return FinalQuest;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IStudentsStatistic>> GetStudentStatisticsAsync(QuezCategory type)
        {
           
            try
            {
                IEnumerable<IStudentsStatistic> StudentStat;
                if (AppCore.Instance.IsOnline)
                {

                    var FormContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("student_id",UserControl.Instance.CurrentStudent?.id.ToString())
             
                     });

                    var Data =await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getStatistics&hashcode=2eb56752d70e796575e4b70f88d07248", FormContent);
                    var jsn = await Data.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<QuezStatisticRootDTO>(jsn);
                    
                    StudentStat = Result.tests.Where(i=>i.test_type==type.ConverEnumToInt())
                        .Select(i => new StudentsStatistic()
                    {
                        CorrectAnswersCount = i.questions
                        .Where(o => o.answers
                        .Where(x => x.right == "1").SingleOrDefault().student_answer == 1).Count(),

                        IncorrectAnswersCount = i.questions
                        .Where(o => o.answers
                        .Where(x => x.right == "1").SingleOrDefault().student_answer == 0).Count()-
                        i.questions
                        .Where(x => x.answers
                        .All(o => o.student_answer == 0)).Count(),

                        SkippedQuestionsCount = i.questions
                        .Where(x => x.answers
                        .All(o => o.student_answer == 0)).Count(),


                        ExamDate = Convert.ToDateTime(i.date),
                        Point = Convert.ToInt32(i.score),
                        TestTimeInSecconds = Convert.ToInt32( i.duration)
                    });



                    return StudentStat;

                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null; ;
            }
        }

        
    }
}
