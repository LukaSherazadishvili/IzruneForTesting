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
	[Register ("PromoSchoolViewController")]
	partial class PromoSchoolViewController
	{
		[Outlet]
		UIKit.UIImageView closeImageView { get; set; }

		[Outlet]
		UIKit.UIView mainView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (closeImageView != null) {
				closeImageView.Dispose ();
				closeImageView = null;
			}
		}
	}
}
