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
    [Register ("StudentRegSecondViewController")]
    partial class StudentRegSecondViewController
    {
        [Outlet]
        UIKit.UILabel cityLbl { get; set; }


        [Outlet]
        UIKit.UIView cityView { get; set; }


        [Outlet]
        UIKit.UILabel classLbl { get; set; }


        [Outlet]
        UIKit.UIView classView { get; set; }


        [Outlet]
        UIKit.UILabel schoolLbl { get; set; }


        [Outlet]
        UIKit.UIView selectSchoolView { get; set; }


        [Outlet]
        UIKit.UITextField villageTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cityLbl != null) {
                cityLbl.Dispose ();
                cityLbl = null;
            }

            if (cityView != null) {
                cityView.Dispose ();
                cityView = null;
            }

            if (classLbl != null) {
                classLbl.Dispose ();
                classLbl = null;
            }

            if (classView != null) {
                classView.Dispose ();
                classView = null;
            }

            if (schoolLbl != null) {
                schoolLbl.Dispose ();
                schoolLbl = null;
            }

            if (selectSchoolView != null) {
                selectSchoolView.Dispose ();
                selectSchoolView = null;
            }

            if (villageTextField != null) {
                villageTextField.Dispose ();
                villageTextField = null;
            }
        }
    }
}