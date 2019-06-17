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
    [Register ("LogInViewController")]
    partial class LogInViewController
    {
        [Outlet]
        UIKit.UILabel forgotPasswordLbl { get; set; }


        [Outlet]
        UIKit.UILabel forgotUserNameLbl { get; set; }


        [Outlet]
        UIKit.UIButton logInBtn { get; set; }


        [Outlet]
        UIKit.UIView loginShadowVoew { get; set; }


        [Outlet]
        UIKit.UIView loginView { get; set; }


        [Outlet]
        UIKit.UITextField passwordTextField { get; set; }


        [Outlet]
        UIKit.UIButton registrationBtn { get; set; }


        [Outlet]
        UIKit.UIView registrationView { get; set; }


        [Outlet]
        UIKit.UIImageView showPasswordIcon { get; set; }


        [Outlet]
        UIKit.UITextField userNameTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (forgotPasswordLbl != null) {
                forgotPasswordLbl.Dispose ();
                forgotPasswordLbl = null;
            }

            if (forgotUserNameLbl != null) {
                forgotUserNameLbl.Dispose ();
                forgotUserNameLbl = null;
            }

            if (logInBtn != null) {
                logInBtn.Dispose ();
                logInBtn = null;
            }

            if (passwordTextField != null) {
                passwordTextField.Dispose ();
                passwordTextField = null;
            }

            if (registrationBtn != null) {
                registrationBtn.Dispose ();
                registrationBtn = null;
            }

            if (userNameTextField != null) {
                userNameTextField.Dispose ();
                userNameTextField = null;
            }
        }
    }
}