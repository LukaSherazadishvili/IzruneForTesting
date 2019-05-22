using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using MPDC.iOS.Utils;
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

        public Action<IAnswer> AnswerClicked { get; set; }

        IQuestion Question;

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

            Question = question;
            questionLbl.Text = question?.title;

            CalculateImagesCollectionViewHeight(question);

            imagesCollectionViewHeight.Constant = 0;
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

            var data = Question?.Answers?.ElementAt(indexPath.Row);

            cell.InitData(data);

            cell.AnswerClicked = (answer) =>
            {
                AnswerClicked?.Invoke(answer);
            };
            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (collectionView == questionImagesCollectionView)
                return (nint)Question?.images?.Count();
            else
                return (nint)Question?.Answers?.Count();
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

        float GetCellHeight(IQuestion question)
        {
            var data = question;

            var appFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 17);

            var titleHeight = (float)data.title.GetSizeByText(appFont).Height;

            var ImagesCount = data?.images?.Count();

            float imagesHeight;

            if (ImagesCount == 0)
                imagesHeight = 0;
            else if (ImagesCount > 0 || ImagesCount <= 2)
                imagesHeight = 90;
            else
                imagesHeight = 180;

            float spaceSumBetweenAnswers = 80;

            float answersHeight = 0;
            foreach (var item in data?.Answers)
            {
                answersHeight += (float)item?.title.GetSizeByText(appFont).Height;
            }

            var totalHeight = titleHeight + imagesHeight + spaceSumBetweenAnswers + answersHeight;

            return totalHeight;
        }
    }
}
