using IZrune.PCL.Abstraction.Models;
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
                {
                    Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();

                    if(Parent != null)
                        Parent.IsAdmin = await MpdcContainer.Instance.Get<IUserServices>()?.IsAdmin();
                }

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


        public void LogOut()
        {
            Parent = null;
            CurrentStudent = null;
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



        public  void RegistrationStudentPartOne(string Name,string LastName,DateTime date,string PersonalId,string Phone,string Mail="")
        {
            RegistrationStudent = new Student();
            RegistrationStudent.Name = Name;
            RegistrationStudent.LastName = LastName;
            RegistrationStudent.Bdate = date;
            RegistrationStudent.PersonalNumber = PersonalId;
            RegistrationStudent.Phone = Phone;
            RegistrationStudent.Email = Mail;
        }



        public  void RegistrationStudentPartTwo(int RegionID,int Schoold,int Clas, string Village ="")
        {
            RegistrationStudent.RegionId = RegionID;
            RegistrationStudent.Village = Village;
            RegistrationStudent.SchoolId = Schoold;
            RegistrationStudent.Class = Clas;

        }

        public void Resetregistration()
        {
            MyRegistrationStudent.Clear();
            RegistrationStudent = null;
            RegistrationUser = null;
        }

        public async void AddStudent()
        {
            await MpdcContainer.Instance.Get<IUserServices>().AddStudent(RegistrationStudent);
            Parent = await MpdcContainer.Instance.Get<IUserServices>().GetUserAsync();
        }

        public int GetAllPackagePrice()
        {
            int Amount = 0;
            foreach(var item in MyRegistrationStudent)
            {
                Amount += item.Amount;
            }
            return Amount;
        }


        public void SetPromoPack(int MonthCount,int Amount,string PromoCode="0")
        {
            RegistrationStudent.PackageStartDate = DateTime.Now;
            RegistrationStudent.PackageMonthCount = MonthCount;
            RegistrationStudent.Amount = Amount;
            RegistrationStudent.Promocode = PromoCode;

            MyRegistrationStudent.Add(RegistrationStudent);
        }


      public  IPay CUrrentPaimentInformation;
        public async Task<IPay> FinishRegistration()
        {
           CUrrentPaimentInformation=await MpdcContainer.Instance.Get<IRegistrationServices>().RegistrationUser(RegistrationUser,MyRegistrationStudent);

            return CUrrentPaimentInformation;
        }


        public async Task ReNewPack(int studentId,int MonthCount, int Amount, string PromoCode = "0",int paybox=0)
        {
            CUrrentPaimentInformation = await MpdcContainer.Instance.Get<IPaymentService>().GetPaymentUrlsAsync(studentId, MonthCount, Amount, PromoCode,paybox);

           

        }


        public IPay GetPaymentInformation()
        {
            return CUrrentPaimentInformation;
        }



        public async Task EditParrentProfile(string ParrentMail, string ParrentPhone, string City, string Village="")
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
                AppCore.Instance.Alertdialog.ShowSaccessDialog("", "წარმატებით მოხდა თქვენი პროფილის შეცვლა");
            }
            catch(Exception ex)
            {
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეფერხება", "მოხდა შეცდომა პროფილის რედაქტირების დროს გთხოვთ სცადოთ ხელახლა");
            }
        }

        private IQuisInfo QuisInf;

        public async Task<IQuisInfo> GetQuisInfo(int Id)
        {
            QuisInf = new QuisInfo();
            
           var Result=  MpdcContainer.Instance.Get<IStatisticServices>().GetCurrentTestDiplomaInfo(Id);
            var DiplomaResult =  MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(Enum.QuezCategory.QuezExam);

           await Task.WhenAll(Result, DiplomaResult);

            var FinalDiplomaResult = DiplomaResult.Result.Where(i => i.Id == Id).FirstOrDefault();



            QuisInf.QueisResult = Result.Result;
            QuisInf.DiplomaURl = FinalDiplomaResult.DiplomaUrl;


            return QuisInf;
        }


        public async Task<IEnumerable<IDiagram>>  GetDiagramStatistic()
        {

            try
            {

                var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest);


                var GroupdExams = Statistic.ToList().GroupBy(c =>
                                         c.ExamDate.Month
                                       ).Select(i => i.Select(o => o.ExamDate).ToList()).ToList();


                var Result = GroupdExams.Select(i => new Diagram()
                {
                    CurrentDate = $"{Monthes.ElementAt(i.FirstOrDefault().Month-1)} {i.FirstOrDefault().Year}",
                    TestCount=i.Count()
                })?.ToList();
                return Result;


            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<string> Monthes = new List<string>
            {
                "იანვარი",
                "თებერვალი",
                "მარტი",
                "აპრილი",
                "მაისი",
                "ივნისი",
                "ივლისი",
                "აგვისტო",
                "სექტემბერი",
                "ოქტომბერი",
                "ნოემვერი",
                "დეკემბერი",
            };
    }
}
