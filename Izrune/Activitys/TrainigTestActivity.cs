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
using Izrune.Fragments.DialogFrag;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class TrainigTestActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutTrainingTest;


        [MapControl(Resource.Id.ExamTestFullTimeButton)]
        LinearLayout ExamTestFullTimeButton;

        [MapControl(Resource.Id.ExamPartTimeButton)]
        LinearLayout ExamPartTimeButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ExamTestFullTimeButton.Click += ExamTestFullTimeButton_Click;

            ExamPartTimeButton.Click += ExamPartTimeButton_Click;
           BackButton.Click += BackButton_Click;

            var transcation = FragmentManager.BeginTransaction();
            ExamDialogFragment dialog = new ExamDialogFragment();
            dialog.Show(transcation, "Image Dialog");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private async void ExamPartTimeButton_Click(object sender, EventArgs e)
        {
          

            Intent intent = new Intent(this, typeof(QuezActivity));
            intent.PutExtra("TimeType", "0");
            intent.PutExtra("ExamType", "0");
            StartActivity(intent);
        }

        private async void ExamTestFullTimeButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(QuezActivity));
            intent.PutExtra("TimeType", "1");
            intent.PutExtra("ExamType", "0");
           
            StartActivity(intent);
        }
    }
}