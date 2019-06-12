using System;

using Foundation;
using Izrune.iOS.Utils;
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
        public Action<IPrice> PriceSelected { get; set; }

        public void InitData(IPrice price, bool isSelected = false)
        {
            Price = price;

            monthLbl.Text = price?.months.ToString() + " თვე";
            priceLbl.Text = price?.price.ToString();

            SelectCell(isSelected);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            priceView.Layer.CornerRadius = 24;
            mainView.Layer.CornerRadius = 26;

            if(mainView.GestureRecognizers == null || mainView.GestureRecognizers.Length == 0)
            {
                mainView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    PriceSelected?.Invoke(Price);
                }));
            }
        }

        public void SelectCell(bool isSelected)
        {
            mainView.Layer.BorderWidth = isSelected? 3.0f : 0;
            mainView.Layer.BorderColor = AppColors.TitleColor.CGColor;
        }
    }
}
