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
    [Register ("AddStudentViewController")]
    partial class AddStudentViewController
    {
        [Outlet]
        UIKit.UIButton addNewStudentBtn { get; set; }


        [Outlet]
        UIKit.UIView agreeView { get; set; }


        [Outlet]
        UIKit.UIButton backBtn { get; set; }


        [Outlet]
        UIKit.UIButton nextBtn { get; set; }


        [Outlet]
        UIKit.UILabel privacyLbl { get; set; }


        [Outlet]
        UIKit.UIView selectedAgreeView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (addNewStudentBtn != null) {
                addNewStudentBtn.Dispose ();
                addNewStudentBtn = null;
            }

            if (agreeView != null) {
                agreeView.Dispose ();
                agreeView = null;
            }

            if (privacyLbl != null) {
                privacyLbl.Dispose ();
                privacyLbl = null;
            }

            if (selectedAgreeView != null) {
                selectedAgreeView.Dispose ();
                selectedAgreeView = null;
            }
        }
    }
}