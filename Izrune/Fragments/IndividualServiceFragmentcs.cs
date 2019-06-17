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
using Izrune.Activitys;
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

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        public IndividualServiceFragmentcs(List<IPrice> prices)
        {
            PriceList = prices;
        }

        private List<IPrice> PriceList = new List<IPrice>();

        private List<View> ServiceViews = new List<View>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }


        bool IsChec=false;

        public  override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ServiceViews.Clear();
           

            foreach (var items in PriceList)
            {
                var Vw = LayoutInflater.Inflate(Resource.Layout.ItemIndividualList, null);

                Vw.FindViewById<TextView>(Resource.Id.TimeTxt).Text = items.months.ToString()+" თვე";
                Vw.FindViewById<TextView>(Resource.Id.SaleTXt).Visibility = ViewStates.Gone;
                Vw.FindViewById<TextView>(Resource.Id.PriceText).Text = items.price.ToString()+ " ₾";
                ServiceViews.Add(Vw);
                Body.AddView(Vw);

                Vw.Click += Vw_Click;
               

            }
            NextButton.Click += NextButton_Click;
        }

        private async void NextButton_Click(object sender, EventArgs e)
        {
            //await UserControl.Instance.FinishRegistration();
            if (IsChec)
            {
                Intent intent = new Intent(this, typeof(RullesActivity));
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "გთხოვთ აირჩიოთ მომსახურების პაკეტი", ToastLength.Long).Show();
            }
        }

        private void Vw_Click(object sender, EventArgs e)
        {
            foreach(var Items in ServiceViews)
            {
                Items.FindViewById<FrameLayout>(Resource.Id.CurrentFrame).SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            }

           (sender as View).FindViewById<FrameLayout>(Resource.Id.CurrentFrame).SetBackgroundResource(Resource.Drawable.ServiceButtonBack);

            var Index = ServiceViews.IndexOf((sender as View));

          var Result=PriceList.ElementAt(Index);

            UserControl.Instance.SetPromoPack(Result.months,Result.price);
            IsChec = true;
            
        }
    }
}