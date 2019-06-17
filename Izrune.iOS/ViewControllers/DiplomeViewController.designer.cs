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
    [Register ("DiplomeViewController")]
    partial class DiplomeViewController
    {
        [Outlet]
        UIKit.UIButton backBtn { get; set; }


        [Outlet]
        UIKit.UICollectionView diplomeCollectionView { get; set; }


        [Outlet]
        UIKit.UILabel diplomeLbl { get; set; }


        [Outlet]
        UIKit.UIImageView headerImageView { get; set; }


        [Outlet]
        UIKit.UILabel headerLbl { get; set; }


        [Outlet]
        UIKit.UIView viewForDropDown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel testLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backBtn != null) {
                backBtn.Dispose ();
                backBtn = null;
            }

            if (diplomeCollectionView != null) {
                diplomeCollectionView.Dispose ();
                diplomeCollectionView = null;
            }

            if (diplomeLbl != null) {
                diplomeLbl.Dispose ();
                diplomeLbl = null;
            }

            if (headerImageView != null) {
                headerImageView.Dispose ();
                headerImageView = null;
            }

            if (headerLbl != null) {
                headerLbl.Dispose ();
                headerLbl = null;
            }

            if (testLbl != null) {
                testLbl.Dispose ();
                testLbl = null;
            }

            if (viewForDropDown != null) {
                viewForDropDown.Dispose ();
                viewForDropDown = null;
            }
        }
    }
}