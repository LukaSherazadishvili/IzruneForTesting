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
    [Register ("PacketViewController")]
    partial class PacketViewController
    {
        [Outlet]
        UIKit.NSLayoutConstraint footerHeightConstraint { get; set; }


        [Outlet]
        UIKit.UIView footerView { get; set; }


        [Outlet]
        UIKit.UILabel headerTitleLbl { get; set; }


        [Outlet]
        UIKit.UILabel individualLbl { get; set; }


        [Outlet]
        UIKit.UIButton nextBtn { get; set; }


        [Outlet]
        UIKit.UIButton prevBtn { get; set; }


        [Outlet]
        UIKit.UILabel promoLbl { get; set; }


        [Outlet]
        UIKit.UIView viewForIndividual { get; set; }


        [Outlet]
        UIKit.UIView viewForPeager { get; set; }


        [Outlet]
        UIKit.UIView viewForPromoCode { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (individualLbl != null) {
                individualLbl.Dispose ();
                individualLbl = null;
            }

            if (promoLbl != null) {
                promoLbl.Dispose ();
                promoLbl = null;
            }

            if (viewForIndividual != null) {
                viewForIndividual.Dispose ();
                viewForIndividual = null;
            }

            if (viewForPeager != null) {
                viewForPeager.Dispose ();
                viewForPeager = null;
            }

            if (viewForPromoCode != null) {
                viewForPromoCode.Dispose ();
                viewForPromoCode = null;
            }
        }
    }
}