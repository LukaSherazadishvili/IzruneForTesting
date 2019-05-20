﻿using System;

using Foundation;
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

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            questiomImageViewHolder.Layer.CornerRadius = 20;
            questionImageView.Layer.CornerRadius = 20;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            questionImageView.Frame = new CoreGraphics.CGRect(0, 0, Frame.Width, Frame.Height);
        }

    }
}
