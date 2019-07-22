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
	[Register ("BadgeCell")]
	partial class BadgeCell
	{
		[Outlet]
		UIKit.UIImageView badgeImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (badgeImageView != null) {
				badgeImageView.Dispose ();
				badgeImageView = null;
			}
		}
	}
}
