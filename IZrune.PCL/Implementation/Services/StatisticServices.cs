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
        public async Task<IQuisResultInfo> GetCurrentTestDiplomaInfo(int TestId)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                        {
                new KeyValuePair<string,string>("test_id",TestId.ToString()),

            });
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getTestInfo&hashcode=1218b084b72f42914d4c868a2eec191b", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<QuisResultInfoRootDTO>(jsn);
                var info = Result.info;
                QuisResultInfo QuesResult = new QuisResultInfo();

                DateTime.TryParse(info.date, out DateTime date);
                int.TryParse(info.duration, out int Time);
                QuesResult.Date = date;
                QuesResult.Duration = Time;
                QuesResult.Egmu = info.egmu;
                QuesResult.Score = info.score;
                QuesResult.Stars = info.stars;
                QuesResult.test_type = info.test_type == "1" ? QuezCategory.QuezExam : QuezCategory.QuezTest;
                QuesResult.text_description = info.text_description;
                QuesResult.text_title = info.text_title;

                return QuesResult;
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "ინფორმაცია ვერ მოიძებნა არ არ ხართ დაკავშირებული ინტერნეტთან");
                return null;
            }
        }

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
                    Description=i.description,
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
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "ინფორმაცია ვერ მოიძებნა ან არ ხართ დაკავშირებული ინტერნეტთან");
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
                         Id=Convert.ToInt32(i.test_id),
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

                        DiplomaUrl=i.diploma_url,
                        ExamDate = Convert.ToDateTime(i.date),
                        Point = Convert.ToInt32(i.score),
                        TestTimeInSecconds = Convert.ToInt32( i.duration),
                        Questions=i.questions.Select(x=>new FinalQuestion() {

                            title = x.title,
                            images = x.images.Select(o => o.url),
                            StudentAnswerIndex = x.answers.IndexOf(x.answers.Where(n => n.student_answer == 1).FirstOrDefault()),
                            Description = x.description,
                            Answers = x.answers.Select(o => new Answer()
                            {
                                IsRight = o.right == "1" ? true : false,
                                title = o.title
                            })

                        })
                        
                    });



                    return StudentStat;

                }
                else
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "ინფორმაცია ვერ მოიძებნა ან არ ხართ დაკავშირებული ინტერნეტთან");
                    return null;
                }
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "ინფორმაცია ვერ მოიძებნა ან არ ხართ დაკავშირებული ინტერნეტთან");
                return null; ;
            }
        }

        public async Task<IEnumerable<IDiplomStatistic>> GetDiplomaStatisticAsync()
        {

            try
            {

                var temp = new List<IDiplomStatistic>();

                var Data = await GetStudentStatisticsAsync(QuezCategory.QuezExam);

                var Result = Data.Where(i => i.DiplomaUrl != "").ToList();


                var Years = Result.DistinctBy(i => i.ExamDate.Year).ToList();

                foreach (var Items in Years)
                {
                    var dploma = new DiplomaStatisticc()
                    {
                        DiplomaDate = $"{Items.ExamDate.Year - 1}-{Items.ExamDate.Year}",
                        
                        
                    };

                    temp.Add(dploma);
                }

                for (int i = 0; i < Years.Count; i++)
                {

                    var After = new DateTime(Years.ElementAt(i).ExamDate.Year, 6, 30);
                    var FromDate = new DateTime(Years.ElementAt(i).ExamDate.Year - 1, 9, 1);


                    var filtered = Result.Where(x => x.ExamDate <= After && x.ExamDate >= FromDate).ToList();
                    var levanaYleProgramistiaTasks = filtered.Select(o => Task<IStudentsStatistic>.Run(async () => await GetCurrentTestDiplomaInfo(o.Id)));

                    await Task.WhenAll(levanaYleProgramistiaTasks);

                    temp.ElementAt(i).DiplomaStatistic = filtered.Select(o=>new QuisInfo() {
                        DiplomaURl=o.DiplomaUrl,
                        QueisResult = levanaYleProgramistiaTasks.ElementAt(filtered.IndexOf(o)).Result,
                        QuestionResult=Result.Where(x => x.ExamDate <= After && x.ExamDate >= FromDate).ElementAt(filtered.IndexOf(o)).Questions
                       

                    }).ToList();



                }

                return temp;
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "ინფორმაცია ვერ მოიძებნა ან არ ხართ დაკავშირებული ინტერნეტთან");
                return null;
            }
        }

        
    }
}
