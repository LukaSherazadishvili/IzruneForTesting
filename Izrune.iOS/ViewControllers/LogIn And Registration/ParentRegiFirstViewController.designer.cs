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
		UIKit.UIView dateTransparentView { get; set; }

		[Outlet]
		UIKit.UITextField daylLbl { get; set; }

		[Outlet]
		UIKit.UITextField monthLbl { get; set; }

		[Outlet]
		UIKit.UIStackView textFieldsStackView { get; set; }

		[Outlet]
		UIKit.UITextField yearLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (textFieldsStackView != null) {
				textFieldsStackView.Dispose ();
				textFieldsStackView = null;
			}

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

			if (yearLbl != null) {
				yearLbl.Dispose ();
				yearLbl = null;
			}
		}
	}
}
