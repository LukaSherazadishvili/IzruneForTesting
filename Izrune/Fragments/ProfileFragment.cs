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
using Izrune.Activitys;
using Izrune.Activitys.InnerActivity;
using Izrune.Attributes;
using IZrune.PCL;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class ProfileFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutChangeProfile;

        [MapControl(Resource.Id.ParentProfileEdit)]
        RelativeLayout ParentButton;

        [MapControl(Resource.Id.StudentProfileEdit)]
        RelativeLayout StudentEdit;

        [MapControl(Resource.Id.AddStudentButton)]
        LinearLayout AddStudent;

        [MapControl(Resource.Id.ChangePasswordButton)]
        RelativeLayout ChangePassword;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ParentButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(ParrentProfileEditActivity));
                StartActivity(intent);
            };

            StudentEdit.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(StudentProfileEditActivity));
                StartActivity(intent);
            };


           
                AddStudent.Click += AddStudent_Click;
           
            ChangePassword.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(ChangePasswordActivity));
                StartActivity(intent);
            };
        }

        private void AddStudent_Click(object sender, EventArgs e)
        {
            if (UserControl.Instance.Parent.Students.Count() < 11)
            {
                Intent intent = new Intent(this, typeof(InnerRegisterStudent));
                StartActivity(intent);
            }
            else
                AppCore.Instance.Alertdialog.ShowAlerDialog("შეცდომა", "10-ზე მეტი მოსწავლის რეგისტრაცია ერთ პროფილზე არ არის შესაძლებელი");


        }
    }
}