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
    [Register ("PromoCodeViewController")]
    partial class PromoCodeViewController
    {
        [Outlet]
        UIKit.UIButton confirmBtn { get; set; }


        [Outlet]
        UIKit.UILabel monthLbl { get; set; }


        [Outlet]
        UIKit.UITextField monthTf { get; set; }


        [Outlet]
        UIKit.UIView monthView { get; set; }


        [Outlet]
        UIKit.UILabel promoCodeErorLbl { get; set; }


        [Outlet]
        UIKit.UITextField promoCodeTf { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (confirmBtn != null) {
                confirmBtn.Dispose ();
                confirmBtn = null;
            }

            if (monthLbl != null) {
                monthLbl.Dispose ();
                monthLbl = null;
            }

            if (monthView != null) {
                monthView.Dispose ();
                monthView = null;
            }

            if (promoCodeErorLbl != null) {
                promoCodeErorLbl.Dispose ();
                promoCodeErorLbl = null;
            }

            if (promoCodeTf != null) {
                promoCodeTf.Dispose ();
                promoCodeTf = null;
            }
        }
    }
}