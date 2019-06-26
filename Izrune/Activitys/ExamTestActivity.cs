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
        }

        private void FullTimeExamButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(QuezActivity));
            intent.PutExtra("TimeType", "1");
            intent.PutExtra("ExamType", "1");
            StartActivity(intent);
        }

        private void ExamPartTimeButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(QuezActivity));
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