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
	[Register ("MenuViewController")]
	partial class MenuViewController
	{
		[Outlet]
		UIKit.UICollectionView menuCollectionView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint menuCollectionViewTopSpace { get; set; }

		[Outlet]
		UIKit.UILabel profileNumberLbl { get; set; }

		[Outlet]
		UIKit.UIStackView profileNumberStackView { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (menuCollectionView != null) {
				menuCollectionView.Dispose ();
				menuCollectionView = null;
			}

			if (menuCollectionViewTopSpace != null) {
				menuCollectionViewTopSpace.Dispose ();
				menuCollectionViewTopSpace = null;
			}

			if (profileNumberStackView != null) {
				profileNumberStackView.Dispose ();
				profileNumberStackView = null;
			}

			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (profileNumberLbl != null) {
				profileNumberLbl.Dispose ();
				profileNumberLbl = null;
			}
		}
	}
}
