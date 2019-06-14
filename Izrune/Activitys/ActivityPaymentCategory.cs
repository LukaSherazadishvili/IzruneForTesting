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

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ActivityPaymentCategory : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutPaymentCategory;

        [MapControl(Resource.Id.CardPayButton)]
        LinearLayout CardButton;

        [MapControl(Resource.Id.BankPayButton)]
        LinearLayout BankButton;

        [MapControl(Resource.Id.PayBoxButton)]
        LinearLayout PayBocButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CardButton.Click += (s, e) =>
            {

                Intent intent = new Intent(this,typeof(OnlinePayActivity));
                StartActivity(intent);

            };


        }

    }
}