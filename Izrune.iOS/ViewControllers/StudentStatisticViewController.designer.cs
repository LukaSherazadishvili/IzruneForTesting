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
	[Register ("StudentStatisticViewController")]
	partial class StudentStatisticViewController
	{
		[Outlet]
		UIKit.UIView diplomeShadow { get; set; }

		[Outlet]
		UIKit.UIView diplomeView { get; set; }

		[Outlet]
		UIKit.UIView exShadow { get; set; }

		[Outlet]
		UIKit.UIView exTestView { get; set; }

		[Outlet]
		UIKit.UIButton paymentHostoryBtn { get; set; }

		[Outlet]
		UIKit.UIView sumShadow { get; set; }

		[Outlet]
		UIKit.UIView sumTestsView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (diplomeView != null) {
				diplomeView.Dispose ();
				diplomeView = null;
			}

			if (sumTestsView != null) {
				sumTestsView.Dispose ();
				sumTestsView = null;
			}

			if (exTestView != null) {
				exTestView.Dispose ();
				exTestView = null;
			}

			if (diplomeShadow != null) {
				diplomeShadow.Dispose ();
				diplomeShadow = null;
			}

			if (sumShadow != null) {
				sumShadow.Dispose ();
				sumShadow = null;
			}

			if (exShadow != null) {
				exShadow.Dispose ();
				exShadow = null;
			}

			if (paymentHostoryBtn != null) {
				paymentHostoryBtn.Dispose ();
				paymentHostoryBtn = null;
			}
		}
	}
}
