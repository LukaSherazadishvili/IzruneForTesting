using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using Izrune.Fragments;
using IZrune.PCL;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = true)]
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

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCore.Instance.InitServices();


            ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            Hamburger.Click += Hamburger_Click;


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