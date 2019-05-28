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
		UIKit.UICollectionView answerProgressCollectionView { get; set; }

		[Outlet]
		UIKit.UICollectionView questionCollectionView { get; set; }

		[Outlet]
		UIKit.UIButton skipQuestionBtn { get; set; }

		[Outlet]
		UIKit.UILabel timeLbl { get; set; }

		[Outlet]
		UIKit.UILabel userNameLbl { get; set; }

		[Outlet]
		UIKit.UIView viewForCircular { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (answerProgressCollectionView != null) {
				answerProgressCollectionView.Dispose ();
				answerProgressCollectionView = null;
			}

			if (questionCollectionView != null) {
				questionCollectionView.Dispose ();
				questionCollectionView = null;
			}

			if (skipQuestionBtn != null) {
				skipQuestionBtn.Dispose ();
				skipQuestionBtn = null;
			}

			if (timeLbl != null) {
				timeLbl.Dispose ();
				timeLbl = null;
			}

			if (userNameLbl != null) {
				userNameLbl.Dispose ();
				userNameLbl = null;
			}

			if (viewForCircular != null) {
				viewForCircular.Dispose ();
				viewForCircular = null;
			}
		}
	}
}
