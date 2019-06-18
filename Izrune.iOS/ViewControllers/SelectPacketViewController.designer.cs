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
	[Register ("SelectPacketViewController")]
	partial class SelectPacketViewController
	{
		[Outlet]
		UIKit.NSLayoutConstraint packetCollectionHeightConstraint { get; set; }

		[Outlet]
		UIKit.UICollectionView packetCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (packetCollectionView != null) {
				packetCollectionView.Dispose ();
				packetCollectionView = null;
			}

			if (packetCollectionHeightConstraint != null) {
				packetCollectionHeightConstraint.Dispose ();
				packetCollectionHeightConstraint = null;
			}
		}
	}
}
