// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;
using MpdcViewExtentions;
using System.Globalization;
using MPDC.iOS.Utils;

namespace Izrune.iOS
{
	public partial class NewsDetailViewController : UIViewController, IUIWebViewDelegate
	{
		public NewsDetailViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("NewsDetailStoryboardId");

        public INews News;

        CultureInfo cultureInfo = new CultureInfo("ka-GE");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            newsWebView.Delegate = this;
            newsWebView.ScrollView.ScrollEnabled = false;

            InitUI();

        }

        private void InitUI()
        {
            newsImageView.Layer.CornerRadius = 10;
            newsImageView.InitImageFromWeb(News?.ImageUrl, false, false);

            newstitleLbl.Text = News?.Title;
            newsDateLbl.Text = News?.date.ToString("dd MMMM yyyy", cultureInfo);

            newsWebView.LoadHtmlString(News?.Content, NSUrl.FromString("https://www.google.com/"));
        }

        [Export("webViewDidFinishLoad:")]
        public void LoadingFinished(UIWebView webView)
        {
            var height = News?.Title.GetStringHeight((float)newsImageView.Frame.Width, 0, 12);

            var webviewHeight = webView.ScrollView.ContentSize.Height;

            var headerHeight = 225 + 25 + height + webviewHeight;

            contentViewHeight.Constant = (System.nfloat)headerHeight;
        }
    }
}
