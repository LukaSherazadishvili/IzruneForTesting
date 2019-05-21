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
	[Register ("StartTestViewController")]
	partial class StartTestViewController
	{
		[Outlet]
		UIKit.UIView exQuizTransparentView { get; set; }

		[Outlet]
		UIKit.UIView summQuizTransparentView { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }

		[Outlet]
		UIKit.UIView userNameTransparentView { get; set; }

		[Outlet]
		UIKit.UIView viewForDropDown { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (summQuizTransparentView != null) {
				summQuizTransparentView.Dispose ();
				summQuizTransparentView = null;
			}

			if (exQuizTransparentView != null) {
				exQuizTransparentView.Dispose ();
				exQuizTransparentView = null;
			}

			if (userNameTransparentView != null) {
				userNameTransparentView.Dispose ();
				userNameTransparentView = null;
			}

			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (viewForDropDown != null) {
				viewForDropDown.Dispose ();
				viewForDropDown = null;
			}
		}
	}
}
