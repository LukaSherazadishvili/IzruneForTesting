using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class PacketCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("PacketCollectionViewCell");
        public static readonly UINib Nib;

        static PacketCollectionViewCell()
        {
            Nib = UINib.FromName("PacketCollectionViewCell", NSBundle.MainBundle);
        }

        protected PacketCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
