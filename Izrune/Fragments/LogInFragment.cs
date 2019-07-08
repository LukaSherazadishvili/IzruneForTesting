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
using Izrune.Attributes;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class LogInFragment : MPDCBaseFragment
    {

        protected override int LayoutResource { get; } = Resource.Layout.layoutLogIn;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

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



        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            base.OnCreate(savedInstanceState);


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

            LoginButton.Click += async (s, e) =>
            {
                Startloading();
                var result = await UserControl.Instance.LogInUser(UserName.Text, Password.Text);

                if (result)
                {
                    var Result = await UserControl.Instance.GetCurrentUser();
                    var intentt = new Intent(this, typeof(MainPageAtivity));
                    StartActivity(intentt);
                }
                StopLoading();

            };
        }


    }
}