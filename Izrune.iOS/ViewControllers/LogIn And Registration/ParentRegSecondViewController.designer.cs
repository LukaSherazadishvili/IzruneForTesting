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
    [Register ("ParentRegSecondViewController")]
    partial class ParentRegSecondViewController
    {
        [Outlet]
        UIKit.UITextField emailTextField { get; set; }


        [Outlet]
        UIKit.UITextField passwordTextField { get; set; }


        [Outlet]
        UIKit.UITextField phoneTextField { get; set; }


        [Outlet]
        UIKit.UITextField repeatPasswordTextField { get; set; }


        [Outlet]
        UIKit.UIStackView textFieldsStackView { get; set; }


        [Outlet]
        UIKit.UITextField userNameTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (emailTextField != null) {
                emailTextField.Dispose ();
                emailTextField = null;
            }

            if (passwordTextField != null) {
                passwordTextField.Dispose ();
                passwordTextField = null;
            }

            if (phoneTextField != null) {
                phoneTextField.Dispose ();
                phoneTextField = null;
            }

            if (repeatPasswordTextField != null) {
                repeatPasswordTextField.Dispose ();
                repeatPasswordTextField = null;
            }

            if (textFieldsStackView != null) {
                textFieldsStackView.Dispose ();
                textFieldsStackView = null;
            }

            if (userNameTextField != null) {
                userNameTextField.Dispose ();
                userNameTextField = null;
            }
        }
    }
}