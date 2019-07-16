using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
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

        [MapControl(Resource.Id.drawer_layout)]
        DrawerLayout drawer;

        [MapControl(Resource.Id.hamburgerBtnMainActivity)]
        FrameLayout Hamburger;

        [MapControl(Resource.Id.HeaderText)]
        TextView HeaderText;

        [MapControl(Resource.Id.LogInNavigation)]
        NavigationView navigationView;


        protected  override void OnCreate(Bundle savedInstanceState)
        {
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

            Hamburger.Click += Hamburger_Click;
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {

            switch (e.MenuItem.GroupId)
            {
                case Resource.Id.mainGroup:
                    {
                        HeaderText.Text = "";
                      //  ChangeFragmentPage(new MainPageTestFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginNews:
                    {
                        HeaderText.Text = "სიახლეები";
                        // ChangeFragmentPage(new StatisticFragment(), MainContainer.Id);
                     //   ChangeFragmentPage(new StatisticHistoryFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginGetInfo:
                    {
                        HeaderText.Text = "გაიგეთ მეტი";
                     //   ChangeFragmentPage(new InnerChangePackFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginContact:
                    {
                        HeaderText.Text = "კონტაქტი";
                      //  ChangeFragmentPage(new ProfileFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
              
            }


        }

        private void Hamburger_Click(object sender, EventArgs e)
        {
            drawer.OpenDrawer(navigationView);
        }
    }
}