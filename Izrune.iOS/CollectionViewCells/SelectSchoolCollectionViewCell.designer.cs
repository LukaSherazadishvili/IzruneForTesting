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
    [Register ("SelectSchoolCollectionViewCell")]
    partial class SelectSchoolCollectionViewCell
    {
        [Outlet]
        UIKit.UIView mainView { get; set; }


        [Outlet]
        UIKit.UILabel schoolLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }

            if (schoolLbl != null) {
                schoolLbl.Dispose ();
                schoolLbl = null;
            }
        }
    }
}