using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class BadgeCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("BadgeCell");
        public static readonly UINib Nib;

        static BadgeCell()
        {
            Nib = UINib.FromName("BadgeCell", NSBundle.MainBundle);
        }

        protected BadgeCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
