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
    [Register ("EditProfileViewController")]
    partial class EditProfileViewController
    {
        [Outlet]
        UIKit.UIButton addStudentBtn { get; set; }


        [Outlet]
        UIKit.UIStackView editStackView { get; set; }


        [Outlet]
        UIKit.UIView firstShadowView { get; set; }


        [Outlet]
        UIKit.UIView secondShadowView { get; set; }


        [Outlet]
        UIKit.UIView thirdShadovView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (addStudentBtn != null) {
                addStudentBtn.Dispose ();
                addStudentBtn = null;
            }

            if (editStackView != null) {
                editStackView.Dispose ();
                editStackView = null;
            }

            if (firstShadowView != null) {
                firstShadowView.Dispose ();
                firstShadowView = null;
            }

            if (secondShadowView != null) {
                secondShadowView.Dispose ();
                secondShadowView = null;
            }

            if (thirdShadovView != null) {
                thirdShadovView.Dispose ();
                thirdShadovView = null;
            }
        }
    }
}