using System;

using Foundation;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public enum AnswerStatus
    {
        Checked,
        Current,
        Unknown
    }

    public partial class AnswerProgressCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("AnswerProgressCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("AnswerProgressCellIdentifier");
        public AnswerStatus AnswerStatus = AnswerStatus.Unknown;

        static AnswerProgressCollectionViewCell()
        {
            Nib = UINib.FromName("AnswerProgressCollectionViewCell", NSBundle.MainBundle);
        }

        protected AnswerProgressCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(AnswerStatus status, int index)
        {
            InitViews(status, index);
        }

        private void InitViews(AnswerStatus status, int index)
        {
            if(status == AnswerStatus.Checked)
            {
                checkImageView.Hidden = false;
                answerNumberView.Hidden = true;
            }

            if(status == AnswerStatus.Current)
            {
                checkImageView.Hidden = true;
                answerNumberView.Hidden = false;
                answerNumberLbl.Text = index.ToString();
            }
            if(status == AnswerStatus.Unknown)
            {
                answerNumberView.Hidden = true;

                var emptyImage = new UIImage();
                checkImageView.Image = emptyImage.GetImageWithColor(UIColor.White);
                checkImageView.Hidden = false;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            answerNumberView.Layer.CornerRadius = 12.5f;
        }
    }
}
