﻿using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class TestCollectionViewCell : UICollectionViewCell, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
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

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if(collectionView == questionImagesCollectionView)
            {
                var questionCell = questionImagesCollectionView.DequeueReusableCell(QuestionImageCollectionViewCell.Identifier, indexPath) as QuestionImageCollectionViewCell;
                return questionCell;
            }

            var cell = answerCollectionView.DequeueReusableCell(AnswerCollectionViewCell.Identifier, indexPath) as AnswerCollectionViewCell;

            //var data = "Test Data";

            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (collectionView == questionImagesCollectionView)
                return 4;
            else
                return 4;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            if (collectionView == questionImagesCollectionView)
                return new CoreGraphics.CGSize(collectionView.Frame.Width, collectionView.Frame.Height);

            return new CoreGraphics.CGSize(collectionView.Frame.Width, 60);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            questionImagesCollectionView.RegisterNibForCell(QuestionImageCollectionViewCell.Nib, QuestionImageCollectionViewCell.Identifier);
            questionImagesCollectionView.Delegate = this;
            questionImagesCollectionView.DataSource = this;

            answerCollectionView.RegisterNibForCell(AnswerCollectionViewCell.Nib, AnswerCollectionViewCell.Identifier);
            answerCollectionView.Delegate = this;
            answerCollectionView.DataSource = this;
        }

        [Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
        public nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0;
        }
    }
}
