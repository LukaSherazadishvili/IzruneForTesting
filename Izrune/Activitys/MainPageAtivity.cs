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
using Izrune.Fragments;
using Izrune.Fragments.DialogFrag;
using IZrune.PCL;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Services;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class MainPageAtivity:MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutMainPage;

        [MapControl(Resource.Id.MainPageContainer)]
        protected override FrameLayout MainFrame { get ; set ; }

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

        protected  override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                Startloading();

                var PageMaper = Intent.GetStringExtra("FromAddStudent");

                if (string.IsNullOrEmpty(PageMaper))
                {
                    ChangeFragmentPage(new MainPageTestFragment(), MainContainer.Id);
                    navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
                    Hamburger.Click -= Hamburger_Click;
                    navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
                    Hamburger.Click += Hamburger_Click;
                }
                else
                {
                    navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
                    Hamburger.Click -= Hamburger_Click;
                    navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
                    Hamburger.Click += Hamburger_Click;
                    HeaderText.Text = "";
                    ChangeFragmentPage(new InnerChangePackFragment() { CurrentStudentPosition = UserControl.Instance.Parent.Students.Count() - 1 }, MainContainer.Id);
                    drawer.CloseDrawers();

                    //  ChangePage(UserControl.Instance.Parent.Students.Count() - 1);
                }

                var Result = await UserControl.Instance.GetCurrentUser();

                var header = navigationView.GetHeaderView(0);




                header.FindViewById<TextView>(Resource.Id.UserNameLastNametxt).Text = $"{Result.Name} {Result.LastName}";
                header.FindViewById<TextView>(Resource.Id.ProfileNumber).Text = $"{Result.ProfileNumber}";
                header.FindViewById<ImageView>(Resource.Id.NavBackButton).Click += (s, e) =>
                {
                    drawer.CloseDrawer(navigationView);
                };
                StopLoading();
            }
            catch(Exception ex)
            {

            }
            //FullNametxt.Text = ;
            //ProfNumber.Text = $"{Result.ProfileNumber}";

        }

        private void Hamburger_Click(object sender, EventArgs e)
        {
            drawer.OpenDrawer(navigationView);
            CloseKeyboard();
        }


        public void ShowMyDialog(string title,string text)
        {
            try
            {
                var transcation = FragmentManager.BeginTransaction();
                warningDialogFragment dialog = new warningDialogFragment(title, text, true);
                dialog.Show(transcation, "Image Dialog");
            }
            catch(Exception ex)
            {

            }
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
                       
                        OnBackPressed();
                        break;
                    }
            }
        }


        public void ChangePage(int position)
        {
            try
            {

                var transcation = FragmentManager.BeginTransaction();
                MainPageAlertDialog dialog = new MainPageAlertDialog()
                {
                    ChangeFragment = () =>
                    {

                        HeaderText.Text = "";
                        ChangeFragmentPage(new InnerChangePackFragment() { CurrentStudentPosition = position }, MainContainer.Id);
                        drawer.CloseDrawers();

                    }
                };
                dialog.Show(transcation, "Image Dialog");
            }
            catch(Exception ex)
            {

            }
        }

      

        public override void OnBackPressed()
        {
            UserControl.Instance.LogOut();
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            UserControl.Instance.Resetregistration();
            StartActivity(intent);

            this.Finish();
        }
    }
}