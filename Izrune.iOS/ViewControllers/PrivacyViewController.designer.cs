// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Izrune.iOS
{
	[Register ("PrivacyViewController")]
	partial class PrivacyViewController
	{
		[Outlet]
		UIKit.UIImageView closeImageView { get; set; }

		[Outlet]
		UIKit.UIView mainBgView { get; set; }

		[Outlet]
		UIKit.UIWebView privacyWebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainBgView != null) {
				mainBgView.Dispose ();
				mainBgView = null;
			}

			if (privacyWebView != null) {
				privacyWebView.Dispose ();
				privacyWebView = null;
			}

			if (closeImageView != null) {
				closeImageView.Dispose ();
				closeImageView = null;
			}
		}
	}
}
