using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class DiplomeCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("DiplomeCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("DiplomeCellIdentifier");
        static DiplomeCollectionViewCell()
        {
            Nib = UINib.FromName("DiplomeCollectionViewCell", NSBundle.MainBundle);
        }

        protected DiplomeCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            mainView.Layer.BorderWidth = 2;
            mainView.Layer.BorderColor = UIColor.White.CGColor;

            mainView.Layer.CornerRadius = 20;
        }
    }
}
