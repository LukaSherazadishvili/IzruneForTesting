using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Enum;
using IZrune.PCL.extensions;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.WebUtils;
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
                            id = o?.id,
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

        public Task GetQuezResultAsync( int Duration, List<IQuestion> QuestionList)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("student_id",TestCode),
                new KeyValuePair<string, string>("duration",Duration.ToString())

            });

            for(int i = 0; i < QuestionList.Count; i++)
            {


                KeyValuePair<string, string> pair = new KeyValuePair<string, string>($"question_id_{i + 1}", QuestionList.ElementAt(i).id.ToString());

                KeyValuePair<string, string> pair2 = new KeyValuePair<string,string>($"answer_id{i + 1}","");
            }

            return null;
        }
    }
}
