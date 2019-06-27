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
	[Register ("StudentRegistrationViewController")]
	partial class StudentRegistrationViewController
	{
		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UIView footerView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint headerHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIView headerView { get; set; }

		[Outlet]
		UIKit.UIScrollView mainScrollView { get; set; }

		[Outlet]
		UIKit.UIButton nextBtn { get; set; }

		[Outlet]
		UIKit.UIView scrollContent { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint subViewsContentHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIView viewForPeager { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (nextBtn != null) {
				nextBtn.Dispose ();
				nextBtn = null;
			}

			if (footerView != null) {
				footerView.Dispose ();
				footerView = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (viewForPeager != null) {
				viewForPeager.Dispose ();
				viewForPeager = null;
			}

			if (headerView != null) {
				headerView.Dispose ();
				headerView = null;
			}

			if (headerHeightConstraint != null) {
				headerHeightConstraint.Dispose ();
				headerHeightConstraint = null;
			}

			if (scrollContent != null) {
				scrollContent.Dispose ();
				scrollContent = null;
			}

			if (mainScrollView != null) {
				mainScrollView.Dispose ();
				mainScrollView = null;
			}

			if (subViewsContentHeightConstraint != null) {
				subViewsContentHeightConstraint.Dispose ();
				subViewsContentHeightConstraint = null;
			}
		}
	}
}
