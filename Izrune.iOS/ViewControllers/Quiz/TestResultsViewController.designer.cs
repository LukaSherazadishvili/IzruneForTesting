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
		UIKit.UICollectionView resultCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (resultCollectionView != null) {
				resultCollectionView.Dispose ();
				resultCollectionView = null;
			}
		}
	}
}
