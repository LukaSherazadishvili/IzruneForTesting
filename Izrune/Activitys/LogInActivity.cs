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
using IZrune.PCL;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = true)]
    class LogInActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutLogIn;

        [MapControl(Resource.Id.ForgotPassword)]
        TextView Forgotpas;

        [MapControl(Resource.Id.forgotUserName)]
        TextView ForgottUser;

        [MapControl(Resource.Id.LoginButton)]
        LinearLayout LoginButton;

        [MapControl(Resource.Id.ButtonRegistration)]
        LinearLayout RegistrationButton;

        [MapControl(Resource.Id.UserName)]
        EditText UserName;

        [MapControl(Resource.Id.Password)]
        EditText Password;


        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCore.Instance.InitServices();

            Intent intent = new Intent(this, typeof(ForgotPasswordOrUserNameActivity));
            Forgotpas.Click += (s, e) =>
            {
                intent.PutExtra("IsPasswordOrNot", "true");
                StartActivity(intent);
            };
            ForgottUser.Click += (s, e) =>
            {
                intent.PutExtra("IsPasswordOrNot", "false");
                StartActivity(intent);
            };

            RegistrationButton.Click += (s, e) =>
            {
                Intent registrationIntent = new Intent(this, typeof(RegistrationActivity));
                StartActivity(registrationIntent);

            };

            LoginButton.Click += async(s, e) =>
            {
             var result=await  UserControl.Instance.LogInUser(UserName.Text,Password.Text);

                if (result)
                {
                    var  intentt = new Intent(this,typeof(MainPageAtivity));
                    StartActivity(intentt);
                }
                else
                {
                    Toast.MakeText(this, "გთხოვთ სწორად შეიყვანოთ ინფორმაცია", ToastLength.Long).Show();
                }

            };
        }
    }
}