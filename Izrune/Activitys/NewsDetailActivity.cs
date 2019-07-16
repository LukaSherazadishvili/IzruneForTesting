using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using FFImageLoading.Views;
using Izrune.Attributes;
using Izrune.Helpers;

namespace Izrune.Activitys
{
    public class MyWebViewClient : WebViewClient
    {
      
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {

                view.LoadUrl(url);
       
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

    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class NewsDetailActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.NewsDetailLayout;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }


        [MapControl(Resource.Id.MainImage)]
        ImageViewAsync MainImage;

        [MapControl(Resource.Id.MainHeaderText)]
        TextView HeaderText;

        [MapControl(Resource.Id.DateText)]
        TextView DateText;

        [MapControl(Resource.Id.MainWebContent)]
        WebView MainContent;

        [MapControl(Resource.Id.NewsDetailRecyclerView)]
        RecyclerView ImageRecycler;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout backButton;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Startloading();
            var Result = IzruneHellper.Instance.CurrentNews;
            MainImage.LoadImage(Result.ImageUrl);
            HeaderText.Text = Result.Title;
            DateText.Text = Result.date.ToShortDateString();

            MainContent.Settings.JavaScriptEnabled = true;
            MainContent.SetWebViewClient(new MyWebViewClient()
            {
              
            });

            if (!string.IsNullOrEmpty(Result.Content))
            {
                var fontPath = "@font-face {font-family: MyFont;src: url(\"file:///android_asset/BPG_ARIAL_0.ttf\")}";

                var htmlContent = $@"<html><head><style> {fontPath} *{"{ margin-left: 0px!important; margin-right: 0px!important; padding-left : 0px!important; padding-right : 0px!important; width: 100%!important;}"} iframe{{ margin-left : 0px!important; margin-right : 0px!important; margin-top : 0px!important; margin-bottom : 0px!important;}} {"td{width: 50%!important; text-align:left!important; height: auto!important; }"} div{"{text-align: left!important;background-color: transparent;}"} p{"{text-align: left!important;font-family:MyFont!important; hyphens: auto!important;-webkit-hyphens: auto!important;-moz-hyphens: auto!important;-ms-hyphens: auto!important;}"} span {"{text-align: left!important;font-family:MyFont!important;color:rgb(112,112,112)}"}   body{"{background-color: #ffffff; color:#272727;}"} </style></head><body><div>{Result.Content}</div></body></html>";

                MainContent.LoadData(htmlContent, "text/html; charset=UTF-8", null);
                backButton.Click += BackButton_Click;
            }
            StopLoading();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}