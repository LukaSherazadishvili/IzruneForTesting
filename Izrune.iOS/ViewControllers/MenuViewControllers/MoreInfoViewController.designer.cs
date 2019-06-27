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
	[Register ("MoreInfoViewController")]
	partial class MoreInfoViewController
	{
		[Outlet]
		UIKit.UIWebView infoWebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (infoWebView != null) {
				infoWebView.Dispose ();
				infoWebView = null;
			}
		}
	}
}
