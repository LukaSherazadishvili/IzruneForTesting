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
	[Register ("PasswordRecoveryViewController")]
	partial class PasswordRecoveryViewController
	{
		[Outlet]
		UIKit.UIImageView backImageView { get; set; }

		[Outlet]
		UIKit.UIView backView { get; set; }

		[Outlet]
		UIKit.UILabel errorLbl { get; set; }

		[Outlet]
		UIKit.UITextField phoneTextField { get; set; }

		[Outlet]
		UIKit.UIButton sendBtn { get; set; }

		[Outlet]
		UIKit.UIView sendView { get; set; }

		[Outlet]
		UIKit.UILabel titleLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (phoneTextField != null) {
				phoneTextField.Dispose ();
				phoneTextField = null;
			}

			if (sendView != null) {
				sendView.Dispose ();
				sendView = null;
			}

			if (backView != null) {
				backView.Dispose ();
				backView = null;
			}

			if (backImageView != null) {
				backImageView.Dispose ();
				backImageView = null;
			}

			if (titleLbl != null) {
				titleLbl.Dispose ();
				titleLbl = null;
			}

			if (errorLbl != null) {
				errorLbl.Dispose ();
				errorLbl = null;
			}

			if (sendBtn != null) {
				sendBtn.Dispose ();
				sendBtn = null;
			}
		}
	}
}
