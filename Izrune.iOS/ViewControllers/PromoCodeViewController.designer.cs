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
	[Register ("PromoCodeViewController")]
	partial class PromoCodeViewController
	{
		[Outlet]
		UIKit.UIButton confirmBtn { get; set; }

		[Outlet]
		UIKit.UILabel monthLbl { get; set; }

		[Outlet]
		UIKit.UITextField monthTf { get; set; }

		[Outlet]
		UIKit.UIView monthView { get; set; }

		[Outlet]
		UIKit.UILabel promoCodeErorLbl { get; set; }

		[Outlet]
		UIKit.UITextField promoCodeTf { get; set; }

		[Outlet]
		UIKit.UIStackView promoStackView { get; set; }

		[Outlet]
		UIKit.UILabel specialPacketLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (confirmBtn != null) {
				confirmBtn.Dispose ();
				confirmBtn = null;
			}

			if (monthLbl != null) {
				monthLbl.Dispose ();
				monthLbl = null;
			}

			if (monthTf != null) {
				monthTf.Dispose ();
				monthTf = null;
			}

			if (monthView != null) {
				monthView.Dispose ();
				monthView = null;
			}

			if (promoCodeErorLbl != null) {
				promoCodeErorLbl.Dispose ();
				promoCodeErorLbl = null;
			}

			if (promoCodeTf != null) {
				promoCodeTf.Dispose ();
				promoCodeTf = null;
			}

			if (promoStackView != null) {
				promoStackView.Dispose ();
				promoStackView = null;
			}

			if (specialPacketLbl != null) {
				specialPacketLbl.Dispose ();
				specialPacketLbl = null;
			}
		}
	}
}
