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
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class InnerIndividualFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; }= Resource.Layout.layoutIndividual;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.BodyContent)]
        LinearLayout Body;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;


        [MapControl(Resource.Id.BotBackButton)]
        LinearLayout BotbackButto;

        private int StudentId;
        IPrice prices;


        public InnerIndividualFragment(List<IPrice> prices,int studentId)
        {
            PriceList = prices;
            StudentId = studentId;
        }

        private List<IPrice> PriceList = new List<IPrice>();

        private List<View> ServiceViews = new List<View>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }


        bool IsChec = false;

       

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ServiceViews.Clear();

            BotbackButto.Visibility = ViewStates.Gone;
            foreach (var items in PriceList)
            {
                var Vw = LayoutInflater.Inflate(Resource.Layout.ItemIndividualList, null);

                Vw.FindViewById<TextView>(Resource.Id.TimeTxt).Text = items.MonthCount.ToString()+" თვე";
                Vw.FindViewById<TextView>(Resource.Id.SaleTXt).Visibility = ViewStates.Gone;
                Vw.FindViewById<TextView>(Resource.Id.PriceText).Text = items.price.ToString() + " ₾";
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
                Startloading();
                IzruneHellper.Instance.CurrentStudentAmount = prices.price.Value;
                UserControl.Instance.CurrentStudent.Amount = prices.price.Value;
                UserControl.Instance.CurrentStudent.PackageMonthCount = prices.MonthCount.Value;
             await UserControl.Instance.ReNewPack(UserControl.Instance.CurrentStudent);
                Intent intent = new Intent(this, typeof(ActivityPaymentCategory));
                intent.PutExtra("Inner", "sddsd");

                StartActivity(intent);
                StopLoading();
                IsChec = false;
            }
            else
            {
                Toast.MakeText(this, "გთხოვთ აირჩიოთ მომსახურების პაკეტი", ToastLength.Long).Show();
            }
        }

        private async void Vw_Click(object sender, EventArgs e)
        {
            foreach (var Items in ServiceViews)
            {
                Items.FindViewById<FrameLayout>(Resource.Id.CurrentFrame).SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            }

           (sender as View).FindViewById<FrameLayout>(Resource.Id.CurrentFrame).SetBackgroundResource(Resource.Drawable.ServiceButtonBack);

            var Index = ServiceViews.IndexOf((sender as View));

 
             prices = PriceList.ElementAt(Index);

           



           // UserControl.Instance.SetPromoPack(Result.months, Result.price);
            IsChec = true;

        }
        private int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

    }
}