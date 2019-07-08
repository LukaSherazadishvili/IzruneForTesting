// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Izrune.iOS.CollectionViewCells
{
	[Register ("PaymentHistoryCell")]
	partial class PaymentHistoryCell
	{
		[Outlet]
		UIKit.UILabel dateLbl { get; set; }

		[Outlet]
		UIKit.UILabel priceLbl { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (dateLbl != null) {
				dateLbl.Dispose ();
				dateLbl = null;
			}

			if (priceLbl != null) {
				priceLbl.Dispose ();
				priceLbl = null;
			}
		}
	}
}
