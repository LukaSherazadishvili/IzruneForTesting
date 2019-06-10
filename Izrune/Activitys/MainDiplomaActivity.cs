using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.ViewPagerAdapter;
using Izrune.Attributes;
using Izrune.Fragments;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class MainDiplomaActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.MainDiplomaLayout;

        [MapControl(Resource.Id.HeaderTab)]
        TabLayout Tabs;

        [MapControl(Resource.Id.ResultPageViePager)]
        ViewPager Pager;

        [MapControl(Resource.Id.ShareButton)]
        LinearLayout ShareButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>() {
          new ResultStatisticFragment() , new ResultQuestionStatisticFragment()
        };

        private List<string> Headers = new List<string>()
        {
            "შედეგები","კითხვები"
        };

       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ShareButton.Visibility = ViewStates.Visible;

            var adapter = new TabAdapter(SupportFragmentManager, FrmList, Headers);
            ResultPagePagerAdapter PagerAdapter = new ResultPagePagerAdapter(SupportFragmentManager, FrmList, Headers);

            Tabs.SetupWithViewPager(Pager);
            Pager.Adapter = PagerAdapter;

            BackButton.Click += BackButton_Click;


        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainPageAtivity));
            StartActivity(intent);
        }

    }
}