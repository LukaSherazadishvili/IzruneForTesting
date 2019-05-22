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
		UIKit.UIView exTestContentView { get; set; }

		[Outlet]
		UIKit.UIView exTestMainView { get; set; }

		[Outlet]
		UIKit.UIView summQuizTransparentView { get; set; }

		[Outlet]
		UIKit.UIView summTestContentView { get; set; }

		[Outlet]
		UIKit.UIView summTestMainView { get; set; }

		[Outlet]
		UIKit.UILabel test1TimerLbl { get; set; }

		[Outlet]
		UIKit.UILabel test2TimerLbl { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }

		[Outlet]
		UIKit.UIView userNameTransparentView { get; set; }

		[Outlet]
		UIKit.UIView viewForDropDown { get; set; }

		[Outlet]
		UIKit.UIView viewForExShadow { get; set; }

		[Outlet]
		UIKit.UIView viewForSummerShadow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (exQuizTransparentView != null) {
				exQuizTransparentView.Dispose ();
				exQuizTransparentView = null;
			}

			if (summQuizTransparentView != null) {
				summQuizTransparentView.Dispose ();
				summQuizTransparentView = null;
			}

			if (test1TimerLbl != null) {
				test1TimerLbl.Dispose ();
				test1TimerLbl = null;
			}

			if (test2TimerLbl != null) {
				test2TimerLbl.Dispose ();
				test2TimerLbl = null;
			}

			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (userNameTransparentView != null) {
				userNameTransparentView.Dispose ();
				userNameTransparentView = null;
			}

			if (viewForDropDown != null) {
				viewForDropDown.Dispose ();
				viewForDropDown = null;
			}

			if (summTestMainView != null) {
				summTestMainView.Dispose ();
				summTestMainView = null;
			}

			if (summTestContentView != null) {
				summTestContentView.Dispose ();
				summTestContentView = null;
			}

			if (exTestMainView != null) {
				exTestMainView.Dispose ();
				exTestMainView = null;
			}

			if (exTestContentView != null) {
				exTestContentView.Dispose ();
				exTestContentView = null;
			}

			if (viewForSummerShadow != null) {
				viewForSummerShadow.Dispose ();
				viewForSummerShadow = null;
			}

			if (viewForExShadow != null) {
				viewForExShadow.Dispose ();
				viewForExShadow = null;
			}
		}
	}
}
