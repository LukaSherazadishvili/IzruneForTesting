using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class BadgeCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("BadgeCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("BadgeCellIdentifier");
        static BadgeCell()
        {
            Nib = UINib.FromName("BadgeCell", NSBundle.MainBundle);
        }

        protected BadgeCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IBadges badges)
        {
            badgeImageView.InitImageFromWeb(badges?.ImageURl, false, false);
        }
    }
}
