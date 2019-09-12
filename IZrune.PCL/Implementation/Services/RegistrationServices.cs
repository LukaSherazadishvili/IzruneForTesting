using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
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
   public class RegistrationServices : IRegistrationServices
    {
        public async Task<bool> ExistPersonalId(string PersonlaId)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("personal_number",PersonlaId),

                     });


                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=existPersonalNumber&hashcode=47210b0fc9c6854d3dc4e33099bbdd14", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<UserNamePersonIdDTO>(jsn);

                if (Result.Code == 1)
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "მომხმარებელი ესეთი პირადი ნომრით უკვე არსებობს");

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "");
                return true;
            }
        }




        public async Task<bool> ExistUserName(string UserName)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                     {
                        new KeyValuePair<string,string>("username",UserName),

                     });

                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=existUsername&hashcode=faea275c3ef6bf91106129af2c6122a4", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                var Result = JsonConvert.DeserializeObject<UserNamePersonIdDTO>(jsn);

                if (Result.Code == 1)
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "მომხმარებელი ესეთი სახელით უკვე არსებობს");

                    return true;

                }
                else
                {
                   
                    return false;
                }
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "");
                return true;
            }

        }

        public async Task<string> GetAgreement()
        {
            try
            {
                var Data = await IzruneWebClient.Instance.GetDataAsync<AgreementDTO>("http://izrune.ge/api.php?op=getAgreement&hashcode=65702027de46ae5cac739f1e02f11847");

                return Data.agreement;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<IEnumerable<IRegion>> GetRegionsAsync()
        {
            IEnumerable<IRegion> ReginResult;
           

            if (AppCore.Instance.IsOnline)
            {
                try
                {
                    using(HttpClient client=new HttpClient())
                    {
                        var data = await IzruneWebClient.Instance.GetDataAsync<RegionRootDTO>("https://izrune.ge/api.php?op=getRegistrationInfo&hashcode=41942a09e43e2260815b542325397d4e");
                         ReginResult = data
                             .regions
                             .Select(i => new Region()
                             {
                                 id =int.Parse( i.id),
                                 title = i.title,
                                 Schools = i.schools.
                                 Select(x => new School() {
                                     id =int.Parse( x.id),
                                     title = x.title

                                 })
                             });
                         
                    }
                    return ReginResult;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public async Task<IPay> RegistrationUser(IParent user,IEnumerable<IStudent> student)
        {

            try
            {
                var KeyValuePairsList = new List<KeyValuePair<string, string>>() {

                  new KeyValuePair<string,string>("username",user.UserName),
                new KeyValuePair<string, string>("password",user.Password),
                new KeyValuePair<string,string>("name",user.Name),
                new KeyValuePair<string, string>("lastname",user.LastName),
                new KeyValuePair<string,string>("email",user.Email),
                new KeyValuePair<string, string>("phone",user.Phone),
                new KeyValuePair<string,string>("city",user.City),
                new KeyValuePair<string, string>("bdate",$"{user.bDate.Value.Year}-{user.bDate.Value.Month}-{user.bDate.Value.Day}" ),
                new KeyValuePair<string,string>("paybox","1"),
            };

               
                    for (int i = 0; i < student.Count(); i++)
                    {

                        var Temps = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>($"name{i+1}", student.ElementAt(i).Name),
                new KeyValuePair<string, string>($"lastname{i+1}", student.ElementAt(i).LastName),
                new KeyValuePair<string, string>($"personal_number{i+1}", student.ElementAt(i).PersonalNumber),
                 new KeyValuePair<string, string>($"email{i+1}", student.ElementAt(i).Email),
                new KeyValuePair<string, string>($"phone{i+1}", student.ElementAt(i).Phone),
                new KeyValuePair<string, string>($"region_id{i+1}", student.ElementAt(i).RegionId.ToString()),
                new KeyValuePair<string, string>($"village{i+1}", student.ElementAt(i).Village),
                new KeyValuePair<string, string>($"bdate{i+1}", $"{student.ElementAt(i).Bdate.Year}-{student.ElementAt(i).Bdate.Month}-{student.ElementAt(i).Bdate.Day}"),
                new KeyValuePair<string, string>($"school_id{i+1}", student.ElementAt(i).SchoolId.ToString()),
                new KeyValuePair<string, string>($"class{i+1}", student.ElementAt(i).Class.ToString()),
                new KeyValuePair<string, string>($"sdate{i+1}", student.ElementAt(i).Promocode=="0"||string.IsNullOrEmpty(student.ElementAt(i).Promocode)?                 $" {DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}":
                         $"{DateTime.Now.Year}-{9}-{1}"),
                new KeyValuePair<string, string>($"months{i+1}", student.ElementAt(i).PackageMonthCount.ToString()),
                 new KeyValuePair<string, string>($"amount{i+1}", student.ElementAt(i).Amount.ToString()),
                  new KeyValuePair<string, string>($"promo{i+1}", student.ElementAt(i).Promocode)
                  //new KeyValuePair<string,string>($"paybox{i+1}","1")
                  };
                        KeyValuePairsList.AddRange(Temps);
                    }

                

                var FormContent = new FormUrlEncodedContent(KeyValuePairsList);

                var Data = await IzruneWebClient.Instance.GetPostData("https://izrune.ge/api.php?op=register&hashcode=4e5e0ccbab0da8c25637b0aa14e6cbbd", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();

                PaymentRootDTO Result = null;

                try
                {
                    Result = JsonConvert.DeserializeObject<PaymentRootDTO>(jsn);
                }
                catch (Exception ex)
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "რეგისტრაციისას მოხდა შეცდომა , სცადეთ მოგვიანებით");
                }



                if (Result != null && Result.Message.ToLower() != "ok")
                {
                    //if(Result.Message.ToLower() == "personal number already exists")
                    //{
                    //    AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "მომხმარებელი ასეთი პირადი ნომრით უკვე არსებობს");
                    //    return null;
                    //}

                    //if(Result.Message.ToLower() == "username already exists")
                    //{
                    //    AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "მომხმარებელი ასეთი სახელით უკვე არსებობს");
                    //    return null;
                    //}

                    AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", Result.Message);
                    return null;
                }
                else if (Result == null)
                {
                    AppCore.Instance.Alertdialog.ShowAlerDialog("მოხდა შეცდომა", "რეგისტრაციისას მოხდა შეცდომა , სცადეთ მოგვიანებით");
                    return null;
                }


                Pay pay = new Pay();
                pay.CurrentUserPayURl = $"https://e-commerce.cartubank.ge/servlet/Process3DSServlet/3dsproxy_init.jsp?PurchaseDesc={Result.payment.PurchaseDesc}&PurchaseAmt={Result.payment.PurchaseAmt}&CountryCode={Result.payment.CountryCode}&CurrencyCode={Result.payment.CurrencyCode}&MerchantName={Result.payment.MerchantName}&MerchantURL={Result.payment.MerchantURL}&MerchantCity={Result.payment.MerchantCity}&MerchantID={Result.payment.MerchantID}&xDDDSProxy.Language={Result.payment.Language}";

                pay.SuccesUrl = Result.payment_success_url;
                pay.FailUrl = Result.payment_fail_url;

                return pay;
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("", "მოხდა შეცდომა რეგისტრაციის დროს");
                return null;
            }
        }

        

    }
}
