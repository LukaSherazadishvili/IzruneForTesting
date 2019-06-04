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
using Izrune.Activitys;
using Izrune.Adapters.ViewPagerAdapter;
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class DiplomaFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.MainDiplomaLayout;

        [MapControl(Resource.Id.HeaderTab)]
        TabLayout Tabs;

        [MapControl(Resource.Id.ResultPageViePager)]
        ViewPager Pager;

        


        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>() {
            new ResultQuestionStatisticFragment(),new ResultStatisticFragment()
        };

        private List<string> Headers = new List<string>()
        {
            "შედეგები","კითხვები"
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            
            var adapter = new TabAdapter((Activity as QuezActivity).SupportFragmentManager, FrmList, Headers);
            ResultPagePagerAdapter PagerAdapter = new ResultPagePagerAdapter((Activity as QuezActivity).SupportFragmentManager, FrmList,Headers);

            Tabs.SetupWithViewPager(Pager);
            Pager.Adapter = PagerAdapter;
        }
    }
}