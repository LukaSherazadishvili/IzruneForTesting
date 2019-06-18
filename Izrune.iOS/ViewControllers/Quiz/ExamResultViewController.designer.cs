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
    [Register ("ExamResultViewController")]
    partial class ExamResultViewController
    {
        [Outlet]
        UIKit.UIImageView diplomeImageView { get; set; }


        [Outlet]
        UIKit.UILabel examTimeLbl { get; set; }


        [Outlet]
        UIKit.UILabel finalResultLbl { get; set; }


        [Outlet]
        UIKit.UIView pointView { get; set; }


        [Outlet]
        UIKit.UIStackView ratingStackView { get; set; }


        [Outlet]
        UIKit.UILabel resultQualityLbl { get; set; }


        [Outlet]
        UIKit.UILabel resultTextLbl { get; set; }


        [Outlet]
        UIKit.UIView timeShadowView { get; set; }


        [Outlet]
        UIKit.UILabel totalPointLbl { get; set; }


        [Outlet]
        UIKit.UILabel userNameLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (diplomeImageView != null) {
                diplomeImageView.Dispose ();
                diplomeImageView = null;
            }

            if (examTimeLbl != null) {
                examTimeLbl.Dispose ();
                examTimeLbl = null;
            }

            if (finalResultLbl != null) {
                finalResultLbl.Dispose ();
                finalResultLbl = null;
            }

            if (pointView != null) {
                pointView.Dispose ();
                pointView = null;
            }

            if (ratingStackView != null) {
                ratingStackView.Dispose ();
                ratingStackView = null;
            }

            if (resultQualityLbl != null) {
                resultQualityLbl.Dispose ();
                resultQualityLbl = null;
            }

            if (resultTextLbl != null) {
                resultTextLbl.Dispose ();
                resultTextLbl = null;
            }

            if (timeShadowView != null) {
                timeShadowView.Dispose ();
                timeShadowView = null;
            }

            if (totalPointLbl != null) {
                totalPointLbl.Dispose ();
                totalPointLbl = null;
            }

            if (userNameLbl != null) {
                userNameLbl.Dispose ();
                userNameLbl = null;
            }
        }
    }
}