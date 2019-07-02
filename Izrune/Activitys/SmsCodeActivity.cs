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
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class SmsCodeActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutSmsCode;

        [MapControl(Resource.Id.GetSmsCodeButton)]
        LinearLayout GetSmsCode;

        [MapControl(Resource.Id.AgreeButton)]
        LinearLayout AgreeButton;

        [MapControl(Resource.Id.SmsCodeEdiTxt)]
        EditText SmsEditext;


        string SmsCode;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AgreeButton.Click += AgreeButton_Click;

            SmsEditext.TextChanged += (s, e) =>
            {
                SmsEditext.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            };

            GetSmsCode.Click +=async (s, e) =>
            {

             var  Result =await  QuezControll.Instance.GetSmsCode();
                SmsCode = Result.ToString();
            };

        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            if (SmsCode == SmsEditext.Text)
            {
                Intent intent = new Intent(this,typeof(QuezActivity));
                StartActivity(intent);
            }
            else
            {
                SmsEditext.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
        }
    }
}