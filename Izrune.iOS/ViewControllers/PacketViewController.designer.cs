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
	[Register ("PacketViewController")]
	partial class PacketViewController
	{
		[Outlet]
		UIKit.UILabel headerTitleLbl { get; set; }

		[Outlet]
		UIKit.UIButton nextBtn { get; set; }

		[Outlet]
		UIKit.UIButton prevBtn { get; set; }

		[Outlet]
		UIKit.UIView viewForIndividual { get; set; }

		[Outlet]
		UIKit.UIView viewForPeager { get; set; }

		[Outlet]
		UIKit.UIView viewForPromoCode { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewForPeager != null) {
				viewForPeager.Dispose ();
				viewForPeager = null;
			}

			if (headerTitleLbl != null) {
				headerTitleLbl.Dispose ();
				headerTitleLbl = null;
			}

			if (viewForIndividual != null) {
				viewForIndividual.Dispose ();
				viewForIndividual = null;
			}

			if (viewForPromoCode != null) {
				viewForPromoCode.Dispose ();
				viewForPromoCode = null;
			}

			if (nextBtn != null) {
				nextBtn.Dispose ();
				nextBtn = null;
			}

			if (prevBtn != null) {
				prevBtn.Dispose ();
				prevBtn = null;
			}
		}
	}
}
