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
	[Register ("TestViewController")]
	partial class TestViewController
	{
		[Outlet]
		UIKit.UICollectionView questionCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (questionCollectionView != null) {
				questionCollectionView.Dispose ();
				questionCollectionView = null;
			}
		}
	}
}
