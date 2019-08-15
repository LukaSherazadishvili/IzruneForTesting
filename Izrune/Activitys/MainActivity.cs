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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Analytics;
using Firebase.Messaging;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Fragments;
using Izrune.Fragments.DialogFrag;
using Izrune.Helpers;
using IZrune.PCL;
using static System.Net.Mime.MediaTypeNames;

namespace Izrune.Activitys
{
    [Activity(Label = "izrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, Icon ="@drawable/logo",MainLauncher = true)]
    class MainActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutMainIncomePage;

        [MapControl(Resource.Id.nav_view)]
        NavigationView navigationView;

        [MapControl(Resource.Id.mainPageContainer)]
        FrameLayout MainContainer;

        [MapControl(Resource.Id.drawer_layout)]
        DrawerLayout drawer;

        [MapControl(Resource.Id.hamburgerBtnMainActivity)]
        FrameLayout Hamburger;

        [MapControl(Resource.Id.HeaderText)]
        TextView HeaderText;

       

        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           

            AppCore.Instance.InitServices();
            var frbase = FirebaseAnalytics.GetInstance(this);

           // FirebaseMessaging.Instance.SubscribeToTopic("all");

            ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            Hamburger.Click += Hamburger_Click;

            var header = navigationView.GetHeaderView(0);
            header.FindViewById<ImageView>(Resource.Id.MenuBackButton).Click += (s, e) =>
            {
                drawer.CloseDrawer(navigationView);
            };
        }
        private void Hamburger_Click(object sender, EventArgs e)
        {
           drawer.OpenDrawer(navigationView);
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.GroupId)
            {
                case Resource.Id.mainGroup:
                    {
                        HeaderText.Text = "";
                        ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginNews:
                    {
                        HeaderText.Text = "საგანმანათლებლო სიახლეები";
                        ChangeFragmentPage(new NewsFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginGetInfo:
                    {
                        HeaderText.Text = "გაიგეთ მეტი";
                        ChangeFragmentPage(new GetMoreInfoFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginContact:
                    {
                        HeaderText.Text = "კონტაქტი";
                        ChangeFragmentPage(new ContactFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }

            }
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            //Intent intent = new Intent(this, typeof(LogInActivity));
            //StartActivity(intent);
            this.Finish();
        }


    }
}