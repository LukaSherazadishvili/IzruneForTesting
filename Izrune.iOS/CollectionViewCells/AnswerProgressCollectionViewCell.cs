using System;

using Foundation;
using IZrune.PCL.Helpers;
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

        public void InitData(QuisSheduler quisSheduler, bool hideLeft = false, bool hideRight = false)
        {
            InitViews(quisSheduler);

            leftView.Hidden = hideLeft;
            rightView.Hidden = hideRight;
        }

        private void InitViews(QuisSheduler quisSheduler)
        {
            if(quisSheduler.AlreadeBe)
            {
                checkImageView.Hidden = false;
                answerNumberView.Hidden = true;
                undefinedView.Hidden = true;
                checkImageView.Image = UIImage.FromBundle("1 – 5.png");
            }

            if(quisSheduler.IsCurrent)
            {
                checkImageView.Hidden = true;
                answerNumberView.Hidden = false;
                undefinedView.Hidden = true;
                answerNumberLbl.Text = (quisSheduler.Position + 1).ToString();
            }

            if(!quisSheduler.IsCurrent && !quisSheduler.AlreadeBe)
            {
                undefinedView.Hidden = false;
                answerNumberView.Hidden = true;
                checkImageView.Hidden = true;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            //answerNumberView.Layer.CornerRadius = 12.5f;

            undefinedView.Layer.CornerRadius = 6.5f;
            undefinedView.Layer.BorderWidth = 1;
            undefinedView.Layer.BorderColor = UIColor.FromRGB(184, 184, 184).CGColor;
        }
    }
}
