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
    [Register ("ChooseTimeViewController")]
    partial class ChooseTimeViewController
    {
        [Outlet]
        UIKit.UIImageView closePopUpView { get; set; }


        [Outlet]
        UIKit.UIView derivedTimeShadowView { get; set; }


        [Outlet]
        UIKit.UIView derivedTimeView { get; set; }


        [Outlet]
        UIKit.UIView popUpView { get; set; }


        [Outlet]
        UIKit.UIView shadowView1 { get; set; }


        [Outlet]
        UIKit.UIView shadowView2 { get; set; }


        [Outlet]
        UIKit.UIView totalTimeShadowView { get; set; }


        [Outlet]
        UIKit.UIView totalTimeView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (closePopUpView != null) {
                closePopUpView.Dispose ();
                closePopUpView = null;
            }

            if (derivedTimeShadowView != null) {
                derivedTimeShadowView.Dispose ();
                derivedTimeShadowView = null;
            }

            if (derivedTimeView != null) {
                derivedTimeView.Dispose ();
                derivedTimeView = null;
            }

            if (popUpView != null) {
                popUpView.Dispose ();
                popUpView = null;
            }

            if (totalTimeShadowView != null) {
                totalTimeShadowView.Dispose ();
                totalTimeShadowView = null;
            }

            if (totalTimeView != null) {
                totalTimeView.Dispose ();
                totalTimeView = null;
            }
        }
    }
}