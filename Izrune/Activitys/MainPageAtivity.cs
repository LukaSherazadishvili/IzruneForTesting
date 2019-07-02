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
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Services;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class MainPageAtivity:MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutMainPage;

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

        //[MapControl(Resource.Id.LeftSideBar)]
        //LinearLayout sideBar;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            


            RegistrationServices test = new RegistrationServices();

            var result = await test.GetRegionsAsync();

            ChangeFragmentPage(new MainPageTestFragment(), MainContainer.Id);
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
                        ChangeFragmentPage(new MainPageTestFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.statistic:
                    {
                        HeaderText.Text = "სტატისტიკა/ისტორია";
                       // ChangeFragmentPage(new StatisticFragment(), MainContainer.Id);
                        ChangeFragmentPage(new StatisticHistoryFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.ReUpdate:
                    {
                        HeaderText.Text = "";
                        ChangeFragmentPage(new InnerChangePackFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.profile:
                    {
                        HeaderText.Text = "";
                        ChangeFragmentPage(new ProfileFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.News:
                    {
                        HeaderText.Text = "საგანმანათლებლო სიახლეები";
                        ChangeFragmentPage(new NewsFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.Exit:
                    {
                        UserControl.Instance.Parent = null;
                        //Intent intent = new Intent(this,typeof(LogInActivity));
                        //StartActivity(intent);
                        OnBackPressed();
                        break;
                    }
            }
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            UserControl.Instance.LogOut();
           
            this.Finish();
        }
    }
}