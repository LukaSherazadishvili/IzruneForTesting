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
                        var data = await IzruneWebClient.Instance.GetDataAsync<RegionRootDTO>("http://izrune.ge/api.php?op=getRegistrationInfo&hashcode=41942a09e43e2260815b542325397d4e");
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

        public async Task<bool> RegistrationUser(IParent user, IStudent student)
        {
            var FormContent = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string,string>("username",user.UserName),
                new KeyValuePair<string, string>("password",user.Password),
                new KeyValuePair<string,string>("name",user.Name),
                new KeyValuePair<string, string>("lastname",user.LastName),
                new KeyValuePair<string,string>("email",user.Email),
                new KeyValuePair<string, string>("phone",user.Phone),
                new KeyValuePair<string,string>("city",user.City),
                new KeyValuePair<string, string>("bdate",user.bDate.Value.ToShortDateString()),
                new KeyValuePair<string, string>("name1",student.Name),
                new KeyValuePair<string,string>("lastname1",student.LastName),
                new KeyValuePair<string, string>("personal_number1",student.PersonalNumber),
                 new KeyValuePair<string, string>("email1",student.Email),
                new KeyValuePair<string,string>("phone1",student.Phone),
                new KeyValuePair<string, string>("region_id1",student.RegionId.ToString()),
                new KeyValuePair<string, string>("village1",student.Village),
                new KeyValuePair<string,string>("bdate1",student.Bdate.ToShortDateString()),
                new KeyValuePair<string, string>("school_id1",student.SchoolId.ToString()),
                new KeyValuePair<string, string>("class1",student.Class.ToString()),
                new KeyValuePair<string,string>("sdate1",student.PackageStartDate.ToShortDateString()),
                new KeyValuePair<string, string>("months1",student.PackageMonthCount.ToString())
            });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=register&hashcode=4e5e0ccbab0da8c25637b0aa14e6cbbd", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            var Result = JsonConvert.DeserializeObject<StatusCodeDTO>(jsn);

            if (Result.Message == "OK" && Result.Code == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
