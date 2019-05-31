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
   public class QuezServices : IQuezServices
    {

        private string TestCode;
        private QuezCategory categor;

        public async Task<string> GetDiploma()
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("test_id",TestCode),
              
            });

           
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getDiploma&hashcode=19b556311f007d86ad7c921626e5da83", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<DiplomaDTO>(jsn);

                return Result.diploma_url;
            }
            catch(Exception ex)
            {
                return "";
            }
        }





        public async Task<TimeSpan> GetExamDate(QuezCategory TestType)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("student_id",1.ToString()),
                new KeyValuePair<string, string>("test_type",TestType.ConverEnumToInt())

            });
            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getNextTestDate&hashcode=7a16e984f8df40c478a47cb356092b92", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();
            var Result = JsonConvert.DeserializeObject<TransferModels.NextTestDateDTO>(jsn);
            DateTime.TryParse(Result.date, out DateTime date);
            var CurResult = date.Subtract(DateTime.Now);

            return CurResult;
        }



        public async Task<IEnumerable<IQuestion>> GetQuestionsAsync( QuezCategory TestType)
        {
            IEnumerable<Question> QuestionLst;
            try
            {
                if (AppCore.Instance.IsOnline)
                {
                    var rr = UserControl.Instance.CurrentStudent?.id.ToString();

                    var Resultttt = TestType.ConverEnumToInt();

                    var FormContent = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string,string>("student_id",UserControl.Instance.CurrentStudent?.id.ToString()),
                new KeyValuePair<string, string>("test_type",TestType.ConverEnumToInt())

            });
                    var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getTest&hashcode=26e0c75cd4f8b1242b620a46aa701431", FormContent);
                    var jsn = await Data.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<TransferModels.QuestionRootDTO>(jsn);
                    TestCode = Result.test_id;
                    QuestionLst = Result?
                        .questions?
                        .Select(i => new Question()
                        {
                        id = Convert.ToInt32(i?.id),
                        images = i?.images.Select(x=>x.url),
                        image_url = i?.image_url,
                        title = i?.title,
                        Answers = i?.answers?
                        .Select
                        (o => new Answer()
                        {
                            id = Convert.ToInt32( o?.id),
                            title = o?.title,
                            right = o?.right,
                            IsRight =o?.right == "1" ? true : false

                        })
                    });

                    return QuestionLst;
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

        public async Task GetQuezResultAsync(IQuezQuestion contrl)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("test_id",TestCode),              
                 new KeyValuePair<string, string>("question_id",contrl.QuestionId.ToString()),
                 new KeyValuePair<string, string>("answer_id",contrl.AnswerId.ToString()),
                 new KeyValuePair<string, string>("time",contrl.Duration.ToString())

            });

           
            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getQuestionResult&hashcode=d7dd618dcab06b6f868e4cc69c81e1bd", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

        }

        public async Task<IQuisResultInfo> GetQuisResult()
        {

            var FormContent = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string,string>("test_id",TestCode),
              
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


            return QuesResult;
        }
    }
}
