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
    [Register ("DiplomeCollectionViewCell")]
    partial class DiplomeCollectionViewCell
    {
        [Outlet]
        UIKit.UILabel dateLbl { get; set; }


        [Outlet]
        UIKit.UIView mainView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (dateLbl != null) {
                dateLbl.Dispose ();
                dateLbl = null;
            }

            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }
        }
    }
}