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
    [Register ("ContactViewController")]
    partial class ContactViewController
    {
        [Outlet]
        UIKit.UIView facebookView { get; set; }


        [Outlet]
        UIKit.UIView mailView { get; set; }


        [Outlet]
        UIKit.UIView phoneView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (facebookView != null) {
                facebookView.Dispose ();
                facebookView = null;
            }

            if (mailView != null) {
                mailView.Dispose ();
                mailView = null;
            }

            if (phoneView != null) {
                phoneView.Dispose ();
                phoneView = null;
            }
        }
    }
}