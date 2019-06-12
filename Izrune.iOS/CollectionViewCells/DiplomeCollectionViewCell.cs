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

        public Action CellClicked { get; set; }

        static DiplomeCollectionViewCell()
        {
            Nib = UINib.FromName("DiplomeCollectionViewCell", NSBundle.MainBundle);
        }

        protected DiplomeCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData()
        {
            //TODO
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            mainView.Layer.BorderWidth = 2;
            mainView.Layer.BorderColor = UIColor.White.CGColor;

            mainView.Layer.CornerRadius = 20;

            if(mainView.GestureRecognizers == null || mainView.GestureRecognizers?.Length == 0)
            {
                mainView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    CellClicked?.Invoke();
                }));
            }
        }
    }
}
