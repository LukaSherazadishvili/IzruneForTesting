using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Fragments;
using Java.Lang;

namespace Izrune.Adapters.ViewPagerAdapter
{
            class ServiceViewPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {

             private List<MPDCBaseFragment> HoroscopeMainPageFragmentList;
           // private List<string> ListHeaders;
            public override int Count => HoroscopeMainPageFragmentList.Count();

            public ServiceViewPagerAdapter(Android.Support.V4.App.FragmentManager fm, List<MPDCBaseFragment> lst) : base(fm)
            {
                HoroscopeMainPageFragmentList = lst;
               // ListHeaders = HeaderLst;
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return HoroscopeMainPageFragmentList.ElementAt(position);
            }



            //public override ICharSequence GetPageTitleFormatted(int position)
            //{
            //    return new Java.Lang.String(ListHeaders.ElementAt(position));
            //}

        }
}