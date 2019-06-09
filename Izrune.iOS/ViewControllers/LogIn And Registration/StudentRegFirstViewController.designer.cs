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
		UIKit.UITextField monthTextField { get; set; }

		[Outlet]
		UIKit.UITextField transparentTextField { get; set; }

		[Outlet]
		UIKit.UITextField yearTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (transparentTextField != null) {
				transparentTextField.Dispose ();
				transparentTextField = null;
			}

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

			if (yearTextField != null) {
				yearTextField.Dispose ();
				yearTextField = null;
			}
		}
	}
}
