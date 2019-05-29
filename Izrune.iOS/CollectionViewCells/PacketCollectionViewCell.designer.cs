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
	[Register ("PacketCollectionViewCell")]
	partial class PacketCollectionViewCell
	{
		[Outlet]
		UIKit.UIView mainView { get; set; }

		[Outlet]
		UIKit.UILabel monthLbl { get; set; }

		[Outlet]
		UIKit.UILabel oldPriceLbl { get; set; }

		[Outlet]
		UIKit.UILabel priceLbl { get; set; }

		[Outlet]
		UIKit.UIView priceView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (monthLbl != null) {
				monthLbl.Dispose ();
				monthLbl = null;
			}

			if (oldPriceLbl != null) {
				oldPriceLbl.Dispose ();
				oldPriceLbl = null;
			}

			if (priceView != null) {
				priceView.Dispose ();
				priceView = null;
			}

			if (priceLbl != null) {
				priceLbl.Dispose ();
				priceLbl = null;
			}
		}
	}
}
