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
	[Register ("PayViaBankViewController")]
	partial class PayViaBankViewController
	{
		[Outlet]
		UIKit.UILabel billLbl { get; set; }

		[Outlet]
		UIKit.UILabel profileNumberLbl { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (profileNumberLbl != null) {
				profileNumberLbl.Dispose ();
				profileNumberLbl = null;
			}

			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (billLbl != null) {
				billLbl.Dispose ();
				billLbl = null;
			}
		}
	}
}
