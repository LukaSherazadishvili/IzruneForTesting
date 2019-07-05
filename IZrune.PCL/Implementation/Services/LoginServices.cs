using IZrune.PCL.Abstraction.Services;
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
   public class LoginServices : ILoginServices
    {

        

        public async Task<bool> LoginUser(string username, string password)
        {
            try
            {
                var FormContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string, string>("password",password)

            });
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=login&hashcode=b1188c7b5e9abe809bd538c48aa08222", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<StatusCodeDTO>(jsn);


                if (Result.Message == "OK" && Result.Code == 0)
                {
                    AppCore.Instance.CurrentUserToken = Result.Token;
                    return true;
                }
                else
                {
                    AppCore.Instance.CurrentUserToken = "";
                    AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "თქვენს მიერშეყვანილი სახელი ან პაროლი არ არის რეგისტრირებული");
                
                return false;
                }
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "თქვენს მიერშეყვანილი სახელი ან პაროლი არ არის რეგისტრირებული");
                return false;
            }
        }
    }
}
