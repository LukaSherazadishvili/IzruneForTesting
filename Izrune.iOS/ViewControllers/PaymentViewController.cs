// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Izrune.iOS
{
	public partial class PaymentViewController : UIViewController
	{
		public PaymentViewController (IntPtr handle) : base (handle)
		{
		}

        public string PaymentUrl;

        public static readonly NSString StoryboardId = new NSString("PaymentStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();



            if(PaymentUrl != null)
            {
                var nsUrl = NSUrl.FromString(PaymentUrl);
                var request = new NSUrlRequest(nsUrl);
                paymentWebView.LoadRequest(request);
            }

        }
    }
}