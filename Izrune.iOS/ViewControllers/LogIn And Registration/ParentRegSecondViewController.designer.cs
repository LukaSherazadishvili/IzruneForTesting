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
	[Register ("ParentRegSecondViewController")]
	partial class ParentRegSecondViewController
	{
		[Outlet]
		UIKit.UITextField emailTextField { get; set; }

		[Outlet]
		UIKit.UITextField passwordTextField { get; set; }

		[Outlet]
		UIKit.UITextField phoneTextField { get; set; }

		[Outlet]
		UIKit.UITextField repeatPasswordTextField { get; set; }

		[Outlet]
		UIKit.UIStackView textFieldsStackView { get; set; }

		[Outlet]
		UIKit.UITextField userNameTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (textFieldsStackView != null) {
				textFieldsStackView.Dispose ();
				textFieldsStackView = null;
			}

			if (phoneTextField != null) {
				phoneTextField.Dispose ();
				phoneTextField = null;
			}

			if (emailTextField != null) {
				emailTextField.Dispose ();
				emailTextField = null;
			}

			if (userNameTextField != null) {
				userNameTextField.Dispose ();
				userNameTextField = null;
			}

			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}

			if (repeatPasswordTextField != null) {
				repeatPasswordTextField.Dispose ();
				repeatPasswordTextField = null;
			}
		}
	}
}
