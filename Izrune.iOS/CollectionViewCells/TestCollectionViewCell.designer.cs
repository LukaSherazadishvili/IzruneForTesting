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
	[Register ("TestCollectionViewCell")]
	partial class TestCollectionViewCell
	{
		[Outlet]
		UIKit.UIView ansver1View { get; set; }

		[Outlet]
		UIKit.UIView answer1BgView { get; set; }

		[Outlet]
		UIKit.UILabel answer1Lbl { get; set; }

		[Outlet]
		UIKit.UIView answer2BgView { get; set; }

		[Outlet]
		UIKit.UILabel answer2Lbl { get; set; }

		[Outlet]
		UIKit.UIView answer2View { get; set; }

		[Outlet]
		UIKit.UIView answer3View { get; set; }

		[Outlet]
		UIKit.UILabel answer4Lbl { get; set; }

		[Outlet]
		UIKit.UIView answer4View { get; set; }

		[Outlet]
		UIKit.UILabel asnwer3Lbl { get; set; }

		[Outlet]
		UIKit.UICollectionView questionImagesCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel questionLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (questionLbl != null) {
				questionLbl.Dispose ();
				questionLbl = null;
			}

			if (questionImagesCollectionView != null) {
				questionImagesCollectionView.Dispose ();
				questionImagesCollectionView = null;
			}

			if (ansver1View != null) {
				ansver1View.Dispose ();
				ansver1View = null;
			}

			if (answer2View != null) {
				answer2View.Dispose ();
				answer2View = null;
			}

			if (answer3View != null) {
				answer3View.Dispose ();
				answer3View = null;
			}

			if (answer4View != null) {
				answer4View.Dispose ();
				answer4View = null;
			}

			if (answer1Lbl != null) {
				answer1Lbl.Dispose ();
				answer1Lbl = null;
			}

			if (answer2Lbl != null) {
				answer2Lbl.Dispose ();
				answer2Lbl = null;
			}

			if (asnwer3Lbl != null) {
				asnwer3Lbl.Dispose ();
				asnwer3Lbl = null;
			}

			if (answer4Lbl != null) {
				answer4Lbl.Dispose ();
				answer4Lbl = null;
			}

			if (answer1BgView != null) {
				answer1BgView.Dispose ();
				answer1BgView = null;
			}

			if (answer2BgView != null) {
				answer2BgView.Dispose ();
				answer2BgView = null;
			}
		}
	}
}
