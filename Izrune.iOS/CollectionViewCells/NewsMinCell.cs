using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class NewsMinCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("NewsMinCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("NewsMinCellIdentifier");
        static NewsMinCell()
        {
            Nib = UINib.FromName("NewsMinCell", NSBundle.MainBundle);
        }

        protected NewsMinCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
