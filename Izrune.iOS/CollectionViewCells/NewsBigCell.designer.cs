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
	[Register ("NewsBigCell")]
	partial class NewsBigCell
	{
		[Outlet]
		UIKit.UILabel dateLbl { get; set; }

		[Outlet]
		UIKit.UIImageView newsImageView { get; set; }

		[Outlet]
		UIKit.UIView newsTransparentView { get; set; }

		[Outlet]
		UIKit.UILabel titleLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (dateLbl != null) {
				dateLbl.Dispose ();
				dateLbl = null;
			}

			if (newsImageView != null) {
				newsImageView.Dispose ();
				newsImageView = null;
			}

			if (titleLbl != null) {
				titleLbl.Dispose ();
				titleLbl = null;
			}

			if (newsTransparentView != null) {
				newsTransparentView.Dispose ();
				newsTransparentView = null;
			}
		}
	}
}
