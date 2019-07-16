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
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{

    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class OnlinePayActivity : MPDCBaseActivity
    {
        IPay CurrentPay;


        public class MyWebViewClient : WebViewClient
        {
          public IPay clientPayment { get; set; }
            public Action ChangeActyvity { get; set; }
          
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {

                if (url == "http://www.izrune.ge/")
                    {
                    ChangeActyvity?.Invoke();
                }
                else
                {
                    view.LoadUrl(url);
                    
                }
              
            

                return true;
            }
            public override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
            {
                base.OnPageStarted(view, url, favicon);
            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
            }
            public override void OnReceivedError(WebView view, [GeneratedEnum] ClientError errorCode, string description, string failingUrl)
            {
                base.OnReceivedError(view, errorCode, description, failingUrl);
            }




        }

        protected override int LayoutResource { get; } = Resource.Layout.LayoutOnlinePay;


        [MapControl(Resource.Id.OnlinePayWebView)]
        WebView MWebView;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        private static string MainUrl = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          


            CurrentPay = UserControl.Instance.GetPaymentInformation();
            string Url = CurrentPay.CurrentUserPayURl;

            if (MainUrl == "")
            {
                MainUrl = CurrentPay.CurrentUserPayURl;
            }


            MWebView.Settings.JavaScriptEnabled = true;
            MWebView.SetWebViewClient(new MyWebViewClient()
            {
                clientPayment = CurrentPay,
                ChangeActyvity = (() =>
                {
                    Intent intent = new Intent(this, typeof(LogInActivity));
                    StartActivity(intent);

                })
            });

            if (Url == "http://www.izrune.ge/")
            {
                MWebView.LoadUrl(MainUrl);
            }
            else
            {
                MWebView.LoadUrl(Url);
            }
           

            BackButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            
            this.Finish();
        }
    }



    
}