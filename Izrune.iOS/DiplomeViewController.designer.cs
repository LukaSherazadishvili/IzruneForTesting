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
	[Register ("DiplomeViewController")]
	partial class DiplomeViewController
	{
		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UICollectionView diplomeCollectionView { get; set; }

		[Outlet]
		UIKit.UIImageView headerImageView { get; set; }

		[Outlet]
		UIKit.UILabel headerLbl { get; set; }

		[Outlet]
		UIKit.UILabel testLbl { get; set; }

		[Outlet]
		UIKit.UIView viewForDropDown { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewForDropDown != null) {
				viewForDropDown.Dispose ();
				viewForDropDown = null;
			}

			if (diplomeCollectionView != null) {
				diplomeCollectionView.Dispose ();
				diplomeCollectionView = null;
			}

			if (testLbl != null) {
				testLbl.Dispose ();
				testLbl = null;
			}

			if (headerLbl != null) {
				headerLbl.Dispose ();
				headerLbl = null;
			}

			if (headerImageView != null) {
				headerImageView.Dispose ();
				headerImageView = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}
		}
	}
}
