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
	[Register ("ParentRegistrationViewController")]
	partial class ParentRegistrationViewController
	{
		[Outlet]
		UIKit.NSLayoutConstraint headerHeightConstant { get; set; }

		[Outlet]
		UIKit.UIImageView headerImageView { get; set; }

		[Outlet]
		UIKit.UIStackView headerStackView { get; set; }

		[Outlet]
		UIKit.UILabel headerTitleLbl { get; set; }

		[Outlet]
		UIKit.UIButton nextBtn { get; set; }

		[Outlet]
		UIKit.UIButton prewBtn { get; set; }

		[Outlet]
		UIKit.UIView viewForPager { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (headerImageView != null) {
				headerImageView.Dispose ();
				headerImageView = null;
			}

			if (headerTitleLbl != null) {
				headerTitleLbl.Dispose ();
				headerTitleLbl = null;
			}

			if (nextBtn != null) {
				nextBtn.Dispose ();
				nextBtn = null;
			}

			if (prewBtn != null) {
				prewBtn.Dispose ();
				prewBtn = null;
			}

			if (viewForPager != null) {
				viewForPager.Dispose ();
				viewForPager = null;
			}

			if (headerHeightConstant != null) {
				headerHeightConstant.Dispose ();
				headerHeightConstant = null;
			}

			if (headerStackView != null) {
				headerStackView.Dispose ();
				headerStackView = null;
			}
		}
	}
}
