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
    [Register ("PasswordRecoveryViewController")]
    partial class PasswordRecoveryViewController
    {
        [Outlet]
        UIKit.UIButton backBtn { get; set; }


        [Outlet]
        UIKit.UIImageView backImageView { get; set; }


        [Outlet]
        UIKit.UIView backView { get; set; }


        [Outlet]
        UIKit.UILabel errorLbl { get; set; }


        [Outlet]
        UIKit.UITextField phoneTextField { get; set; }


        [Outlet]
        UIKit.UIButton sendBtn { get; set; }


        [Outlet]
        UIKit.UIView sendView { get; set; }


        [Outlet]
        UIKit.UILabel titleLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (backBtn != null) {
                backBtn.Dispose ();
                backBtn = null;
            }

            if (errorLbl != null) {
                errorLbl.Dispose ();
                errorLbl = null;
            }

            if (phoneTextField != null) {
                phoneTextField.Dispose ();
                phoneTextField = null;
            }

            if (sendBtn != null) {
                sendBtn.Dispose ();
                sendBtn = null;
            }

            if (titleLbl != null) {
                titleLbl.Dispose ();
                titleLbl = null;
            }
        }
    }
}