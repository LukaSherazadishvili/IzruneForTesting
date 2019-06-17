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
    [Register ("TestCollectionViewCell")]
    partial class TestCollectionViewCell
    {
        [Outlet]
        UIKit.UICollectionView answerCollectionView { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint answerCollectionViewHeight { get; set; }


        [Outlet]
        UIKit.UIView bottomLine { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint imagesCollectionViewHeight { get; set; }


        [Outlet]
        UIKit.UIView mainView { get; set; }


        [Outlet]
        UIKit.UICollectionView questionImagesCollectionView { get; set; }


        [Outlet]
        UIKit.UILabel questionLbl { get; set; }


        [Outlet]
        UIKit.UIView topLine { get; set; }

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

            if (bottomLine != null) {
                bottomLine.Dispose ();
                bottomLine = null;
            }

            if (imagesCollectionViewHeight != null) {
                imagesCollectionViewHeight.Dispose ();
                imagesCollectionViewHeight = null;
            }

            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }

            if (questionImagesCollectionView != null) {
                questionImagesCollectionView.Dispose ();
                questionImagesCollectionView = null;
            }

            if (questionLbl != null) {
                questionLbl.Dispose ();
                questionLbl = null;
            }

            if (topLine != null) {
                topLine.Dispose ();
                topLine = null;
            }
        }
    }
}