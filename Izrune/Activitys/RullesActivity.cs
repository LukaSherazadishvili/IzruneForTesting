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
using Com.Airbnb.Lottie;
using Izrune.Attributes;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class RullesActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutEndRegistration;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.Checker)]
        LottieAnimationView checker;

        bool isChecked = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            checker.Progress = 0;
            checker.Click += Checker_Click;

        }

        private void Checker_Click(object sender, EventArgs e)
        {
            if (!isChecked)
            {
                checker.PlayAnimation();

               // checker.Progress = 100;

                isChecked = true;
            }
            else
            {
                checker.Progress = 0;
                isChecked = false;
            }
        }
    }
}