using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using Izrune.Fragments;

namespace Izrune.Activitys
{
    class MainDiplomaActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.MainDiplomaLayout;

        [MapControl(Resource.Id.HeaderTab)]
        TableLayout Tabs;

        [MapControl(Resource.Id.ResultPageViePager)]
        ViewPager Pager;



        private List<MPDCBaseFragment> FrmList = new List<MPDCBaseFragment>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



        }


    }
}