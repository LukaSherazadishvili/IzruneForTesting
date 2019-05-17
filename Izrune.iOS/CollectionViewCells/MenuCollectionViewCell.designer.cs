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
	[Register ("MenuCollectionViewCell")]
	partial class MenuCollectionViewCell
	{
		[Outlet]
		UIKit.UIImageView menuImage { get; set; }

		[Outlet]
		UIKit.UILabel titleLbl { get; set; }

		[Outlet]
		UIKit.UIView transparentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (menuImage != null) {
				menuImage.Dispose ();
				menuImage = null;
			}

			if (titleLbl != null) {
				titleLbl.Dispose ();
				titleLbl = null;
			}

			if (transparentView != null) {
				transparentView.Dispose ();
				transparentView = null;
			}
		}
	}
}
