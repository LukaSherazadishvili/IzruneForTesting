using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class TestCollectionViewCell : UICollectionViewCell, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
    {
        public static readonly NSString Key = new NSString("TestCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("TestCellIdentifier");

        const float pageSpacing = 10;

        public nfloat imagesCollectioHeight { get; set; } = 180;
        public nfloat answersCollectioHeight { get; set; }

        public static nfloat CellSize { get; set; }

        static TestCollectionViewCell()
        {
            Nib = UINib.FromName("TestCollectionViewCell", NSBundle.MainBundle);
        }

        protected TestCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IQuestion question)
        {
            questionLbl.Text = question?.title;

            CalculateImagesCollectionViewHeight(question);

        }

        private void CalculateImagesCollectionViewHeight(IQuestion question)
        {
            var imagesCount = question?.images?.Count();
            if (question?.images == null || imagesCount == 0)
                imagesCollectioHeight = 0;

            if (imagesCount > 0 && imagesCount <= 2)
                imagesCollectioHeight = 90;

            else
                imagesCollectioHeight = 180;

            imagesCollectionViewHeight.Constant = imagesCollectioHeight;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if(collectionView == questionImagesCollectionView)
            {
                var questionCell = questionImagesCollectionView.DequeueReusableCell(QuestionImageCollectionViewCell.Identifier, indexPath) as QuestionImageCollectionViewCell;
                return questionCell;
            }

            var cell = answerCollectionView.DequeueReusableCell(AnswerCollectionViewCell.Identifier, indexPath) as AnswerCollectionViewCell;

            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (collectionView == questionImagesCollectionView)
                return 1;
            else
                return 2;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            if (collectionView == questionImagesCollectionView)
                return new CoreGraphics.CGSize(collectionView.Frame.Width * 0.5, collectionView.Frame.Height * 0.5);

            return new CoreGraphics.CGSize(collectionView.Frame.Width, 60);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            InitCollectionViewSettings();

            InitImagesCollectionViewLayout();

            var height = questionLbl.Bounds.Size.Height;

            CellSize = height + imagesCollectioHeight + answersCollectioHeight;
        }

        private void InitImagesCollectionViewLayout()
        {
            var layout = new UICollectionViewFlowLayout();
            layout.ItemSize = new CoreGraphics.CGSize(this.Frame.Width + pageSpacing, this.Frame.Height);
            layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            layout.MinimumLineSpacing = 0;
            layout.MinimumInteritemSpacing = 0;

            var frame = new CoreGraphics.CGRect(0, 0, this.Frame.Width + pageSpacing, this.Frame.Height);
            questionImagesCollectionView.Frame = frame;
            questionImagesCollectionView.CollectionViewLayout = layout;
        }

        private void InitCollectionViewSettings()
        {
            questionImagesCollectionView.RegisterNibForCell(QuestionImageCollectionViewCell.Nib, QuestionImageCollectionViewCell.Identifier);
            questionImagesCollectionView.Delegate = this;
            questionImagesCollectionView.DataSource = this;

            answerCollectionView.RegisterNibForCell(AnswerCollectionViewCell.Nib, AnswerCollectionViewCell.Identifier);
            answerCollectionView.Delegate = this;
            answerCollectionView.DataSource = this;
        }

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView)
        {
            if (collectionView == questionImagesCollectionView)
                return 2;
            return 1;
        }

    }
}
