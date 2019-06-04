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
	[Register ("TestResultsViewController")]
	partial class TestResultsViewController
	{
		[Outlet]
		UIKit.UIView monthDropdownView { get; set; }

		[Outlet]
		UIKit.UILabel monthLbl { get; set; }

		[Outlet]
		UIKit.UICollectionView resultCollectionView { get; set; }

		[Outlet]
		UIKit.UIView yearDropdownView { get; set; }

		[Outlet]
		UIKit.UILabel yearLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (resultCollectionView != null) {
				resultCollectionView.Dispose ();
				resultCollectionView = null;
			}

			if (yearDropdownView != null) {
				yearDropdownView.Dispose ();
				yearDropdownView = null;
			}

			if (yearLbl != null) {
				yearLbl.Dispose ();
				yearLbl = null;
			}

			if (monthDropdownView != null) {
				monthDropdownView.Dispose ();
				monthDropdownView = null;
			}

			if (monthLbl != null) {
				monthLbl.Dispose ();
				monthLbl = null;
			}
		}
	}
}
