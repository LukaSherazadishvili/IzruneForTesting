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
    class NextRegistrationParentActyvity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutNextRegistrationParrent;


        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            NextButton.Click += NextButton_Click;

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;
        }

        private void BotBackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegistrationStudentActivity));
            StartActivity(intent);
        }
    }
}