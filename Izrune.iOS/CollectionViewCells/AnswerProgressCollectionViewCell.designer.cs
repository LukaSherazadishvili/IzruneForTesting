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

            if (leftView != null) {
                leftView.Dispose ();
                leftView = null;
            }

            if (rightView != null) {
                rightView.Dispose ();
                rightView = null;
            }

            if (undefinedView != null) {
                undefinedView.Dispose ();
                undefinedView = null;
            }
        }
    }
}