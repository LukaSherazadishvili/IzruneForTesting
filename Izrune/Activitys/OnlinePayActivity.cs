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
using Android.Webkit;
using Android.Widget;
using Izrune.Attributes;
using Izrune.WebViewClasses;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;
using Java.Lang;

namespace Izrune.Activitys
{
 


    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class OnlinePayActivity : MPDCBaseActivity
    {
        IPay CurrentPay;


      

        protected override int LayoutResource { get; } = Resource.Layout.LayoutOnlinePay;


        [MapControl(Resource.Id.OnlinePayWebView)]
        WebView MWebView;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        private static string MainUrl = "";
        OnlinePayWebView WebViewclnt;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var Result = Intent.GetStringExtra("PayemantString");
           

            CurrentPay = UserControl.Instance.GetPaymentInformation();


            string Url = CurrentPay.CurrentUserPayURl;

            if (!string.IsNullOrEmpty(Result))
            await UserControl.Instance.ReNewPack(UserControl.Instance.CurrentStudent);
            else
            await UserControl.Instance.ReNewPack();


            MainUrl = CurrentPay.CurrentUserPayURl;
            
            WebViewclnt = new OnlinePayWebView()
            {
                //clientPayment = CurrentPay,
                LoadUrl = MainUrl,
                ChangeActyvity = (() =>
                {

                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);

                })
            };

            MWebView.Settings.JavaScriptEnabled = true;
            MWebView.SetWebViewClient(WebViewclnt);



            //if (Url == "http://www.izrune.ge/")
            //{
            //    MWebView.LoadUrl(MainUrl);
            //}
            //else
            //{
            //    MWebView.LoadUrl(MainUrl);
            //}

            MWebView.LoadUrl(MainUrl);



            BackButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
           // MWebView.LoadUrl("");
            //Intent intent = new Intent(this, typeof(ActivityPaymentCategory));
            //intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            //StartActivity(intent);
            this.Finish();
          
        }
    }



    
}