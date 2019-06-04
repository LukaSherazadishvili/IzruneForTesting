using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class ResultCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ResultCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("ResultCellIdentifier");

        static ResultCollectionViewCell()
        {
            Nib = UINib.FromName("ResultCollectionViewCell", NSBundle.MainBundle);
        }

        protected ResultCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }


    }
}
