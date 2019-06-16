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
    [Register ("ParentRegistrationViewController")]
    partial class ParentRegistrationViewController
    {
        [Outlet]
        UIKit.UIView footerView { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint headerHeightConstant { get; set; }


        [Outlet]
        UIKit.UIImageView headerImageView { get; set; }


        [Outlet]
        UIKit.UIStackView headerStackView { get; set; }


        [Outlet]
        UIKit.UILabel headerTitleLbl { get; set; }


        [Outlet]
        UIKit.UIView headerView { get; set; }


        [Outlet]
        UIKit.UIButton nextBtn { get; set; }


        [Outlet]
        UIKit.UIButton prewBtn { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint subViewsContentHeightConstraint { get; set; }


        [Outlet]
        UIKit.UIView viewForPager { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (footerView != null) {
                footerView.Dispose ();
                footerView = null;
            }

            if (headerHeightConstant != null) {
                headerHeightConstant.Dispose ();
                headerHeightConstant = null;
            }

            if (headerImageView != null) {
                headerImageView.Dispose ();
                headerImageView = null;
            }

            if (headerStackView != null) {
                headerStackView.Dispose ();
                headerStackView = null;
            }

            if (headerTitleLbl != null) {
                headerTitleLbl.Dispose ();
                headerTitleLbl = null;
            }

            if (headerView != null) {
                headerView.Dispose ();
                headerView = null;
            }

            if (nextBtn != null) {
                nextBtn.Dispose ();
                nextBtn = null;
            }

            if (prewBtn != null) {
                prewBtn.Dispose ();
                prewBtn = null;
            }

            if (subViewsContentHeightConstraint != null) {
                subViewsContentHeightConstraint.Dispose ();
                subViewsContentHeightConstraint = null;
            }

            if (viewForPager != null) {
                viewForPager.Dispose ();
                viewForPager = null;
            }
        }
    }
}