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
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class IndividualServiceFragmentcs : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutIndividual;

        [MapControl(Resource.Id.BodyContent)]
        LinearLayout Body;
        class test
        {
            public string Time { get; set; }
            public string Sale { get; set; }
            public string Price { get; set; }
        }


        private List<test> Salelist = new List<test>()
        {
            new test(){Time="1 თვე",Sale="27 ₾" ,Price="27 ₾"},
            new test(){Time="1 თვე",Sale="27 ₾" ,Price="27 ₾"},
            new test(){Time="1 თვე",Sale="27 ₾" ,Price="27 ₾"},
             new test(){Time="1 თვე",Sale="27 ₾" ,Price="27 ₾"},
           
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            foreach (var items in Salelist)
            {
                var Vw = LayoutInflater.Inflate(Resource.Layout.ItemIndividualList, null);

                Vw.FindViewById<TextView>(Resource.Id.TimeTxt).Text = items.Time;
                Vw.FindViewById<TextView>(Resource.Id.SaleTXt).Text = items.Sale;
                Vw.FindViewById<TextView>(Resource.Id.PriceText).Text = items.Price;

                Body.AddView(Vw);




            }
        }
    }
}