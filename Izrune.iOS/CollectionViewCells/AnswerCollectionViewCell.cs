using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class AnswerCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("AnswerCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("AnswerCellIdentifier");

        static AnswerCollectionViewCell()
        {
            Nib = UINib.FromName("AnswerCollectionViewCell", NSBundle.MainBundle);
        }

        protected AnswerCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            answerView.Layer.CornerRadius = 20;
            answerView.Layer.BorderWidth = 2;
            answerView.Layer.BorderColor = UIColor.Red.CGColor;

            numberView.Layer.CornerRadius = 20;
        }
    }
}
