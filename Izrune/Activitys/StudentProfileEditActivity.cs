using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class StudentProfileEditActivity : MPDCBaseActivity
    {
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
        TextView StudentSchool;

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

        protected override int LayoutResource { get; } = Resource.Layout.layoutStudentProfile;

        IStudent student;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

            var Result = await UserControl.Instance.GetCurrentUser();
            var Regions = await MpdcContainer.Instance.Get<IRegistrationServices>().GetRegionsAsync();

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
              Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());

            StudentSpiner.Adapter = DataAdapter;


            var RegionAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
           Regions.Select(i => i.title).ToList());

            StudentCity.Adapter = RegionAdapter;

           

            StudentSpiner.ItemSelected += (s, e) =>
            {

                student = Result.Students.ElementAt(e.Position);

                StudentName.Text = student.Name;
                StudentLastName.Text = student.LastName;
                StudentId.Text = student.PersonalNumber;
                MobilePhone.Text = student.Phone;
                StudentClass.Text = student.Class.ToString();
                

               // StudentSchool.Text = Regions.FirstOrDefault(i => i.id == student.RegionId).Schools.Where(i => i.id == student.SchoolId).FirstOrDefault().title;
                StudentMail.Text = student.Email;
                StudentVillage.Text = student.Village;

            };
            bool isCheck = false;
            StudentCity.ItemSelected += (s, e) =>
            {
                if (isCheck)
                {
                    student.RegionId = Regions.ElementAt(e.Position).id;
                    isCheck = true;
                }
            };

            var reg = Regions.Where(i => i.id == student.RegionId).FirstOrDefault();

           var pos= RegionAdapter.GetPosition(reg.title);
            StudentCity.SetSelection(pos);


            SaveButton.Click +=async (s, e) =>
            {
                if(!(string.IsNullOrEmpty(StudentName.Text)
                && string.IsNullOrEmpty(StudentLastName.Text)
                && string.IsNullOrEmpty(StudentId.Text)
                && string.IsNullOrEmpty(MobilePhone.Text)
                && string.IsNullOrEmpty(StudentClass.Text)
                && string.IsNullOrEmpty(StudentSchool.Text)
                &&string.IsNullOrEmpty(StudentMail.Text)
                ))
                {
                  await UserControl.Instance.EditStudentprofile(StudentMail.Text, MobilePhone.Text, student.RegionId, StudentVillage.Text, student.SchoolId);

                    Toast.MakeText(this, "წარმატებით მოხდა პროფილის განახლება  ", ToastLength.Long).Show();
                }
            };


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
        }
    }
}