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
	[Register ("StudentRegSecondViewController")]
	partial class StudentRegSecondViewController
	{
		[Outlet]
		UIKit.UILabel cityLbl { get; set; }

		[Outlet]
		UIKit.UIView cityView { get; set; }

		[Outlet]
		UIKit.UILabel classLbl { get; set; }

		[Outlet]
		UIKit.UIView classView { get; set; }

		[Outlet]
		UIKit.UILabel schoolLbl { get; set; }

		[Outlet]
		UIKit.UIView selectSchoolView { get; set; }

		[Outlet]
		UIKit.UITextField villageTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (cityLbl != null) {
				cityLbl.Dispose ();
				cityLbl = null;
			}

			if (cityView != null) {
				cityView.Dispose ();
				cityView = null;
			}

			if (classLbl != null) {
				classLbl.Dispose ();
				classLbl = null;
			}

			if (classView != null) {
				classView.Dispose ();
				classView = null;
			}

			if (villageTextField != null) {
				villageTextField.Dispose ();
				villageTextField = null;
			}

			if (selectSchoolView != null) {
				selectSchoolView.Dispose ();
				selectSchoolView = null;
			}

			if (schoolLbl != null) {
				schoolLbl.Dispose ();
				schoolLbl = null;
			}
		}
	}
}
