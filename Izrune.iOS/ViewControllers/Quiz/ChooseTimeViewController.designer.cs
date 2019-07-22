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
	[Register ("ChooseTimeViewController")]
	partial class ChooseTimeViewController
	{
		[Outlet]
		UIKit.UIImageView closePopUpView { get; set; }

		[Outlet]
		UIKit.UIView derivedTimeShadowView { get; set; }

		[Outlet]
		UIKit.UIView derivedTimeView { get; set; }

		[Outlet]
		UIKit.UILabel headerTitleLbl { get; set; }

		[Outlet]
		UIKit.UIView popUpView { get; set; }

		[Outlet]
		UIKit.UIView shadowView1 { get; set; }

		[Outlet]
		UIKit.UIView shadowView2 { get; set; }

		[Outlet]
		UIKit.UIView totalTimeShadowView { get; set; }

		[Outlet]
		UIKit.UIView totalTimeView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (closePopUpView != null) {
				closePopUpView.Dispose ();
				closePopUpView = null;
			}

			if (derivedTimeShadowView != null) {
				derivedTimeShadowView.Dispose ();
				derivedTimeShadowView = null;
			}

			if (derivedTimeView != null) {
				derivedTimeView.Dispose ();
				derivedTimeView = null;
			}

			if (popUpView != null) {
				popUpView.Dispose ();
				popUpView = null;
			}

			if (shadowView1 != null) {
				shadowView1.Dispose ();
				shadowView1 = null;
			}

			if (shadowView2 != null) {
				shadowView2.Dispose ();
				shadowView2 = null;
			}

			if (totalTimeShadowView != null) {
				totalTimeShadowView.Dispose ();
				totalTimeShadowView = null;
			}

			if (totalTimeView != null) {
				totalTimeView.Dispose ();
				totalTimeView = null;
			}

			if (headerTitleLbl != null) {
				headerTitleLbl.Dispose ();
				headerTitleLbl = null;
			}
		}
	}
}
