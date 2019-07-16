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

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class ExamTestActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutExamTest;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.PartTimeExamButton)]
        LinearLayout ExamPartTimeButton;

        [MapControl(Resource.Id.FullTimeExamButton)]
        LinearLayout FullTimeExamButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            BackButton.Click += BackButton_Click;

            ExamPartTimeButton.Click += ExamPartTimeButton_Click;
            FullTimeExamButton.Click += FullTimeExamButton_Click;

            var transcation = FragmentManager.BeginTransaction();
            ExamDialogFragment dialog = new ExamDialogFragment();
            dialog.Show(transcation, "Image Dialog");
        }

        private void FullTimeExamButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SmsCodeActivity));
            intent.PutExtra("TimeType", "1");
            intent.PutExtra("ExamType", "1");
            StartActivity(intent);
        }

        private void ExamPartTimeButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SmsCodeActivity));
            intent.PutExtra("TimeType", "0");
            intent.PutExtra("ExamType", "1");
            StartActivity(intent);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}