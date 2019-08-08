using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.WebUtils;
using IZrune.TransferModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Implementation.Services
{
    public class PaymentService : IPaymentService
    {
        public async  Task<IEnumerable<IPaymentHistory>> GetPaymentHistory()
        {
            var FormContent = new FormUrlEncodedContent(new[]
                  {
                        new KeyValuePair<string,string>("parent_id",UserControl.Instance.Parent.id.ToString()),
                         
                     });
           
            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getPaymentHistory&hashcode=e733269189a8065cc61d5d43b2f39c9d", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            var Result = JsonConvert.DeserializeObject<PaymentHistoryRootDTO>(jsn);

            var students = await UserControl.Instance.GetCurrentUserStudents();

            if (Result.Code == 0)
            {


                var Res = Result.payment_history.Select(i => new PaymentHistory()
                {

                    Amount = int.Parse(i.amount),
                    Date = DateTime.ParseExact(i.date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    StudentName = students.FirstOrDefault(o => o.id == Convert.ToInt32(i.student_id)).Name
                });

                    return Res;
          
                

            }
            else
            {
                return null;
            }


        }

        public async Task<IPay> GetPaymentUrlsAsync( IEnumerable<IStudent> Students ,int PayBox=1)
        {
            var KeyValuePairsList = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < Students.Count(); i++)
            {

                var temp = new List<KeyValuePair<string, string>>()
                       {
                        new KeyValuePair<string,string>($"student_id{i+1}",Students.ElementAt(i).id.ToString()),
                         new KeyValuePair<string,string>($"sdate{i+1}",$"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}"),
                         new KeyValuePair<string,string>($"months{i+1}",Students.ElementAt(i).PackageMonthCount.ToString()),
                          new KeyValuePair<string,string>($"amount{i+1}",Students.ElementAt(i).Amount.ToString()),
                           new KeyValuePair<string,string>($"promo{i+1}",Students.ElementAt(i).Promocode.ToString()),
                            new KeyValuePair<string,string>($"paybox{i+1}",PayBox.ToString())
                };
                KeyValuePairsList.AddRange(temp);

            }

        var FormContent = new FormUrlEncodedContent(KeyValuePairsList);


        var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=renew&hashcode=6baf003058e14cc0c2031b07ff673871", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            var Result = JsonConvert.DeserializeObject<PaymentRootDTO>(jsn);

            Pay pay = new Pay();
            pay.CurrentUserPayURl = $"https://e-commerce.cartubank.ge/servlet/Process3DSServlet/3dsproxy_init.jsp?PurchaseDesc={Result.payment.PurchaseDesc}&PurchaseAmt={Result.payment.PurchaseAmt}&CountryCode={Result.payment.CountryCode}&CurrencyCode={Result.payment.CurrencyCode}&MerchantName={Result.payment.MerchantName}&MerchantURL={Result.payment.MerchantURL}&MerchantCity={Result.payment.MerchantCity}&MerchantID={Result.payment.MerchantID}&xDDDSProxy.Language={Result.payment.Language}";

            pay.SuccesUrl = Result.payment_success_url;
            pay.FailUrl = Result.payment_fail_url;

            return pay;

        }

        public async Task<IPay> GetPaymentUrlsAsync(IStudent Student, int PayBox = 1)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                    {
                       new KeyValuePair<string,string>($"student_id1",Student.id.ToString()),
                         new KeyValuePair<string,string>($"sdate1",$"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}"),
                         new KeyValuePair<string,string>($"months1",Student.PackageMonthCount.ToString()),
                          new KeyValuePair<string,string>($"amount1",Student.Amount.ToString()),
                           new KeyValuePair<string,string>($"promo1",Student.Promocode.ToString()),
                            new KeyValuePair<string,string>($"paybox1","1")

                     });



            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=renew&hashcode=6baf003058e14cc0c2031b07ff673871", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            var Result = JsonConvert.DeserializeObject<PaymentRootDTO>(jsn);

            Pay pay = new Pay();
            pay.CurrentUserPayURl = $"https://e-commerce.cartubank.ge/servlet/Process3DSServlet/3dsproxy_init.jsp?PurchaseDesc={Result.payment.PurchaseDesc}&PurchaseAmt={Result.payment.PurchaseAmt}&CountryCode={Result.payment.CountryCode}&CurrencyCode={Result.payment.CurrencyCode}&MerchantName={Result.payment.MerchantName}&MerchantURL={Result.payment.MerchantURL}&MerchantCity={Result.payment.MerchantCity}&MerchantID={Result.payment.MerchantID}&xDDDSProxy.Language={Result.payment.Language}";

            pay.SuccesUrl = Result.payment_success_url;
            pay.FailUrl = Result.payment_fail_url;

            return pay;
        }
    }
}
