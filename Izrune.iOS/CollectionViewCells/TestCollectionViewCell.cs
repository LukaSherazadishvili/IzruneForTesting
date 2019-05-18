using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class TestCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("TestCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("TestCellIdentifier");

        static TestCollectionViewCell()
        {
            Nib = UINib.FromName("TestCollectionViewCell", NSBundle.MainBundle);
        }

        protected TestCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
