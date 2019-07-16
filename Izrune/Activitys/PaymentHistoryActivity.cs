using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;


namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class PaymentHistoryActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutPaymentHistory;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

        [MapControl(Resource.Id.HistoryRecycler)]
        RecyclerView HistoryRecycler;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Startloading();

            var Result = await MpdcContainer.Instance.Get<IPaymentService>().GetPaymentHistory();

            var adapter = new PaymentRecyclerAdapter(Result?.ToList());

            HistoryRecycler.SetLayoutManager(new LinearLayoutManager(this));
            HistoryRecycler.SetAdapter(adapter);


            BackButton.Click += (s, e) =>
            {
                OnBackPressed();
            };

            StopLoading();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

    }
}