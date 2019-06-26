﻿using IZrune.PCL.Abstraction.Models;
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
   public class UserServices : IUserServices
    {
        public async Task AddStudent(IStudent student)
        {
            var FormContent = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string,string>("parent_id",UserControl.Instance.GetCurrentUser().Result.id.ToString()),
                 new KeyValuePair<string,string>("name",student.Name),
                  new KeyValuePair<string,string>("lastname",student.LastName),
                  new KeyValuePair<string,string>("personal_number",student.PersonalNumber),
                  new KeyValuePair<string,string>("email",student.Email),
                  new KeyValuePair<string,string>("phone",student.Phone),
                 new KeyValuePair<string,string>("region_id",student.RegionId.ToString()),
                  new KeyValuePair<string,string>("village",student.Village),
                  new KeyValuePair<string,string>("bdate",student.Bdate.ToShortTimeString()),
                  new KeyValuePair<string,string>("school_id",student.SchoolId.ToString()),
                  new KeyValuePair<string,string>("class",student.Class.ToString()),
                  new KeyValuePair<string,string>("sdate",DateTime.Now.ToShortDateString()),
                   new KeyValuePair<string,string>("months",student.PackageMonthCount.ToString())
                });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=addStudent&hashcode=d529edb90d98f79c0c0e2e799933c1c4", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();


        }

        public async Task<bool> EditePassword( string oldPassword, string NewPassword)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("parent_id",UserControl.Instance.GetCurrentUser().Result.id.ToString()),
                 new KeyValuePair<string,string>("old_password",oldPassword),
                  new KeyValuePair<string,string>("new_password",NewPassword),
                
                });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=editPassword&hashcode=0912a1be35b2f263eb97149b2e67f40a", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RecoverStatusDTO>(jsn);

            if (result.Code == 0)
                return true;
            else
                return false;



        }

        public async Task EditParentProfileAsync( string ParrentMail, string ParrentPhone, string City, string Village)
        {

            var FormContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("parent_id",UserControl.Instance.GetCurrentUser().Result.id.ToString()),
                 new KeyValuePair<string,string>("email",ParrentMail),
                  new KeyValuePair<string,string>("phone",ParrentPhone),
                  new KeyValuePair<string,string>("city",City),
                  new KeyValuePair<string,string>("village",Village)
                });
            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=editParentProfile&hashcode=0a3110bbe8a96c91eb33bf6072598368", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();
         
        }

        public async Task EditStudentProfile(string Email, string Phone, int regionId, string village, int SchoolId)
        {
            var FormContent = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string,string>("student_id",UserControl.Instance.CurrentStudent.id.ToString()),
                 new KeyValuePair<string,string>("email",Email),
                  new KeyValuePair<string,string>("phone",Phone),
                  new KeyValuePair<string,string>("region_id",regionId.ToString()),
                  new KeyValuePair<string,string>("village",village),
                   new KeyValuePair<string,string>("school_id",SchoolId.ToString())
                });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=editStudentProfile&hashcode=892c21e1d80fd2f7c1bd78e8a63f704f", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();
        }

        public async Task<IPromoCode> GetPromoCodeAsync(int SchoolId=0)
        {

            try
            {

                var FormContent = new FormUrlEncodedContent(new[]
                 {
                new KeyValuePair<string,string>("id",SchoolId.ToString()),
                });
                var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=getPromoCode&hashcode=52c490da82162ed3cfaff1d7f5bb9287", FormContent);
                var jsn = await Data.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PromoCodeDTO>(jsn);
                PromoCode promCod = new PromoCode();


                if (result.Code == 0)
                {
                    promCod.PrommoCode = result?.promocode;
                    promCod.Prices = result?.prices?.Select(i =>
                    new Price() {
                        price = i.price,
                        months = i.months });
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
                    parent.Name = DTO?.name;
                    parent.Vilage = DTO.village;
                    parent.Phone = DTO.phone;
                    parent.Email = DTO.email;
                    parent.bDate = DateTime.ParseExact(DTO.birth_date,"dd.MM.yyyy",CultureInfo.InvariantCulture);
                    parent.LastName = DTO?.lastname;
                    parent.City = DTO.city;
                    parent.Students = DTO?.students?.                      
                        Select(i => new Student() {
                            Name = i?.name,
                            LastName=i?.lastname,
                            id=Convert.ToInt32(i?.id),
                            Bdate=DateTime.ParseExact(i.birth_date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                            Email =i.email,
                            Phone=i.phone,
                            Village=i.village,
                            PersonalNumber=i.personal_number,
                            RegionId= Convert.ToInt32( i.region_id),
                            SchoolId=Convert.ToInt32(i.school_id),                            
                            
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

        public async Task<bool> RecoverPasswordAsync(string PhoneNumber)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                  {
                         new KeyValuePair<string,string>("phone",PhoneNumber),
                    });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=recoverPassword&hashcode=f8da048644f6752faca46da3d3d38229", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();

            var Result = JsonConvert.DeserializeObject<RecoverStatusDTO>(jsn);

            if (Result.Code == 0)
                return true;
            else
                return false;

        }

        public async Task<bool> RecoverUserNamedAsync(string PhoneNumber)
        {
            var FormContent = new FormUrlEncodedContent(new[]
                 {
                         new KeyValuePair<string,string>("phone",PhoneNumber),
                    });

            var Data = await IzruneWebClient.Instance.GetPostData("http://izrune.ge/api.php?op=recoverUsername&hashcode=82a93889e6a50556b3c1805dd55d59e9", FormContent);
            var jsn = await Data.Content.ReadAsStringAsync();
            var Result = JsonConvert.DeserializeObject<RecoverStatusDTO>(jsn);

            if (Result.Code == 0)
                return true;
            else
                return false;
        }


    }
}
