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
		UIKit.UILabel currentStudentLbl { get; set; }

		[Outlet]
		UIKit.UIView currentStudentView { get; set; }

		[Outlet]
		UIKit.UIView diplomeShadow { get; set; }

		[Outlet]
		UIKit.UIView diplomeView { get; set; }

		[Outlet]
		UIKit.UIView exShadow { get; set; }

		[Outlet]
		UIKit.UIView exTestView { get; set; }

		[Outlet]
		UIKit.UILabel packetDateLbl { get; set; }

		[Outlet]
		UIKit.UIButton paymentHostoryBtn { get; set; }

		[Outlet]
		UIKit.UIView sumShadow { get; set; }

		[Outlet]
		UIKit.UIView sumTestsView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (diplomeShadow != null) {
				diplomeShadow.Dispose ();
				diplomeShadow = null;
			}

			if (diplomeView != null) {
				diplomeView.Dispose ();
				diplomeView = null;
			}

			if (exShadow != null) {
				exShadow.Dispose ();
				exShadow = null;
			}

			if (exTestView != null) {
				exTestView.Dispose ();
				exTestView = null;
			}

			if (paymentHostoryBtn != null) {
				paymentHostoryBtn.Dispose ();
				paymentHostoryBtn = null;
			}

			if (sumShadow != null) {
				sumShadow.Dispose ();
				sumShadow = null;
			}

			if (sumTestsView != null) {
				sumTestsView.Dispose ();
				sumTestsView = null;
			}

			if (currentStudentView != null) {
				currentStudentView.Dispose ();
				currentStudentView = null;
			}

			if (currentStudentLbl != null) {
				currentStudentLbl.Dispose ();
				currentStudentLbl = null;
			}

			if (packetDateLbl != null) {
				packetDateLbl.Dispose ();
				packetDateLbl = null;
			}
		}
	}
}
