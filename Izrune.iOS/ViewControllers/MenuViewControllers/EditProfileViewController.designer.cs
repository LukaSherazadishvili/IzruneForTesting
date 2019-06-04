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
		UIKit.UIStackView editStackView { get; set; }

		[Outlet]
		UIKit.UIView firstShadowView { get; set; }

		[Outlet]
		UIKit.UIView secondShadowView { get; set; }

		[Outlet]
		UIKit.UIView thirdShadovView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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
		}
	}
}
