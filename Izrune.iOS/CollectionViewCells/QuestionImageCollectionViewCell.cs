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

        public Action<string> ImageClicked { get; set; }

        string ImageUrl;

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
            questionImageView.Image = null;
            questionImageView.InitImageFromWeb(url, false, false);
            ImageUrl = url;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            questiomImageViewHolder.Layer.CornerRadius = 10;
            questionImageView.Layer.CornerRadius = 10;

            if (questiomImageViewHolder.GestureRecognizers == null || questiomImageViewHolder.GestureRecognizers?.Length == 0)
                questiomImageViewHolder.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    ImageClicked?.Invoke(ImageUrl);
                }));
        }

    }
}
