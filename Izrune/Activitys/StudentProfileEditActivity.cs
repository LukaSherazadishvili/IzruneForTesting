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
using IZrune.PCL.Helpers;

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



        protected override int LayoutResource { get; } = Resource.Layout.layoutStudentProfile;

        IStudent student;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

            var Result = await UserControl.Instance.GetCurrentUser();

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
              Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());

            StudentSpiner.Adapter = DataAdapter;

            StudentSpiner.ItemSelected += (s, e) =>
            {

                student = Result.Students.ElementAt(e.Position);

                StudentName.Text = student.Name;
                StudentLastName.Text = student.LastName;
                StudentId.Text = student.PersonalNumber;
                MobilePhone.Text = student.Phone;

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