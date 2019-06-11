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
	[Register ("StudentRegFirstViewController")]
	partial class StudentRegFirstViewController
	{
		[Outlet]
		UIKit.UIStackView dateStackView { get; set; }

		[Outlet]
		UIKit.UITextField dayTextField { get; set; }

		[Outlet]
		UIKit.UITextField emailTf { get; set; }

		[Outlet]
		UIKit.UITextField firstNameTf { get; set; }

		[Outlet]
		UIKit.UITextField lastNameLTf { get; set; }

		[Outlet]
		UIKit.UITextField monthTextField { get; set; }

		[Outlet]
		UIKit.UITextField phoneTf { get; set; }

		[Outlet]
		UIKit.UITextField privateNumberTf { get; set; }

		[Outlet]
		UIKit.UIStackView textFieldsStackView { get; set; }

		[Outlet]
		UIKit.UITextField transparentTextField { get; set; }

		[Outlet]
		UIKit.UITextField yearTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (dateStackView != null) {
				dateStackView.Dispose ();
				dateStackView = null;
			}

			if (dayTextField != null) {
				dayTextField.Dispose ();
				dayTextField = null;
			}

			if (monthTextField != null) {
				monthTextField.Dispose ();
				monthTextField = null;
			}

			if (transparentTextField != null) {
				transparentTextField.Dispose ();
				transparentTextField = null;
			}

			if (yearTextField != null) {
				yearTextField.Dispose ();
				yearTextField = null;
			}

			if (textFieldsStackView != null) {
				textFieldsStackView.Dispose ();
				textFieldsStackView = null;
			}

			if (firstNameTf != null) {
				firstNameTf.Dispose ();
				firstNameTf = null;
			}

			if (lastNameLTf != null) {
				lastNameLTf.Dispose ();
				lastNameLTf = null;
			}

			if (privateNumberTf != null) {
				privateNumberTf.Dispose ();
				privateNumberTf = null;
			}

			if (phoneTf != null) {
				phoneTf.Dispose ();
				phoneTf = null;
			}

			if (emailTf != null) {
				emailTf.Dispose ();
				emailTf = null;
			}
		}
	}
}
