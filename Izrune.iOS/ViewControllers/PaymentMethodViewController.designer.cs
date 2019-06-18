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
	[Register ("PaymentMethodViewController")]
	partial class PaymentMethodViewController
	{
		[Outlet]
		UIKit.UIButton payViaBankBtn { get; set; }

		[Outlet]
		UIKit.UIButton payViaCardBtn { get; set; }

		[Outlet]
		UIKit.UIButton payViaPayBoxBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (payViaCardBtn != null) {
				payViaCardBtn.Dispose ();
				payViaCardBtn = null;
			}

			if (payViaBankBtn != null) {
				payViaBankBtn.Dispose ();
				payViaBankBtn = null;
			}

			if (payViaPayBoxBtn != null) {
				payViaPayBoxBtn.Dispose ();
				payViaPayBoxBtn = null;
			}
		}
	}
}
