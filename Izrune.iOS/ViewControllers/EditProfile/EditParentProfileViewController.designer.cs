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
	[Register ("EditParentProfileViewController")]
	partial class EditParentProfileViewController
	{
		[Outlet]
		UIKit.UILabel cityLbl { get; set; }

		[Outlet]
		UIKit.UIView cityView { get; set; }

		[Outlet]
		UIKit.UITextField dateTransparentTf { get; set; }

		[Outlet]
		UIKit.UITextField dayTf { get; set; }

		[Outlet]
		UIKit.UITextField emailTf { get; set; }

		[Outlet]
		UIKit.UITextField monthTf { get; set; }

		[Outlet]
		UIKit.UILabel parentLastNameLbl { get; set; }

		[Outlet]
		UIKit.UILabel parentNameLbl { get; set; }

		[Outlet]
		UIKit.UITextField phoneTf { get; set; }

		[Outlet]
		UIKit.UITextField villageTf { get; set; }

		[Outlet]
		UIKit.UITextField yearTf { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (parentNameLbl != null) {
				parentNameLbl.Dispose ();
				parentNameLbl = null;
			}

			if (parentLastNameLbl != null) {
				parentLastNameLbl.Dispose ();
				parentLastNameLbl = null;
			}

			if (cityView != null) {
				cityView.Dispose ();
				cityView = null;
			}

			if (cityLbl != null) {
				cityLbl.Dispose ();
				cityLbl = null;
			}

			if (villageTf != null) {
				villageTf.Dispose ();
				villageTf = null;
			}

			if (phoneTf != null) {
				phoneTf.Dispose ();
				phoneTf = null;
			}

			if (emailTf != null) {
				emailTf.Dispose ();
				emailTf = null;
			}

			if (dateTransparentTf != null) {
				dateTransparentTf.Dispose ();
				dateTransparentTf = null;
			}

			if (dayTf != null) {
				dayTf.Dispose ();
				dayTf = null;
			}

			if (monthTf != null) {
				monthTf.Dispose ();
				monthTf = null;
			}

			if (yearTf != null) {
				yearTf.Dispose ();
				yearTf = null;
			}
		}
	}
}
