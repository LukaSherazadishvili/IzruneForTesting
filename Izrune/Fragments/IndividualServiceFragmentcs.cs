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
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class IndividualServiceFragmentcs : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutIndividual;

        [MapControl(Resource.Id.BodyContent)]
        LinearLayout Body;
        

        public IndividualServiceFragmentcs(List<IPrice> prices)
        {
            PriceList = prices;
        }

        private List<IPrice> PriceList = new List<IPrice>();

      

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public  override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

           

            foreach (var items in PriceList)
            {
                var Vw = LayoutInflater.Inflate(Resource.Layout.ItemIndividualList, null);

                Vw.FindViewById<TextView>(Resource.Id.TimeTxt).Text = items.months.ToString()+" თვე";
                Vw.FindViewById<TextView>(Resource.Id.SaleTXt).Visibility = ViewStates.Gone;
                Vw.FindViewById<TextView>(Resource.Id.PriceText).Text = items.price.ToString();

                Body.AddView(Vw);




            }
        }
    }
}