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
	[Register ("QuestionImageViewController")]
	partial class QuestionImageViewController
	{
		[Outlet]
		UIKit.UIButton closeBtn { get; set; }

		[Outlet]
		UIKit.UIView mainBgView { get; set; }

		[Outlet]
		UIKit.UIImageView questionImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainBgView != null) {
				mainBgView.Dispose ();
				mainBgView = null;
			}

			if (questionImageView != null) {
				questionImageView.Dispose ();
				questionImageView = null;
			}

			if (closeBtn != null) {
				closeBtn.Dispose ();
				closeBtn = null;
			}
		}
	}
}
