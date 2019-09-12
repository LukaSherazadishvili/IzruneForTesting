using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.ViewPagerAdapter;
using Izrune.Attributes;
using Izrune.Fragments;
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class MainDiplomaActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.MainDiplomaLayout;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.HeaderTab)]
        TabLayout Tabs;

        [MapControl(Resource.Id.ResultPageViePager)]
        ViewPager Pager;

        [MapControl(Resource.Id.ShareButton)]
        LinearLayout ShareButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>() {
          new ResultStatisticFragment(), new ResultQuestionStatisticFragment()
        };

        private List<string> Headers = new List<string>()
        {
            "შედეგები","კითხვები"
        };

       

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);



                var adapter = new TabAdapter(SupportFragmentManager, FrmList, Headers);
                ResultPagePagerAdapter PagerAdapter = new ResultPagePagerAdapter(SupportFragmentManager, FrmList, Headers);

                Tabs.SetupWithViewPager(Pager);
                Pager.Adapter = PagerAdapter;

                BackButton.Click += BackButton_Click;

                var Result = await QuezControll.Instance.GetExamInfoAsync();

                if (!string.IsNullOrEmpty(Result.DiplomaURl))
                {
                    ShareButton.Visibility = ViewStates.Visible;
                    ShareButton.Click += (s, e) =>
                    {
                        var SharingIntent = new Intent();
                        SharingIntent.SetAction(Intent.ActionSend);
                        SharingIntent.SetType("text/plain");
                    //  SharingIntent.PutExtra(Intent.ExtraSubject, "Subject");
                    SharingIntent.PutExtra(Intent.ExtraText, Result.DiplomaURl);
                    // SharingIntent.PutExtra(Intent.ExtraTitle, "Subject");
                    StartActivity(Intent.CreateChooser(SharingIntent, "sharing option"));
                    };
                }


            }
            catch(Exception e)
            {

            }
        }

       

        public void OnBadgetClick()
        {
            var uri = Android.Net.Uri.Parse("http://www.izrune.ge/geo/175");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainPageAtivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
         
            this.Finish();
            
        }


       


    }
}