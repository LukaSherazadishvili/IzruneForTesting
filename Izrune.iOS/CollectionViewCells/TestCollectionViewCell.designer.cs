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
		UIKit.UICollectionView answerCollectionView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint answerCollectionViewHeight { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint answersColletionViewHeight { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint contentHeight { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint imagesCollectionViewHeight { get; set; }

		[Outlet]
		UIKit.UICollectionView questionImagesCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel questionLbl { get; set; }

		[Outlet]
		UIKit.UIView scrollViewContentHeight { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (answerCollectionView != null) {
				answerCollectionView.Dispose ();
				answerCollectionView = null;
			}

			if (answerCollectionViewHeight != null) {
				answerCollectionViewHeight.Dispose ();
				answerCollectionViewHeight = null;
			}

			if (contentHeight != null) {
				contentHeight.Dispose ();
				contentHeight = null;
			}

			if (questionImagesCollectionView != null) {
				questionImagesCollectionView.Dispose ();
				questionImagesCollectionView = null;
			}

			if (questionLbl != null) {
				questionLbl.Dispose ();
				questionLbl = null;
			}

			if (imagesCollectionViewHeight != null) {
				imagesCollectionViewHeight.Dispose ();
				imagesCollectionViewHeight = null;
			}

			if (answersColletionViewHeight != null) {
				answersColletionViewHeight.Dispose ();
				answersColletionViewHeight = null;
			}

			if (scrollViewContentHeight != null) {
				scrollViewContentHeight.Dispose ();
				scrollViewContentHeight = null;
			}
		}
	}
}
