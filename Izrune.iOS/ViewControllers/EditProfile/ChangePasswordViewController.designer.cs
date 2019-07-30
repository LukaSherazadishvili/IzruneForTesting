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
	[Register ("ChangePasswordViewController")]
	partial class ChangePasswordViewController
	{
		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UITextField oldPassTf { get; set; }

		[Outlet]
		UIKit.UIView oldPassView { get; set; }

		[Outlet]
		UIKit.UIView passNewView { get; set; }

		[Outlet]
		UIKit.UITextField passwordNewTf { get; set; }

		[Outlet]
		UIKit.UIView repeatNewPassView { get; set; }

		[Outlet]
		UIKit.UITextField repeatNewPasswordTf { get; set; }

		[Outlet]
		UIKit.UIButton saveBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (oldPassTf != null) {
				oldPassTf.Dispose ();
				oldPassTf = null;
			}

			if (passwordNewTf != null) {
				passwordNewTf.Dispose ();
				passwordNewTf = null;
			}

			if (repeatNewPasswordTf != null) {
				repeatNewPasswordTf.Dispose ();
				repeatNewPasswordTf = null;
			}

			if (saveBtn != null) {
				saveBtn.Dispose ();
				saveBtn = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (oldPassView != null) {
				oldPassView.Dispose ();
				oldPassView = null;
			}

			if (passNewView != null) {
				passNewView.Dispose ();
				passNewView = null;
			}

			if (repeatNewPassView != null) {
				repeatNewPassView.Dispose ();
				repeatNewPassView = null;
			}
		}
	}
}
