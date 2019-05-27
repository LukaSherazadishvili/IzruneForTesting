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
		
		void ReleaseDesignerOutlets ()
		{
			if (checkImageView != null) {
				checkImageView.Dispose ();
				checkImageView = null;
			}

			if (answerNumberLbl != null) {
				answerNumberLbl.Dispose ();
				answerNumberLbl = null;
			}

			if (answerNumberView != null) {
				answerNumberView.Dispose ();
				answerNumberView = null;
			}
		}
	}
}
