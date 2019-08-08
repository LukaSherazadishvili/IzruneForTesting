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
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.ViewPagerAdapter;
using Izrune.Attributes;
using Izrune.Fragments;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class InnerDiplomaStatisticActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.MainDiplomaLayout;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }

        [MapControl(Resource.Id.HeaderTab)]
        TabLayout Tabs;

        [MapControl(Resource.Id.ResultPageViePager)]
        ViewPager Pager;

        [MapControl(Resource.Id.ShareButton)]
        LinearLayout ShareButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>();
      
        private List<string> Headers = new List<string>()
        {
            "შედეგები","კითხვები"
        };



        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetCurrentTestDiplomaInfo(IzruneHellper.Instance.CurrentStatistic.Id);
          
            FrmList.Add(new InnerResultStatisticFragment() { Result = Result });
            FrmList.Add(new InnerResultQuestionStatisticFragment());

            var adapter = new TabAdapter(SupportFragmentManager, FrmList, Headers);
            ResultPagePagerAdapter PagerAdapter = new ResultPagePagerAdapter(SupportFragmentManager, FrmList, Headers);

            Tabs.SetupWithViewPager(Pager);
            Pager.Adapter = PagerAdapter;

            BackButton.Click += BackButton_Click;

           


            if (!string.IsNullOrEmpty(IzruneHellper.Instance.CurrentStatistic.DiplomaUrl))
            {
                ShareButton.Visibility = ViewStates.Visible;
                ShareButton.Click += (s, e) =>
                {
                    var SharingIntent = new Intent();
                    SharingIntent.SetAction(Intent.ActionSend);
                    SharingIntent.SetType("text/plain");
                    //  SharingIntent.PutExtra(Intent.ExtraSubject, "Subject");
                    SharingIntent.PutExtra(Intent.ExtraText, IzruneHellper.Instance.CurrentStatistic.DiplomaUrl);
                    // SharingIntent.PutExtra(Intent.ExtraTitle, "Subject");
                    StartActivity(Intent.CreateChooser(SharingIntent, "sharing option"));
                };
            }



        }



        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public void OnBadgetClick()
        {
            var uri = Android.Net.Uri.Parse("http://www.izrune.ge/geo/175");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }
        public void OnEgmuClick(string Egmu)
        {
            var uri = Android.Net.Uri.Parse(Egmu);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
           
            this.Finish();
        }





    }
}
