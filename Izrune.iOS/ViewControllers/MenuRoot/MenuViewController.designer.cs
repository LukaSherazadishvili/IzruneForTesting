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
    [Register ("MenuViewController")]
    partial class MenuViewController
    {
        [Outlet]
        UIKit.UICollectionView menuCollectionView { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint menuCollectionViewTopSpace { get; set; }


        [Outlet]
        UIKit.UIStackView profileNumberStackView { get; set; }


        [Outlet]
        UIKit.UILabel userNameLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (menuCollectionView != null) {
                menuCollectionView.Dispose ();
                menuCollectionView = null;
            }

            if (menuCollectionViewTopSpace != null) {
                menuCollectionViewTopSpace.Dispose ();
                menuCollectionViewTopSpace = null;
            }

            if (profileNumberStackView != null) {
                profileNumberStackView.Dispose ();
                profileNumberStackView = null;
            }

            if (userNameLbl != null) {
                userNameLbl.Dispose ();
                userNameLbl = null;
            }
        }
    }
}