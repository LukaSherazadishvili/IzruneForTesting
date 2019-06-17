// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
            if (questiomImageViewHolder != null) {
                questiomImageViewHolder.Dispose ();
                questiomImageViewHolder = null;
            }

            if (questionImageView != null) {
                questionImageView.Dispose ();
                questionImageView = null;
            }
        }
    }
}