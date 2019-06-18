// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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

            if (imageScrollView != null) {
                imageScrollView.Dispose ();
                imageScrollView = null;
            }

            if (imageViewBottom != null) {
                imageViewBottom.Dispose ();
                imageViewBottom = null;
            }

            if (imageViewLeading != null) {
                imageViewLeading.Dispose ();
                imageViewLeading = null;
            }

            if (imageViewTop != null) {
                imageViewTop.Dispose ();
                imageViewTop = null;
            }

            if (imageViewTrailing != null) {
                imageViewTrailing.Dispose ();
                imageViewTrailing = null;
            }

            if (mainBgView != null) {
                mainBgView.Dispose ();
                mainBgView = null;
            }

            if (questionImageView != null) {
                questionImageView.Dispose ();
                questionImageView = null;
            }
        }
    }
}