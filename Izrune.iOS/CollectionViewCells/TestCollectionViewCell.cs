using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public nfloat imagesCollectioHeight { get; set; } 
        public nfloat answersCollectioHeight { get; set; }

        public nfloat CellSize { get; set; }

        public Action<IAnswer> AnswerClicked { get; set; }

        public Action<string> ImageClicked { get; set; }

        private List<string> NumberList = new List<string>()
        {
            "ა",
            "ბ",
            "გ",
            "დ"
        };

        public bool IsResultCell { get; set; }

        IQuestion Question;

        static TestCollectionViewCell()
        {
            Nib = UINib.FromName("TestCollectionViewCell", NSBundle.MainBundle);
        }

        protected TestCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IQuestion question, string index="")
        {
            Question = question;

            //InitCollectionViews();

            SetCellHeight(question);
            questionLbl.Text = $"{index}{ GetStringFromHtml(question.title)}";

            //CalculateImagesCollectionViewHeight(question);

            imagesCollectionViewHeight.Constant = imagesCollectioHeight;

            questionImagesCollectionView.Frame = new CGRect(0, 0, questionImagesCollectionView.Frame.Width,
                imagesCollectioHeight);

            answerCollectionViewHeight.Constant = answersCollectioHeight;

            questionImagesCollectionView.ReloadData();
            answerCollectionView.ReloadData();

            if (IsResultCell)
                ShowBottomLine();
        }

        public void InitDataForResult(IFinalQuestion finalQuestion, string index = "")
        {
            SetCellHeight(finalQuestion);

            questionLbl.Text = $"{index}{ GetStringFromHtml(finalQuestion.title)}";
            //questionLbl.Text = $"{index}{finalQuestion.title}";

            imagesCollectionViewHeight.Constant = imagesCollectioHeight;

            questionImagesCollectionView.Frame = new CGRect(0, 0, questionImagesCollectionView.Frame.Width,
                imagesCollectioHeight);

            answerCollectionViewHeight.Constant = answersCollectioHeight;

            questionImagesCollectionView.ReloadData();
            answerCollectionView.ReloadData();
        }

        private void InitCollectionViews()
        {
            questionImagesCollectionView.Delegate = this;
            questionImagesCollectionView.DataSource = this;

            answerCollectionView.Delegate = this;
            answerCollectionView.DataSource = this;
        }

        private void CalculateImagesCollectionViewHeight(IQuestion question)
        {
            var imagesCount = question?.images?.Count();
            if (question?.images == null || imagesCount == 0)
                imagesCollectioHeight = 90;

            if (imagesCount > 0 && imagesCount <= 2)
                imagesCollectioHeight = 90;

            else
                imagesCollectioHeight = 90;

            imagesCollectionViewHeight.Constant = imagesCollectioHeight;

            questionImagesCollectionView.Frame = new CGRect(0, 0, questionImagesCollectionView.Frame.Width,
                imagesCollectioHeight);
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if (collectionView == questionImagesCollectionView)
            {
                var questionCell = questionImagesCollectionView.DequeueReusableCell(QuestionImageCollectionViewCell.Identifier, indexPath) as QuestionImageCollectionViewCell;

                questionImagesCollectionView.Delegate = this;

                var currData = Question?.images?.ElementAt(indexPath.Row);
                questionCell.InitData(currData);


                questionCell.ImageClicked = (image) =>
                {
                    //TODO
                    ImageClicked?.Invoke(image);
                };

                return questionCell;
            }


            //Answer Cell
            var cell = answerCollectionView.DequeueReusableCell(AnswerCollectionViewCell.Identifier, indexPath) as AnswerCollectionViewCell;
            //cell.ContentView.BackgroundColor = UIColor.Yellow;
            cell.IsResult = IsResultCell;

            var data = Question?.Answers?.ElementAt(indexPath.Row);

            var currQuestion = Question as IFinalQuestion;

            if(IsResultCell)
            {
                if (indexPath.Row == currQuestion.StudentAnswerIndex)
                {
                    cell.InitData(data, NumberList?[indexPath.Row], true);

                    return cell;
                }
                else if (data.IsRight)
                    cell.InitData(data, NumberList?[indexPath.Row], true);
                else
                    cell.InitData(data, NumberList?[indexPath.Row], false);

                return cell;
            }

            cell.InitData(data, NumberList?[indexPath.Row]);

            cell.AnswerClicked = (answer) =>
            {
                AnswerClicked?.Invoke(answer);

                var answers = Question?.Answers?.ToList();
                var correctAnswer = answers?.IndexOf(answers?.FirstOrDefault(x => x.IsRight == true));

                var answerCell = answerCollectionView.CellForItem(NSIndexPath.FromRowSection((System.nint)correctAnswer, 0)) as AnswerCollectionViewCell;



                answerCell.CheckAnswer(true);
            };

            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (collectionView == questionImagesCollectionView)
            {
                var count = Question?.images?.Count() ?? 0;
                Debug.WriteLine($"Question Images Count : {count}");
                return count;
            }
            else
                return Question?.Answers?.Count()?? 0;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            if (collectionView == questionImagesCollectionView)
            {
                var imagesCount = Question?.images?.Count();

                if(imagesCount == 1)
                    return new CoreGraphics.CGSize(collectionView.Frame.Width, collectionView.Frame.Height);
                if (imagesCount == 2)
                {
                    return new CoreGraphics.CGSize(collectionView.Frame.Width * 0.5, collectionView.Frame.Height);
                }

                return new CoreGraphics.CGSize(collectionView.Frame.Width*0.5, collectionView.Frame.Height* 0.5);
            }


            var answer = Question?.Answers?.ElementAt(indexPath.Row).title;

            var titleHeight = answer.GetStringHeight((float)collectionView.Frame.Width, 64, 15);

            var size = new CoreGraphics.CGSize(collectionView.Frame.Width, titleHeight + 40);
            return size;
            //if(titleHeight >=40)
            //{
            //    var size =  new CoreGraphics.CGSize(collectionView.Frame.Width, titleHeight + 40);
            //    return size;
            //}
            //else
            //{
            //    var size = new CoreGraphics.CGSize(collectionView.Frame.Width, 60);
            //    return size;
            //}

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            InitCollectionViewSettings();

            InitImagesCollectionViewLayout();

            //mainView.Layer.BorderWidth = 2;
            //mainView.Layer.BorderColor = UIColor.Red.CGColor;
            //var height = questionLbl.Bounds.Size.Height;

            //CellSize = height + imagesCollectioHeight + answersCollectioHeight;
        }

        private void InitImagesCollectionViewLayout()
        {
            var layout = new UICollectionViewFlowLayout();
            //layout.ItemSize = new CoreGraphics.CGSize(this.Frame.Width + pageSpacing, this.Frame.Height);
            //layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
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
            //if (collectionView == questionImagesCollectionView)
                //return 2;
            return 1;
        }

        void SetCellHeight(IQuestion question)
        {
            imagesCollectioHeight = 0;

            var data = question;

            var ImagesCount = data?.images?.Count();

            if (ImagesCount == 0)
                imagesCollectioHeight = 0;
            else
                imagesCollectioHeight = 180;

            foreach (var item in data?.Answers)
            {
                var height = item.title.GetStringHeight((float)this.Frame.Width - 60, 64, 15);

                answersCollectioHeight += height + 40;
            }
        }

        public void ShowBottomLine()
        {
            topLine.Hidden = true;
            bottomLine.Hidden = false;
        }

        private string GetStringFromHtml(string htmlString)
        {
            var attr = new NSAttributedStringDocumentAttributes();

            var nsError = new NSError();

            attr.DocumentType = NSDocumentType.HTML;

            var myHtmlData = NSData.FromString(htmlString, NSStringEncoding.Unicode);

            var data = new NSAttributedString(myHtmlData, attr, ref nsError);

            //var data = new NSAttributedString($"<span>{htmlString}</span>", attr, ref nsError);

            return data.Value;
        }
    }
}
