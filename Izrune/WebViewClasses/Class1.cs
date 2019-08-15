using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Izrune.WebViewClasses
{
    public class OnlinePayWebView : WebViewClient
    {
        // public IPay clientPayment { get; set; }
        public Action ChangeActyvity { get; set; }
        public string LoadUrl { get; set; }

        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {


            if (request.Url.ToString() == "http://www.izrune.ge/")
            {
                ChangeActyvity?.Invoke();
            }
            else
            {
                view.LoadUrl(LoadUrl);
            }




            return true;
        }

        

        public override void OnPageFinished(WebView view, string url)
        {
           
;           base.OnPageFinished(view, LoadUrl);
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            base.OnPageStarted(view, LoadUrl, favicon);
        }

    }
}