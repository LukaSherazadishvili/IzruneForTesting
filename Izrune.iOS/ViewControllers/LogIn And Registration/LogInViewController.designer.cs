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
	[Register ("LogInViewController")]
	partial class LogInViewController
	{
		[Outlet]
		UIKit.UILabel forgotPasswordLbl { get; set; }

		[Outlet]
		UIKit.UILabel forgotUserNameLbl { get; set; }

		[Outlet]
		UIKit.UIView loginView { get; set; }

		[Outlet]
		UIKit.UITextField passwordTextField { get; set; }

		[Outlet]
		UIKit.UIView registrationView { get; set; }

		[Outlet]
		UIKit.UIImageView showPasswordIcon { get; set; }

		[Outlet]
		UIKit.UITextField userNameTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (forgotPasswordLbl != null) {
				forgotPasswordLbl.Dispose ();
				forgotPasswordLbl = null;
			}

			if (forgotUserNameLbl != null) {
				forgotUserNameLbl.Dispose ();
				forgotUserNameLbl = null;
			}

			if (loginView != null) {
				loginView.Dispose ();
				loginView = null;
			}

			if (registrationView != null) {
				registrationView.Dispose ();
				registrationView = null;
			}

			if (userNameTextField != null) {
				userNameTextField.Dispose ();
				userNameTextField = null;
			}

			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}

			if (showPasswordIcon != null) {
				showPasswordIcon.Dispose ();
				showPasswordIcon = null;
			}
		}
	}
}
