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
using IZrune.PCL;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class MainExamStatisticActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutMainExamStatistic;

        [MapControl(Resource.Id.HeaderStatisticTab)]
        TabLayout Tabs;

        [MapControl(Resource.Id.StatisticPager)]
        ViewPager pager;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout backButton;

        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>() {
          new ExamStatisticFragment() , new DiagramFragment()
        };

        private List<string> Headers = new List<string>()
        {
            "შედეგები","დიაგრამები"
        };



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCore.Instance.InitServices();

            var adapter = new TabAdapter(SupportFragmentManager, FrmList, Headers);
            ResultPagePagerAdapter PagerAdapter = new ResultPagePagerAdapter(SupportFragmentManager, FrmList, Headers);

            Tabs.SetupWithViewPager(pager);
            pager.Adapter = PagerAdapter;

            backButton.Click += (s, e) =>
            {
                OnBackPressed();
            };

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

    }
}