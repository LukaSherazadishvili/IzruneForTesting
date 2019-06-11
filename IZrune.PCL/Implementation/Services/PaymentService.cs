using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.WebUtils;
using IZrune.TransferModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Implementation.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<IPay> GetPaymentUrlsAsync(int StudentId, int MonthCount, int Amount, string promoCode = "0")
        {

            var FormContent = new FormUrlEncodedContent(new[]
                   {
                        new KeyValuePair<string,string>("student_id1",StudentId.ToString()),
                         new KeyValuePair<string,string>("sdate1",DateTime.Now.ToShortDateString()),
                         new KeyValuePair<string,string>("months1",MonthCount.ToString()),
                          new KeyValuePair<string,string>("amount1",Amount.ToString()),
                           new KeyValuePair<string,string>("promo1",promoCode)
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
