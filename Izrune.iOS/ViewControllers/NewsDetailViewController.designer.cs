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
	[Register ("NewsDetailViewController")]
	partial class NewsDetailViewController
	{
		[Outlet]
		UIKit.NSLayoutConstraint contentViewHeight { get; set; }

		[Outlet]
		UIKit.UILabel newsDateLbl { get; set; }

		[Outlet]
		UIKit.UIImageView newsImageView { get; set; }

		[Outlet]
		UIKit.UILabel newstitleLbl { get; set; }

		[Outlet]
		UIKit.UIWebView newsWebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (newsImageView != null) {
				newsImageView.Dispose ();
				newsImageView = null;
			}

			if (newstitleLbl != null) {
				newstitleLbl.Dispose ();
				newstitleLbl = null;
			}

			if (newsDateLbl != null) {
				newsDateLbl.Dispose ();
				newsDateLbl = null;
			}

			if (newsWebView != null) {
				newsWebView.Dispose ();
				newsWebView = null;
			}

			if (contentViewHeight != null) {
				contentViewHeight.Dispose ();
				contentViewHeight = null;
			}
		}
	}
}
