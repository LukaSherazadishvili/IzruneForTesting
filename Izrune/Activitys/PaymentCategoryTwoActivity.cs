using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class PaymentCategoryTwoActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.Sabanko_Gadmoricxvis;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BackButton.Click += (s, e) =>
            {
                OnBackPressed();
            };
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

    }
}