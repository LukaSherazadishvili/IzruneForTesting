using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Izrune.Fragments.DialogFrag
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

   public class RullesDialogFragment: DialogFragment
    {

        private string Content { get; set; }

        public RullesDialogFragment(string cont)
        {
            Content = cont;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);


            return inflater.Inflate(Resource.Layout.LayoutRullesDialog, container, false);

            

        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var CloseButton = view.FindViewById<ImageView>(Resource.Id.CloseButton);

            CloseButton.Click += (s, e) =>
            {

                this.Dismiss();

            };

            var RullesWeb = view.FindViewById<WebView>(Resource.Id.RullesWebView);
            RullesWeb.Settings.JavaScriptEnabled = true;

            RullesWeb.SetWebViewClient(new MyWebViewClient()
            {

            });
            RullesWeb.LoadData(Content, "text/html; charset=UTF-8", null);
          


        }

      

    }
}