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
	[Register ("SmsVerificationViewController")]
	partial class SmsVerificationViewController
	{
		[Outlet]
		UIKit.UIButton confirmBtn { get; set; }

		[Outlet]
		UIKit.UIButton getCodeBtn { get; set; }

		[Outlet]
		UIKit.UIView smsShadowView { get; set; }

		[Outlet]
		UIKit.UILabel smsTextLbl { get; set; }

		[Outlet]
		UIKit.UITextField smsTf { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (smsTextLbl != null) {
				smsTextLbl.Dispose ();
				smsTextLbl = null;
			}

			if (smsTf != null) {
				smsTf.Dispose ();
				smsTf = null;
			}

			if (confirmBtn != null) {
				confirmBtn.Dispose ();
				confirmBtn = null;
			}

			if (getCodeBtn != null) {
				getCodeBtn.Dispose ();
				getCodeBtn = null;
			}

			if (smsShadowView != null) {
				smsShadowView.Dispose ();
				smsShadowView = null;
			}
		}
	}
}
