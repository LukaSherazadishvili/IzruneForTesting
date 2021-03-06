﻿using IZrune.PCL.Abstraction.Models;
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

        public async Task<bool> CheckSmsCode(string SmsCode)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("parent_id",UserControl.Instance.Parent.id.ToString()),
                 new KeyValuePair<string,string>("sms_code",SmsCode),
            });


                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=checkSmsCode&hashcode=1c20e2be31dd7524a829b3149542d6c5", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<ChesmsDto>(jsn);

                return Result.Code == 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public async Task<string> GetDiploma()
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                   {
                new KeyValuePair<string,string>("test_id",TestCode),
              
            });

           
                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getDiploma&hashcode=19b556311f007d86ad7c921626e5da83", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<DiplomaDTO>(jsn);

                return Result.diploma_url;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<string> GetEgmuAsync(int TestId)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                      {
                new KeyValuePair<string,string>("test_id",TestId.ToString()),

            });

                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getEgmuUrl&hashcode=2d56f16f329559e972c51f1b6ed5401c", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<EgmuDTO>(jsn);

                return Result.egmu_url;

            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<TimeSpan> GetExamDate(QuezCategory TestType)
        {

            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                      {
                new KeyValuePair<string,string>("student_id",UserControl.Instance.CurrentStudent?.id.ToString()),
                new KeyValuePair<string, string>("test_type",TestType.ConverEnumToInt())

            });
                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getNextTestDate&hashcode=7a16e984f8df40c478a47cb356092b92", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<TransferModels.NextTestDateDTO>(jsn);
                DateTime.TryParse(Result.date, out DateTime date);
                var CurResult = date.Subtract(DateTime.Now);

                return CurResult;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new TimeSpan();
            }
            
           
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
                    var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getTest&hashcode=26e0c75cd4f8b1242b620a46aa701431", FormContent);
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
                        Description=i.description,
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
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "თქვენ არ გაქვთ ინტერნეტთან კავშირი");
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

           
            var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getQuestionResult&hashcode=d7dd618dcab06b6f868e4cc69c81e1bd", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

        }

        public async Task<IQuisResultInfo> GetQuisResult()
        {

            var FormContent = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string,string>("test_id",TestCode),
              
            });

            QuisResultInfo QuesResult = new QuisResultInfo();
            try
            {
                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getTestInfo&hashcode=1218b084b72f42914d4c868a2eec191b", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<QuisResultInfoRootDTO>(jsn);
                var info = Result.info;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return QuesResult;
        }

        public async Task<int> GetSmsCodeAsync(int ParrentId)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                         {
                new KeyValuePair<string,string>("parent_id",ParrentId.ToString()),

            });

                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=getSmsCode&hashcode=379c81983cb70e54978a50cef15f4308", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<SmsCodeDTO>(jsn);

                if (Result.Code == 0)
                {
                    AppCore.Instance.Alertdialog.ShowSaccessDialog("", "კოდი გამოგზავნილია თქვენს მიერ რეგისტრაციის დროს მითითებულ ნომერზე");
                    return Result.sms_code;
                }
                else
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "ვერ მოხერხდა კოდის გამოგზავნა");

                    return 0;
                }
            }
            catch(Exception ex)
            {

                AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "ვერ მოხერხდა კოდის გამოგზავნა");
                return 0;
               
            }
        }
    }
}
