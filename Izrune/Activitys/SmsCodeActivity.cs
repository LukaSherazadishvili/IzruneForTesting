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
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class SmsCodeActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutSmsCode;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }

        [MapControl(Resource.Id.GetSmsCodeButton)]
        LinearLayout GetSmsCode;

        [MapControl(Resource.Id.AgreeButton)]
        LinearLayout AgreeButton;

        [MapControl(Resource.Id.SmsCodeEdiTxt)]
        EditText SmsEditext;


        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButto;





        string SmsCode;
        private string ExamType;
        private string TimeType;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            ExamType = Intent.GetStringExtra("ExamType");
            TimeType = Intent.GetStringExtra("TimeType");

            AgreeButton.Click += AgreeButton_Click;

            SmsEditext.TextChanged += (s, e) =>
            {
                SmsEditext.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            };

            GetSmsCode.Click +=async (s, e) =>
            {

                Startloading(true);

             var  Result =await  QuezControll.Instance.GetSmsCode();
                SmsCode = Result.ToString();

                StopLoading();
            };

            BackButto.Click += BackButto_Click;

        }

        private void BackButto_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            this.Finish();
        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            if (SmsCode == SmsEditext.Text)
            {
                Intent intent = new Intent(this,typeof(QuezActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                intent.PutExtra("TimeType", TimeType);
                intent.PutExtra("ExamType", ExamType);
                StartActivity(intent);
            }
            else
            {
                SmsEditext.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
        }
    }
}