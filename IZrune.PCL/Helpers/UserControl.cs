﻿using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MpdcContainer = ServiceContainer.ServiceContainer;


namespace IZrune.PCL.Helpers
{
   public class UserControl
   {
        private static UserControl instance = null;
        private static readonly object padlock = new object();

        UserControl()
        {
        }

        public static UserControl Instance {
            get {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            AppCore.Instance.InitServices();
                            instance = new UserControl();
                        }
                    }
                }
                return instance;
            }
        }

        public IParent Parent;

        public IStudent CurrentStudent;

        public async Task<bool>  IsLogedIn()
        {
            try
            {
                var result = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();
                if (result == null)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool>LogInUser(string UserName,string Password)
        {
            try
            {
                
                var Result = await MpdcContainer.Instance.Get<ILoginServices>().LoginUser(UserName, Password);

                return Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IParent> GetCurrentUser()
        {
            try {
                if (Parent == null)
            Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();



            return Parent;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IStudent>> GetCurrentUserStudents()
        {
            try
            {
                if (Parent == null)
                {
                    Parent = await GetCurrentUser();
                }
                var Result = Parent?.Students;

                if (Result?.ToList().Count > 0)
                {
                    return Result;
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

        public async void SeTSelectedStudent(int StudentId)
        {
            try
            {
                if (Parent == null)
                {
                    Parent = await GetCurrentUser();
                }

              
                    CurrentStudent = Parent?.Students.Where(i => i.id == StudentId)?.SingleOrDefault();
              
             
            }
            catch (Exception ex)
            {
              
            }
        }

        private List<Student> MyRegistrationStudent = new List<Student>();

       public Parent RegistrationUser { get; set; }
        public  void RegistrationParrentPartOne(string Name,string LastName,DateTime date,string city,string Village="")
        {
            RegistrationUser = new Parent();
            RegistrationUser.Name = Name;
            RegistrationUser.LastName = LastName;
            RegistrationUser.bDate = date;
            RegistrationUser.City = city;
            RegistrationUser.Vilage = Village;
        }

        public  void RegistrationParrentPartTwo(string Phone,string Mail,string UserName,string Password)
        {
            RegistrationUser.Phone = Phone;
            RegistrationUser.Email = Mail;
            RegistrationUser.UserName = UserName;
            RegistrationUser.Password = Password;
        }

        public Student RegistrationStudent { get; set; }



        public  void RegistrationStudentPartOne(string Name,string LastName,DateTime date,string PersonalId,string Phone,string Mail)
        {
            RegistrationStudent = new Student();
            RegistrationStudent.Name = Name;
            RegistrationStudent.LastName = LastName;
            RegistrationStudent.Bdate = date;
            RegistrationStudent.PersonalNumber = PersonalId;
            RegistrationStudent.Phone = Phone;
            RegistrationStudent.Email = Mail;
        }



        public  void RegistrationStudentPartTwo(int RegionID,string Village,int Schoold,int Clas)
        {
            RegistrationStudent.RegionId = RegionID;
            RegistrationStudent.Village = Village;
            RegistrationStudent.SchoolId = Schoold;
            RegistrationStudent.Class = Clas;

        }



        public async void AddStudent()
        {
            await MpdcContainer.Instance.Get<IUserServices>().AddStudent(RegistrationStudent);
        }




        public void SetPromoPack(int MonthCount,int Amount,string PromoCode="0")
        {
            RegistrationStudent.PackageStartDate = DateTime.Now;
            RegistrationStudent.PackageMonthCount = MonthCount;
            RegistrationStudent.Amount = Amount;
            RegistrationStudent.Promocode = PromoCode;

            MyRegistrationStudent.Add(RegistrationStudent);
        }


        IPay CUrrentPaimentInformation;
        public async Task<IPay> FinishRegistration()
        {
           CUrrentPaimentInformation=await MpdcContainer.Instance.Get<IRegistrationServices>().RegistrationUser(RegistrationUser,MyRegistrationStudent);

            return CUrrentPaimentInformation;
        }

        public IPay GetPaymentInformation()
        {
            return CUrrentPaimentInformation;
        }

        public async Task EditParrentProfile(string ParrentMail, string ParrentPhone, string City, string Village)
        {
            try
            {
                await MpdcContainer.Instance.Get<IUserServices>().EditParentProfileAsync(ParrentMail, ParrentPhone, City, Village);
                Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();
                
            }
            catch(Exception ex)
            {

            }
        }

        public async Task EditStudentprofile(string Email, string Phone, int regionId, string village, int SchoolId)
        {
            try
            {
                await MpdcContainer.Instance.Get<IUserServices>().EditStudentProfile(Email, Phone, regionId, village, SchoolId);
                Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();
               
            }
            catch(Exception ex)
            {

            }
        }


    }
}
