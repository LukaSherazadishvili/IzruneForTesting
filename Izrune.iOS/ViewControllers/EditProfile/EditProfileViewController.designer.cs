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
	[Register ("EditProfileViewController")]
	partial class EditProfileViewController
	{
		[Outlet]
		UIKit.UIButton addStudentBtn { get; set; }

		[Outlet]
		UIKit.UIStackView editStackView { get; set; }

		[Outlet]
		UIKit.UIView firstShadowView { get; set; }

		[Outlet]
		UIKit.UIView parentShadow { get; set; }

		[Outlet]
		UIKit.UIView parentView { get; set; }

		[Outlet]
		UIKit.UIView passowrdView { get; set; }

		[Outlet]
		UIKit.UIView passwordShadow { get; set; }

		[Outlet]
		UIKit.UIView secondShadowView { get; set; }

		[Outlet]
		UIKit.UIView studentShadow { get; set; }

		[Outlet]
		UIKit.UIView studentView { get; set; }

		[Outlet]
		UIKit.UIView thirdShadovView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (addStudentBtn != null) {
				addStudentBtn.Dispose ();
				addStudentBtn = null;
			}

			if (editStackView != null) {
				editStackView.Dispose ();
				editStackView = null;
			}

			if (firstShadowView != null) {
				firstShadowView.Dispose ();
				firstShadowView = null;
			}

			if (secondShadowView != null) {
				secondShadowView.Dispose ();
				secondShadowView = null;
			}

			if (thirdShadovView != null) {
				thirdShadovView.Dispose ();
				thirdShadovView = null;
			}

			if (parentView != null) {
				parentView.Dispose ();
				parentView = null;
			}

			if (studentView != null) {
				studentView.Dispose ();
				studentView = null;
			}

			if (passowrdView != null) {
				passowrdView.Dispose ();
				passowrdView = null;
			}

			if (parentShadow != null) {
				parentShadow.Dispose ();
				parentShadow = null;
			}

			if (studentShadow != null) {
				studentShadow.Dispose ();
				studentShadow = null;
			}

			if (passwordShadow != null) {
				passwordShadow.Dispose ();
				passwordShadow = null;
			}
		}
	}
}
