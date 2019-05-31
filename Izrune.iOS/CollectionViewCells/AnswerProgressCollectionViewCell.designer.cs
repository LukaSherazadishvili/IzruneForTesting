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
	[Register ("AnswerProgressCollectionViewCell")]
	partial class AnswerProgressCollectionViewCell
	{
		[Outlet]
		UIKit.UILabel answerNumberLbl { get; set; }

		[Outlet]
		UIKit.UIView answerNumberView { get; set; }

		[Outlet]
		UIKit.UIImageView checkImageView { get; set; }

		[Outlet]
		UIKit.UIView leftView { get; set; }

		[Outlet]
		UIKit.UIView rightView { get; set; }

		[Outlet]
		UIKit.UIView undefinedView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (answerNumberLbl != null) {
				answerNumberLbl.Dispose ();
				answerNumberLbl = null;
			}

			if (answerNumberView != null) {
				answerNumberView.Dispose ();
				answerNumberView = null;
			}

			if (checkImageView != null) {
				checkImageView.Dispose ();
				checkImageView = null;
			}

			if (undefinedView != null) {
				undefinedView.Dispose ();
				undefinedView = null;
			}

			if (leftView != null) {
				leftView.Dispose ();
				leftView = null;
			}

			if (rightView != null) {
				rightView.Dispose ();
				rightView = null;
			}
		}
	}
}
