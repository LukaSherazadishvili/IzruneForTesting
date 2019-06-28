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
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Fragments
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

    class GetMoreInfoFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutGetMoreInfo;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

        [MapControl(Resource.Id.getMoreInfoWebView)]
        WebView MWebView;

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Startloading();


            var Result = await MpdcContainer.Instance.Get<INewsService>().GetMoreInfoAsync();

            MWebView.Settings.JavaScriptEnabled = true;
            MWebView.SetWebViewClient(new MyWebViewClient());
            MWebView.LoadData(Result, "text/html; charset=UTF-8", null);

            StopLoading();
        }
    }
}