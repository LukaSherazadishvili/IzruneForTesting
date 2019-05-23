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
   public class UserServices : IUserServices
    {
        public async Task<bool> EditParentProfileAsync(int ParrentId, string ParrentMail, string ParrentPhone, string City, string Village)
        {

            var FormContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("parent_id",ParrentId.ToString()),
                 new KeyValuePair<string,string>("parent_id",ParrentMail),
                  new KeyValuePair<string,string>("parent_id",ParrentPhone),
                  new KeyValuePair<string,string>("parent_id",City),
                  new KeyValuePair<string,string>("parent_id",Village)
                });
            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=editParentProfile&hashcode=0a3110bbe8a96c91eb33bf6072598368", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            return false;

        }

        public async Task<IPromoCode> GetPromoCodeAsync(string SchoolId= "1902")
        {

            try
            {

                var FormContent = new FormUrlEncodedContent(new[]
                 {
                new KeyValuePair<string,string>("id",SchoolId),
                });
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getPromoCode&hashcode=52c490da82162ed3cfaff1d7f5bb9287", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PromoCodeDTO>(jsn);
                PromoCode promCod = new PromoCode();


                if (result.Code == 0)
                {
                    promCod.PrommoCode = result?.promocode;
                    promCod.Prices = result?.prices?.Select(i => new Price() { price = i.price, months = i.months });
                    return promCod;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async  Task<IParent> GetUserAsync()
        {
            try
            {
                if (AppCore.Instance.IsOnline)
                {


                    if (AppCore.Instance.CurrentUserToken == "")
                        return null;


                    var FormContent = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string,string>("token",AppCore.Instance.CurrentUserToken),
                    });

                    var Data =await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getUser&hashcode=bab8e7bb7604c16794517613e2078e2e", FormContent);
                    var jsn =await Data.Content.ReadAsStringAsync();
                    var DTO = JsonConvert.DeserializeObject<ParentStatusDTO>(jsn);

                    Parent parent = new Parent();
                    parent.id =Convert.ToInt32( DTO.ID);
                    parent.Name = DTO?.Name;
                    parent.LastName = DTO?.Lastname;
                    parent.Students = DTO?.students?.
                        Select(i => new Student() {
                            Name = i?.name,
                            LastName=i?.lastname,
                            id=Convert.ToInt32(i?.id)
                      });
                    return parent;
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
    }
}
