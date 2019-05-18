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
	[Register ("QuestionImageCollectionViewCell")]
	partial class QuestionImageCollectionViewCell
	{
		[Outlet]
		UIKit.UIView questiomImageViewHolder { get; set; }

		[Outlet]
		UIKit.UIImageView questionImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (questionImageView != null) {
				questionImageView.Dispose ();
				questionImageView = null;
			}

			if (questiomImageViewHolder != null) {
				questiomImageViewHolder.Dispose ();
				questiomImageViewHolder = null;
			}
		}
	}
}
