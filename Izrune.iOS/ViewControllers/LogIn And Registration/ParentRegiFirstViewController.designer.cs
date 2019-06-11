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
	[Register ("ParentRegiFirstViewController")]
	partial class ParentRegiFirstViewController
	{
		[Outlet]
		UIKit.UILabel cityLbl { get; set; }

		[Outlet]
		UIKit.UIView cityView { get; set; }

		[Outlet]
		UIKit.UIView dateTransparentView { get; set; }

		[Outlet]
		UIKit.UITextField daylLbl { get; set; }

		[Outlet]
		UIKit.UITextField firstNameTextfield { get; set; }

		[Outlet]
		UIKit.UITextField lastNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField monthLbl { get; set; }

		[Outlet]
		UIKit.UIStackView textFieldsStackView { get; set; }

		[Outlet]
		UIKit.UITextField transparentDateTextfield { get; set; }

		[Outlet]
		UIKit.UITextField villageTextField { get; set; }

		[Outlet]
		UIKit.UITextField yearLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (dateTransparentView != null) {
				dateTransparentView.Dispose ();
				dateTransparentView = null;
			}

			if (daylLbl != null) {
				daylLbl.Dispose ();
				daylLbl = null;
			}

			if (monthLbl != null) {
				monthLbl.Dispose ();
				monthLbl = null;
			}

			if (textFieldsStackView != null) {
				textFieldsStackView.Dispose ();
				textFieldsStackView = null;
			}

			if (transparentDateTextfield != null) {
				transparentDateTextfield.Dispose ();
				transparentDateTextfield = null;
			}

			if (yearLbl != null) {
				yearLbl.Dispose ();
				yearLbl = null;
			}

			if (cityView != null) {
				cityView.Dispose ();
				cityView = null;
			}

			if (cityLbl != null) {
				cityLbl.Dispose ();
				cityLbl = null;
			}

			if (firstNameTextfield != null) {
				firstNameTextfield.Dispose ();
				firstNameTextfield = null;
			}

			if (lastNameTextField != null) {
				lastNameTextField.Dispose ();
				lastNameTextField = null;
			}

			if (villageTextField != null) {
				villageTextField.Dispose ();
				villageTextField = null;
			}
		}
	}
}
