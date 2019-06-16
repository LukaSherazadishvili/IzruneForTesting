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
    [Register ("SuccesViewController")]
    partial class SuccesViewController
    {
        [Outlet]
        UIKit.UIView checkView { get; set; }


        [Outlet]
        UIKit.UIButton loginBtn { get; set; }


        [Outlet]
        UIKit.UILabel titleLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (checkView != null) {
                checkView.Dispose ();
                checkView = null;
            }

            if (loginBtn != null) {
                loginBtn.Dispose ();
                loginBtn = null;
            }

            if (titleLbl != null) {
                titleLbl.Dispose ();
                titleLbl = null;
            }
        }
    }
}