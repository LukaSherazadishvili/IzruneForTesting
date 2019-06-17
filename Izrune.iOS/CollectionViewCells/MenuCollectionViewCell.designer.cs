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
    [Register ("MenuCollectionViewCell")]
    partial class MenuCollectionViewCell
    {
        [Outlet]
        UIKit.UIImageView menuImage { get; set; }


        [Outlet]
        UIKit.UILabel titleLbl { get; set; }


        [Outlet]
        UIKit.UIView transparentView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (menuImage != null) {
                menuImage.Dispose ();
                menuImage = null;
            }

            if (titleLbl != null) {
                titleLbl.Dispose ();
                titleLbl = null;
            }

            if (transparentView != null) {
                transparentView.Dispose ();
                transparentView = null;
            }
        }
    }
}