using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class PacketCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("PacketCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("PacketCellIdentifier");

        static PacketCollectionViewCell()
        {
            Nib = UINib.FromName("PacketCollectionViewCell", NSBundle.MainBundle);
        }

        protected PacketCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        IPrice Price;

        public void InitData(IPrice price)
        {
            Price = price;

            monthLbl.Text = price.months.ToString() + " თვე";
            priceLbl.Text = price.price.ToString();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            priceView.Layer.CornerRadius = 20;
        }
    }
}
