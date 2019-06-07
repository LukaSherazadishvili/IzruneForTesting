using System;

using Foundation;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class QuestionImageCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("QuestionImageCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("QuestionImageCellIdentifier");
        static QuestionImageCollectionViewCell()
        {
            Nib = UINib.FromName("QuestionImageCollectionViewCell", NSBundle.MainBundle);
        }

        protected QuestionImageCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(string url)
        {
            questionImageView.InitImageFromWeb(url, false, false);

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            questiomImageViewHolder.Layer.CornerRadius = 10;
            questionImageView.Layer.CornerRadius = 10;
        }

    }
}
