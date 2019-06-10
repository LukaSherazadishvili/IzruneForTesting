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
		UIKit.UIScrollView imageScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint imageViewBottom { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint imageViewLeading { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint imageViewTop { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint imageViewTrailing { get; set; }

		[Outlet]
		UIKit.UIView mainBgView { get; set; }

		[Outlet]
		UIKit.UIImageView questionImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (closeBtn != null) {
				closeBtn.Dispose ();
				closeBtn = null;
			}

			if (mainBgView != null) {
				mainBgView.Dispose ();
				mainBgView = null;
			}

			if (questionImageView != null) {
				questionImageView.Dispose ();
				questionImageView = null;
			}

			if (imageScrollView != null) {
				imageScrollView.Dispose ();
				imageScrollView = null;
			}

			if (imageViewBottom != null) {
				imageViewBottom.Dispose ();
				imageViewBottom = null;
			}

			if (imageViewTrailing != null) {
				imageViewTrailing.Dispose ();
				imageViewTrailing = null;
			}

			if (imageViewLeading != null) {
				imageViewLeading.Dispose ();
				imageViewLeading = null;
			}

			if (imageViewTop != null) {
				imageViewTop.Dispose ();
				imageViewTop = null;
			}
		}
	}
}
