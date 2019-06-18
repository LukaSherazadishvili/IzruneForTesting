// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Izrune.iOS.CollectionViewCells
{
    [Register ("PacketCollectionViewCell")]
    partial class PacketCollectionViewCell
    {
        [Outlet]
        UIKit.UIView mainView { get; set; }


        [Outlet]
        UIKit.UILabel monthLbl { get; set; }


        [Outlet]
        UIKit.UILabel oldPriceLbl { get; set; }


        [Outlet]
        UIKit.UILabel priceLbl { get; set; }


        [Outlet]
        UIKit.UIView priceView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }

            if (monthLbl != null) {
                monthLbl.Dispose ();
                monthLbl = null;
            }

            if (oldPriceLbl != null) {
                oldPriceLbl.Dispose ();
                oldPriceLbl = null;
            }

            if (priceLbl != null) {
                priceLbl.Dispose ();
                priceLbl = null;
            }

            if (priceView != null) {
                priceView.Dispose ();
                priceView = null;
            }
        }
    }
}