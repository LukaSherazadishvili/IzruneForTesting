// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDC.iOS.Utils;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class TestViewController : UIViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public TestViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("TestViewControllerStoryboardId");

        public List<IQuestion> AllQuestions;

        private List<IQuestion> Questions = new List<IQuestion>();
        private float imagesHeight;
        private float answersHeight;
        private float totalHeight;
        private int currentIndex;
        private Timer timer;

        public bool IsTotalTime { get; set; } = false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            skipQuestionBtn.Layer.CornerRadius = 20;

            skipQuestionBtn.TouchUpInside += delegate
            {
                GetNextQuestion();
            };

            InitCollectionView();
            MoveToQuestions();

            questionCollectionView.ReloadData();

            InitTotalTimer(IsTotalTime? 29 : 0);

            InitCircular(IsTotalTime? 29 * 60 + 59 : 59);
        }

        private void GetNextQuestion()
        {
            Questions.Clear();
            MoveToQuestions();
            questionCollectionView.ReloadData();
        }

        private void MoveToQuestions()
        {
            if(currentIndex < AllQuestions.Count)
            {
                Questions.Add(AllQuestions?[currentIndex]);
                //AllQuestions.RemoveAt(0);
            }

        }

        private async Task LoadDataAsync()
        {
            try
            {
                var testService = ServiceContainer.ServiceContainer.Instance.Get<IQuezServices>();

                var userService = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();

                var user = await userService.GetUserAsync();

                //var data = (await testService.GetQuestionsAsync(user.id, IZrune.PCL.Enum.QuezCategory.QuezExam))?.ToList();

                //Questions = data;

                questionCollectionView.ReloadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitCollectionView()
        {
            questionCollectionView.RegisterNibForCell(TestCollectionViewCell.Nib, TestCollectionViewCell.Identifier);

            questionCollectionView.Delegate = this;
            questionCollectionView.DataSource = this;

            answerProgressCollectionView.RegisterNibForCell(AnswerProgressCollectionViewCell.Nib, AnswerProgressCollectionViewCell.Identifier);

            answerProgressCollectionView.Delegate = this;
            answerProgressCollectionView.DataSource = this;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if(collectionView == answerProgressCollectionView)
            {
                var answerCell = answerProgressCollectionView.DequeueReusableCell(AnswerProgressCollectionViewCell.Identifier, indexPath) as AnswerProgressCollectionViewCell;

                return answerCell;
            }

            var cell = questionCollectionView.DequeueReusableCell(TestCollectionViewCell.Identifier, indexPath) as TestCollectionViewCell;


            var data = AllQuestions[currentIndex];

            cell.AnswerClicked = async (question) =>
            {
                //TODO Scroll Progress CollectionView
                //question.Status = AnswerStatus.Current;

                timeLbl.Text = ($"01:00");
                var timeSpan = DateTime.Now;
                Debug.WriteLine($"First Time : {timeSpan.Millisecond}");

                await Task.Delay(200);

                if (currentIndex < AllQuestions?.Count - 1)
                    currentIndex++;
                else
                {
                    //TODO End Test And Submit
                    this.NavigationController.PopViewController(true);
                }
                GetNextQuestion();
                answerProgressCollectionView.ReloadData();

                if (!IsTotalTime)
                {
                    timer.Dispose();
                    InitTotalTimer(0);
                }

                if (!IsTotalTime)
                {
                    InitCircular(59);

                }

                timeSpan = DateTime.Now;
                Debug.WriteLine($"Last Time : {timeSpan.Millisecond}");
            };

            cell.InitData(data);
            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if(collectionView == answerProgressCollectionView)
                return AllQuestions?.Count?? 0;
            return 1;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {

            //TODO Calculate CellHeight

            if (collectionView == answerProgressCollectionView)
                return new CoreGraphics.CGSize(40, 30);

            if(currentIndex < AllQuestions?.Count)
                SetCellHeight(AllQuestions?[currentIndex]);

            return new CoreGraphics.CGSize(collectionView.Frame.Width, totalHeight + 60);
        }

        void SetCellHeight(IQuestion question)
        {
            totalHeight = 0;
            answersHeight = 0;

            var data = question;
            var text = data.title;
            var titleHeight = text.GetStringHeight((float)questionCollectionView.Frame.Width, 50, 17);
            var ImagesCount = data?.images?.Count();
            if (ImagesCount == 0)
                imagesHeight = 0;
            else if (ImagesCount > 0 && ImagesCount <= 2)
                imagesHeight = 90;
            else
                imagesHeight = 180;

            float spaceSumBetweenAnswers = 80;

            foreach (var item in data?.Answers)
            {
                var height = item.title.GetStringHeight((float)questionCollectionView.Frame.Width, 64, 15);
                answersHeight += height + 40;
            }

            totalHeight = titleHeight + imagesHeight + answersHeight + spaceSumBetweenAnswers + 50;

            //return totalHeight;
        }

        private void InitTotalTimer(int _minutes)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;

            var minutes = _minutes;
            var secondes = 60;

            string Stringminutes;
            string Stringsecondes;

            timer.Elapsed += (sender, e) => {

                if (secondes == 0)
                {
                    if (minutes <= 0 && secondes <= 0)
                    {
                        timer.Enabled = false;
                        timer.Stop();
                        //TODO SkipQuestion
                    }

                    minutes--;
                    secondes = 60;
                }
                    
                secondes--;

                if (minutes < 10 && minutes >= 0)
                    Stringminutes = $"0{minutes}";
                else
                    Stringminutes = minutes.ToString();

                if (secondes < 10 && secondes >= 0)
                    Stringsecondes = $"0{secondes}";
                else
                    Stringsecondes = secondes.ToString();

                InvokeOnMainThread(() => timeLbl.Text = $"{Stringminutes}:{Stringsecondes}");

            };

            timer.Start();
        }

        private void ShowLoginAlert()
        {
            var alert = UIAlertController.Create("ყურადღევა", "დრო ამოიწურა.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void InitCircular(double duration)
        {
            #region CircularAnimation
            var progressLayer = new CAShapeLayer();
            var trackLayer = new CAShapeLayer();

            var progressColor = AppColors.TitleColor;
            var trackColor = UIColor.Clear.FromHexString("EDEDED");

            progressLayer.StrokeColor = progressColor.CGColor;
            trackLayer.StrokeColor = trackColor.CGColor;

            viewForCircular.BackgroundColor = UIColor.Clear;
            viewForCircular.ClipsToBounds = true;
            viewForCircular.Layer.CornerRadius = 50;

            var circlePath = UIBezierPath.FromArc(new CGPoint(viewForCircular.Frame.Width / 2, viewForCircular.Frame.Height / 2), (System.nfloat)((viewForCircular.Frame.Size.Width - 1.5) / 2),
                (System.nfloat)(-0.5 * Math.PI), (System.nfloat)(1.5 * Math.PI), true);

            trackLayer.Path = circlePath.CGPath;
            trackLayer.FillColor = UIColor.Clear.CGColor;
            trackLayer.StrokeColor = trackColor.CGColor;

            trackLayer.LineWidth = 10.0f;
            trackLayer.StrokeEnd = 1.0f;

            viewForCircular.Layer.AddSublayer(trackLayer);

            progressLayer.Path = circlePath.CGPath;
            progressLayer.FillColor = UIColor.Clear.CGColor;
            progressLayer.StrokeColor = progressColor.CGColor;
            progressLayer.LineWidth = 10.0f;
            progressLayer.StrokeEnd = 0;

            viewForCircular.Layer.AddSublayer(progressLayer);

            var animation = CABasicAnimation.FromKeyPath("strokeEnd");
            animation.Duration = duration;

            animation.From = NSObject.FromObject(0);
            animation.To = NSObject.FromObject(1.0f);

            animation.TimingFunction = CAMediaTimingFunction.FromName(new NSString(CAMediaTimingFunction.Linear.ToString()));
            progressLayer.StrokeEnd = 0.0f;
            progressLayer.AddAnimation(animation, "animateCircle");

            #endregion
        }
    }
}
