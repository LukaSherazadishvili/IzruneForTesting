using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Models;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class StudentProfileEditActivity : MPDCBaseActivity
    {

        protected override int LayoutResource { get; } = Resource.Layout.layoutStudentProfile;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }


        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.StudentSpiner)]
        Spinner StudentSpiner;

        [MapControl(Resource.Id.StudentNameTxt)]
        TextView StudentName;

        [MapControl(Resource.Id.StudentLastNametxt)]
        TextView StudentLastName;

        [MapControl(Resource.Id.StudentId)]
        TextView StudentId;

        [MapControl(Resource.Id.StudentMobile)]
        EditText MobilePhone;

        [MapControl(Resource.Id.StudentScholTxt)]
        Spinner StudentSchool;

        [MapControl(Resource.Id.StudentClasses)]
        TextView StudentClass;

        [MapControl(Resource.Id.StudentCity)]
        Spinner StudentCity;

        [MapControl(Resource.Id.SaveButton)]
        LinearLayout SaveButton;

        [MapControl(Resource.Id.StudentMail)]
        EditText StudentMail;

        [MapControl(Resource.Id.StudentVillage)]
        EditText StudentVillage;

        [MapControl(Resource.Id.StudentBDayDay)]
        TextView BDayDay;

        [MapControl(Resource.Id.StudentBdayMonth)]
        TextView BdayMonth;

        [MapControl(Resource.Id.StudentBdayYear)]
        TextView BdayYear;

        IStudent student;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Startloading();

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

            var Result = await UserControl.Instance.GetCurrentUser();
            var Regions = await MpdcContainer.Instance.Get<IRegistrationServices>().GetRegionsAsync();

            student = Result.Students.FirstOrDefault(); 

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
              Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());


            var RegionAdapter = new ArrayAdapter<string>(this,
          Android.Resource.Layout.SimpleSpinnerDropDownItem,
         Regions.Select(i => i.title).ToList());





           

            StudentSpiner.Adapter = DataAdapter;
            StudentSpiner.ItemSelected += (s, e) =>
            {
                UserControl.Instance.SeTSelectedStudent(Result.Students.ElementAt(e.Position).id);
                student = UserControl.Instance.CurrentStudent;

                StudentName.Text = student.Name;
                StudentLastName.Text = student.LastName;
                StudentId.Text = student.PersonalNumber;
                MobilePhone.Text = student.Phone;
                StudentClass.Text = student.Class.ToString();
                BDayDay.Text = student.Bdate.Day.ToString();
                BdayMonth.Text = student.Bdate.Month.ToString();
                BdayYear.Text = student.Bdate.Year.ToString();

                // StudentSchool.Text = Regions.FirstOrDefault(i => i.id == student.RegionId).Schools.Where(i => i.id == student.SchoolId).FirstOrDefault().title;
                StudentMail.Text = student.Email;
                StudentVillage.Text = student.Village;

                StudentCity.Adapter = RegionAdapter;

                var regg = Regions.Where(i => i.id == student.RegionId).FirstOrDefault();

                var poss = RegionAdapter.GetPosition(regg.title);
                StudentCity.SetSelection(poss);


                var SchoolRes = Regions.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.Select(x => x.title).ToList();

                SchoolRes.Insert(0, "სკოლა");


                var SchoolAdapterr = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
              SchoolRes
            );

                var aasd = student.SchoolId;

              
                var Ress = Regions?.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.Where(i => i.id == student.SchoolId).FirstOrDefault();
                StudentSchool.Adapter = SchoolAdapterr;

                if (Ress != null)
                {
                    var ScholdPoss = SchoolAdapterr.GetPosition(Ress.title);
                    StudentSchool.SetSelection(ScholdPoss);
                }
                else
                {
                    StudentSchool.SetSelection(0);
                }
            };





            //  StudentCity.Adapter = RegionAdapter;
            var SchoolRess = Regions.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.Select(x => x.title).ToList();
              SchoolRess.Insert(0, "სკოლა");
         
            var SchoolAdapter = new ArrayAdapter<string>(this,
         Android.Resource.Layout.SimpleSpinnerDropDownItem,
        SchoolRess);


         //   bool isCheck = false;
            StudentCity.ItemSelected += (s, e) =>
            {
               
                    student.RegionId = Regions.ElementAt(e.Position).id;

                var SchooldResource = Regions.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.Select(x => x.title).ToList();
                SchooldResource.Insert(0, "სკოლა");


                var schladapter = new ArrayAdapter<string>(this,
          Android.Resource.Layout.SimpleSpinnerDropDownItem, SchooldResource
         );
                StudentSchool.Adapter = schladapter;

                StudentSchool.ItemSelected += (sender, eargs) =>
                {
                   // student.SchoolId = Regions.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.ElementAt(eargs.Position).id;

                };
                // isCheck = true;

            };

            var reg = Regions.Where(i => i.id == student.RegionId).FirstOrDefault();

           var pos= RegionAdapter.GetPosition(reg.title);
            StudentCity.SetSelection(pos);




            var asd = student.SchoolId;
        
          StudentSchool.Adapter = SchoolAdapter;
            var Res = Regions?.Where(i => i.id == student.RegionId).FirstOrDefault()?.Schools?.Where(i => i.id == student.SchoolId)?.FirstOrDefault();

            StudentSchool.ItemSelected += (s, e) =>
            {
                if (Res == null)
                {
                    student.SchoolId = Regions.Where(i => i.id == student.RegionId).FirstOrDefault().Schools.FirstOrDefault().id;
                }

                if (Res != null)
                {
                    var ScholdPos = SchoolAdapter.GetPosition(Res.title);
                    StudentSchool.SetSelection(ScholdPos);
                }
                else
                {

                    StudentSchool.SetSelection(0);
                }
            };


          




            SaveButton.Click +=async (s, e) =>
            {
                if(!(string.IsNullOrEmpty(StudentName.Text)
                && string.IsNullOrEmpty(StudentLastName.Text)
                && string.IsNullOrEmpty(StudentId.Text)
                && string.IsNullOrEmpty(MobilePhone.Text)
                && string.IsNullOrEmpty(StudentClass.Text)
                
                &&string.IsNullOrEmpty(StudentMail.Text)
                ))
                {
                    Startloading(true);
                  await UserControl.Instance.EditStudentprofile(StudentMail.Text, MobilePhone.Text, student.RegionId, StudentVillage.Text, student.SchoolId);
                    StopLoading();
                  
                }
            };

            StopLoading();
        }

        private void BotBackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}