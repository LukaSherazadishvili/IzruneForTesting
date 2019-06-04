using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Izrune.Fragments;
using Java.Lang;

namespace Izrune.Adapters.ViewPagerAdapter
{
    class TabAdapter:FragmentPagerAdapter
    {
        private List<MPDCBaseFragment> FragmentList;
        private List<string> HeaderList;

        public TabAdapter(Android.Support.V4.App.FragmentManager frm, List<MPDCBaseFragment> FrmList, List<string> HeaderTextList) : base(frm)
        {
            FragmentList = FrmList;
            HeaderList = HeaderTextList;
        }

        public override int Count => FragmentList.Count();

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return FragmentList.ElementAt(position);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(HeaderList.ElementAt(position));
        }
    }
}
   