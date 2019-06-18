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
    [Register ("TestResultsViewController")]
    partial class TestResultsViewController
    {
        [Outlet]
        UIKit.UIView monthDropdownView { get; set; }


        [Outlet]
        UIKit.UILabel monthLbl { get; set; }


        [Outlet]
        UIKit.UICollectionView resultCollectionView { get; set; }


        [Outlet]
        UIKit.UIView yearDropdownView { get; set; }


        [Outlet]
        UIKit.UILabel yearLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (monthDropdownView != null) {
                monthDropdownView.Dispose ();
                monthDropdownView = null;
            }

            if (monthLbl != null) {
                monthLbl.Dispose ();
                monthLbl = null;
            }

            if (resultCollectionView != null) {
                resultCollectionView.Dispose ();
                resultCollectionView = null;
            }

            if (yearDropdownView != null) {
                yearDropdownView.Dispose ();
                yearDropdownView = null;
            }

            if (yearLbl != null) {
                yearLbl.Dispose ();
                yearLbl = null;
            }
        }
    }
}