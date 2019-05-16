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
	[Register ("SuccesViewController")]
	partial class SuccesViewController
	{
		[Outlet]
		UIKit.UIView checkView { get; set; }

		[Outlet]
		UIKit.UIButton loginBtn { get; set; }

		[Outlet]
		UIKit.UILabel titleLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleLbl != null) {
				titleLbl.Dispose ();
				titleLbl = null;
			}

			if (checkView != null) {
				checkView.Dispose ();
				checkView = null;
			}

			if (loginBtn != null) {
				loginBtn.Dispose ();
				loginBtn = null;
			}
		}
	}
}
