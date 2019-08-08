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
using IZrune.PCL.Enum;
using IZrune.PCL.Helpers;
using MPDC.iOS.Utils;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;
using Airbnb.Lottie;

namespace Izrune.iOS
{
	public partial class TestViewController : BaseViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public TestViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("TestViewControllerStoryboardId");

        #region Fields
        List<IQuestion> AllQuestions;
        List<IBadges> Badges;

        //private List<IQuestion> Questions = new List<IQuestion>();
        private float imagesHeight;
        private float answersHeight;
        private float totalHeight;

        private int currentIndex;
        private Timer timer;

        public QuezCategory quezCategory;

        public IStudent SelectedStudent;

        IQuestion CurrentQuestion;
        private int lastVisibleIndex;
        private CABasicAnimation strokeAnimation;
        private CAShapeLayer progressLayer;

        public bool IsTotalTime { get; set; }

        LOTAnimationView likeAnimation = new LOTAnimationView();

        int correctAnswers;

        bool isRibbonAnimation = false;
        string AnimationFilePath = "like_animation.json";

        #endregion
        const int TotalMinutes = 29;
        const int TotalSecondes = 60;

        const int SeparatedMinutes = 1;
        const int SeparatedSecondes = 30;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitCollectionView();

            await LoadDataAsync();

            //InitTotalTimer(IsTotalTime? 29 : 0);

            if (IsTotalTime)
                InitTotalTimer(29, 60);
            else
                InitTotalTimer(1, 30);

            InitCircular(IsTotalTime? 1800 : 90, 0);

            lastVisibleIndex = 7;

            InitLottie(AnimationFilePath);

            userNameLbl.Text = SelectedStudent?.Name + " " + SelectedStudent?.LastName;

            badgeCollectionView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                var badgeUrl = "http://www.izrune.ge/geo/175";
                if (UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString(badgeUrl)))
                    UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(badgeUrl));
            }));
        }

        private async Task LoadDataAsync()
        {
            ShowLoading();
            try
            {
                questionCollectionView.Hidden = true;
                var testService = ServiceContainer.ServiceContainer.Instance.Get<IQuezServices>();

                var userService = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();

                var data = (await QuezControll.Instance.GetAllQuestion(quezCategory))?.ToList();

                if (data == null || data?.Count == 0)
                {
                    this.NavigationController.PopToRootViewController(true);
                }

                var badgeService = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();
                Badges = (await badgeService.GetBadgesAsync())?.ToList();

                badgeCollectionView.ReloadData();

                AllQuestions = data;

                CurrentQuestion = QuezControll.Instance.GetCurrentQuestion();

                questionCollectionView.ReloadData();

                answerProgressCollectionView.ReloadData();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                questionCollectionView.Hidden = false;
            }
            EndLoading();

        }

        private void InitCollectionView()
        {
            questionCollectionView.RegisterNibForCell(TestCollectionViewCell.Nib, TestCollectionViewCell.Identifier);
            questionCollectionView.Delegate = this;
            questionCollectionView.DataSource = this;

            badgeCollectionView.RegisterNibForCell(BadgeCell.Nib, BadgeCell.Identifier);
            badgeCollectionView.Delegate = this;
            badgeCollectionView.DataSource = this;

            answerProgressCollectionView.RegisterNibForCell(AnswerProgressCollectionViewCell.Nib, AnswerProgressCollectionViewCell.Identifier);
            answerProgressCollectionView.Delegate = this;
            answerProgressCollectionView.DataSource = this;
        }

        #region CollcetionView
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if(collectionView == answerProgressCollectionView)
            {
                var answerCell = answerProgressCollectionView.DequeueReusableCell(AnswerProgressCollectionViewCell.Identifier, indexPath) as AnswerProgressCollectionViewCell;
              
                var shedulerList = QuezControll.Instance.Sheduler;
                var sheduler = shedulerList?[indexPath.Row];

                //if(currentIndex == sheduler.Position)

                answerCell.InitData(sheduler, hideLeft: indexPath.Row == 0, hideRight: indexPath.Row == shedulerList?.Count()-1);
                return answerCell;
            }

            if(collectionView == badgeCollectionView)
            {
                var badgeCell = badgeCollectionView.DequeueReusableCell(BadgeCell.Identifier, indexPath) as BadgeCell;

                var badge = Badges?[indexPath.Row];

                badgeCell.InitData(badge);

                return badgeCell;
            }

            var cell = questionCollectionView.DequeueReusableCell(TestCollectionViewCell.Identifier, indexPath) as TestCollectionViewCell;

            questionCollectionView.UserInteractionEnabled = true;

            cell.AnswerClicked = async (answer) =>
            {
                //likeAnimation.Play();

                secondesAfterStartTest = 0;
                if (questionCollectionView.UserInteractionEnabled == false)
                    return;

                questionCollectionView.UserInteractionEnabled = false;

                if (answer.IsRight)
                    correctAnswers++;
                else
                    correctAnswers = 0;
                    
                if(correctAnswers == 5)
                {
                    correctAnswers = 0;
                    PlayAnimation();
                    isRibbonAnimation = !isRibbonAnimation;
                }
                if (!IsTotalTime)
                    timeLbl.Text = ($"0{SeparatedMinutes}:{SeparatedSecondes}");

                await Task.Delay(1500);

                try
                {
                    questionCollectionView.Hidden = true;
                    await QuezControll.Instance.AddQuestion(answer.id);
                    CurrentQuestion = QuezControll.Instance.GetCurrentQuestion();
                    currentIndex++;
                    questionCollectionView.ReloadData();
                    questionCollectionView.Hidden = false;
                    if (currentIndex < AllQuestions?.Count)
                    {
                        ScrollAnswerProgressCell();

                        ResetTimer();
                    }

                    else
                    {
                        timer.Stop();

                        await GoToResultPage();
                    }


                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if(currentIndex < 20)
                    questionCollectionView.ScrollToItem(NSIndexPath.FromRowSection(0, 0), UICollectionViewScrollPosition.Top, true);
            };

            cell.InitData(CurrentQuestion);

            cell.ImageClicked = (image) =>
            {
                var ImageVc = Storyboard.InstantiateViewController(QuestionImageViewController.StoryboardId) as QuestionImageViewController;

                ImageVc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

                ImageVc.ImageUrl = image;

                this.NavigationController.PresentViewController(ImageVc, true, null);
            };
            return cell;
        }

        private void ScrollAnswerProgressCell()
        {
            if (currentIndex >= lastVisibleIndex)
            {
                answerProgressCollectionView.ScrollToItem(NSIndexPath.FromRowSection(currentIndex + 1, 0), UICollectionViewScrollPosition.CenteredHorizontally, true);
                lastVisibleIndex++;
            }

            else
            {
                answerProgressCollectionView.ScrollToItem(NSIndexPath.FromRowSection(currentIndex, 0), UICollectionViewScrollPosition.CenteredHorizontally, true);
                lastVisibleIndex++;
            }

            //questionCollectionView.ReloadData();
            answerProgressCollectionView.ReloadData();
        }

        private async Task GoToResultPage()
        {
            timer.Stop();
            timer.Dispose();
            var pausedTime = progressLayer.ConvertTimeToLayer(CAAnimation.CurrentMediaTime(), null);
            progressLayer.Speed = 0f;
            progressLayer.TimeOffset = pausedTime;

            await Task.Delay(500);

            InvokeOnMainThread(async () => {
                questionCollectionView.Hidden = true;

                ShowLoading();

                var info = (await QuezControll.Instance.GetExamInfoAsync());

                var questionList = info.QuestionResult?.ToList();

                EndLoading();

                var navVc = this.NavigationController;
                this.NavigationController.PopToRootViewController(false);
                var resultTab = Storyboard.InstantiateViewController(ResultTabbedViewController.StoryboardId) as ResultTabbedViewController;

                resultTab.AfterExam = true;
                resultTab.IsexamType = quezCategory == QuezCategory.QuezTest;

                resultTab.Questions = questionList;

                resultTab.QuisInfo = info;

                navVc.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);
                navVc.PushViewController(resultTab, true);
            });
        }

        [Export("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            if (collectionView != questionCollectionView)
                return null;

            var headerView = collectionView.DequeueReusableSupplementaryView(elementKind, new NSString("FooterReusableView"), indexPath) as FooterTestView;

            headerView.UserInteractionEnabled = true;

            headerView.SkipClicked = async () => {

                try
                {
                    if (headerView.UserInteractionEnabled == false)
                        return;
                    headerView.UserInteractionEnabled = false;

                    var testCell = questionCollectionView.CellForItem(indexPath) as TestCollectionViewCell;

                    testCell.SkipQuestion();

                    await Task.Delay(500);

                    if (currentIndex >= AllQuestions?.Count - 1)
                    {
                        currentIndex++;
                        await SkipQuestion();
                        await GoToResultPage();
                    }
                    else
                    {
                        if (!IsTotalTime)
                            timeLbl.Text = ($"01:30");
                        await SkipQuestion();
                        ScrollAnswerProgressCell();
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };

            return headerView;
        }

        [Export("collectionView:layout:referenceSizeForFooterInSection:")]
        public CGSize GetReferenceSizeForFooter(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            if(collectionView !=questionCollectionView)
                return new CGSize(0,0);

            //nfloat delta = collectionView.Frame.Height - (totalHeight + 150);
            return new CGSize(0, 160 );

        }
        #endregion

        private void ResetTimer()
        {
            if (!IsTotalTime)
            {
                timer.Dispose();
                InitTotalTimer(SeparatedMinutes, SeparatedSecondes);
            }

            if (!IsTotalTime)
            {
                InitCircular(90, 0);
            }
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if(collectionView == answerProgressCollectionView)
                return AllQuestions?.Count?? 0;

            if (collectionView == badgeCollectionView)
                return Badges?.Count ?? 0;
            return CurrentQuestion == null ? 0 : 1;
        }

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {

            if (collectionView == answerProgressCollectionView)
                return new CoreGraphics.CGSize(25, 25);

            if (collectionView == badgeCollectionView)
                return new CGSize(30, 40);

            if(currentIndex < AllQuestions?.Count && CurrentQuestion != null)
                SetCellHeight(CurrentQuestion);
                

            nfloat delta = collectionView.Frame.Height - (totalHeight + 150);


            return new CoreGraphics.CGSize(collectionView.Frame.Width, totalHeight + (delta>0? delta:0));
        }

        void SetCellHeight(IQuestion question)
        {
            totalHeight = 0;
            answersHeight = 0;

            var data = question;
            var text = data?.title;
            var titleHeight = text.GetStringHeight((float)questionCollectionView.Frame.Width, 50, 17);
            //var commentHeight = data?.Description?.GetStringHeight((float)questionCollectionView.Frame.Width, 50, 14);

            var ImagesCount = data?.images?.Count();

            if (ImagesCount == 0)
                imagesHeight = 0;
                //Debug.WriteLine($"ImagesCount : {ImagesCount}");
            else
                imagesHeight = 180;

            foreach (var item in data?.Answers)
            {
                var height = item.title.GetStringHeight((float)questionCollectionView.Frame.Width - 60, 64, 15);
                answersHeight += height + 40;
            }

            totalHeight = titleHeight + imagesHeight + answersHeight + 60 ;
        }

        private int totalMinutes;
        private int totalSecondes;

        bool AlredyGoToResult = false;

        private void InitTotalTimer(int _minutes, int _secondes)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;

            var minutes = _minutes;
            var secondes = _secondes;

            string Stringminutes;
            string Stringsecondes;

            timer.Elapsed += async (sender, e) => {

                if (secondes == 0)
                {
                    if (minutes <= 0 && secondes <= 0)
                    {
                        timer.Enabled = false;
                        timer.Stop();
                        timer.Dispose();
                        if(IsTotalTime)
                        {
                            for (int i = currentIndex; i < AllQuestions?.Count; i++)
                            {
                                await SkipQuestion();
                            }

                            if (AlredyGoToResult)
                                return;
                            AlredyGoToResult = true;
                            await GoToResultPage();
                        }
                        else
                        {
                            await SkipQuestion();
                            if (currentIndex >= AllQuestions?.Count)
                            {
                                if (AlredyGoToResult)
                                    return;
                                AlredyGoToResult = true;
                                await GoToResultPage();
                            }
                                
                        }

                        return;
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

                totalMinutes = minutes;
                totalSecondes = secondes;

                RestSecondes = TimeSpan.FromSeconds((60 * minutes) + (secondes));
            };

            timer.Start();
        }

        TimeSpan RestSecondes;

        private async Task SkipQuestion()
        {

            secondesAfterStartTest = 0;
            correctAnswers = 0;
            InvokeOnMainThread(() => questionCollectionView.Hidden = true);

            if (currentIndex <= AllQuestions?.Count)
            {
                await QuezControll.Instance.AddQuestion();

                if (currentIndex <= AllQuestions?.Count - 2)
                    CurrentQuestion = QuezControll.Instance.GetCurrentQuestion();
            }

            currentIndex++;
            InvokeOnMainThread(() =>
            {

                questionCollectionView.ReloadData();
                //questionCollectionView.ScrollToItem(NSIndexPath.FromRowSection(0, 0), UICollectionViewScrollPosition.Top, false);
                answerProgressCollectionView.ReloadData();
                    if(currentIndex<AllQuestions.Count)
                      ResetTimer();

                questionCollectionView.Hidden = false;
            });

        }

        private void ShowLoginAlert()
        {
            var alert = UIAlertController.Create("ყურადღება", "დრო ამოიწურა.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void InitCircular(double duration, float strokeAnimationFrom)
        {
            #region CircularAnimation
             progressLayer = new CAShapeLayer();
            var trackLayer = new CAShapeLayer();
            //trackColor
            var trackColor = AppColors.TitleColor;
            var progressColor = UIColor.Clear.FromHexString("EDEDED");

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

            strokeAnimation = CABasicAnimation.FromKeyPath("strokeEnd");
            strokeAnimation.Duration = duration;

            strokeAnimation.From = NSObject.FromObject(strokeAnimationFrom);
            strokeAnimation.To = NSObject.FromObject(1.0f);

            strokeAnimation.TimingFunction = CAMediaTimingFunction.FromName(new NSString(CAMediaTimingFunction.Linear.ToString()));
            progressLayer.StrokeEnd = 0.0f;
            progressLayer.AddAnimation(strokeAnimation, "animateCircle");


            #endregion
        }

        private void InitLottie(string filePath)
        {
            likeAnimation = LOTAnimationView.AnimationWithFilePath(filePath);
            likeAnimation.ContentMode = UIViewContentMode.ScaleAspectFit;
            viewForAnimation.Frame = new CGRect(questionCollectionView.Frame.X, questionCollectionView.Frame.Y, 0, 0);
            likeAnimation.UserInteractionEnabled = false;
            likeAnimation.Frame = new CoreGraphics.CGRect(0, 0, viewForAnimation.Frame.Width, viewForAnimation.Frame.Height);
            viewForAnimation.AddSubview(likeAnimation);
        }

        private void PlayAnimation()
        {
            AnimationFilePath = isRibbonAnimation ? "exploding-ribbon.json" : "like_animation.json";

            likeAnimation.Hidden = false;
            likeAnimation.RemoveFromSuperview();

            InitLottie(AnimationFilePath);

            viewForAnimation.Frame = new CGRect(questionCollectionView.Frame.X, questionCollectionView.Frame.Y, 
                questionCollectionView.Bounds.Width, questionCollectionView.Bounds.Height);

            likeAnimation.Frame = new CoreGraphics.CGRect(0, 0, viewForAnimation.Frame.Width, viewForAnimation.Frame.Height);

            likeAnimation.PlayWithCompletion((animationFinished) =>
            {
                viewForAnimation.Frame = new CGRect(questionCollectionView.Frame.X, questionCollectionView.Frame.Y, 0, 0);
                likeAnimation.Hidden = true;
            });
        }

        private async Task UpdateTimerAndCircular(TimeSpan diff)
        {
            var _restSecondes = 90 - (secondesAfterStartTest + diff.TotalSeconds);

            if(TimeSpan.FromSeconds(_restSecondes).TotalSeconds > TimeSpan.FromMinutes(30).TotalSeconds)
            {
                //TODO finish test
            }
            else
            {
                if(IsTotalTime)
                {
                    _restSecondes = 1800 - (secondesAfterStartTest + diff.TotalSeconds);
                    var from = (float)((float)(TimeSpan.FromSeconds(_restSecondes).TotalSeconds)/ 1800);

                    InitCircular(TimeSpan.FromSeconds(_restSecondes).TotalSeconds, 1-from);

                    var minutes = (int)(_restSecondes) / 60;
                    var seconde = (int)(_restSecondes) % 60;

                    timer.Stop();
                    timer.Dispose();

                    InitTotalTimer(minutes, seconde);

                }
                else
                {
                    if (_restSecondes > 0)
                    {
                        UpdateTimerAndCircular(_restSecondes, false);
                    }
                    else
                    {
                        var count = (int)(diff.TotalSeconds / 90);
                        var seconds = diff.TotalSeconds % 90;

                        for (int i = 0; i < count; i++)
                        {
                            await SkipQuestion();
                        }

                        UpdateTimerAndCircular(90-seconds, false);
                    }
                }
            }
        }

        private void UpdateTimerAndCircular(double _restSecondes, bool isSkipped)
        {
            var from = (float)(_restSecondes / 90.0f);

            InitCircular(90, isSkipped ? from : 1.0f - from);

            var minutes = (int)(_restSecondes) / 60;
            var seconde = (int)(_restSecondes) % 60;

            timer.Stop();
            timer.Dispose();
            if(isSkipped)
                InitTotalTimer(1, seconde);
            else
                InitTotalTimer(minutes, seconde);
        }

        private NSObject _willResignActiveNotificationObserver;
        private NSObject _didBecomeActiveNotificationObserver;

        DateTime ResignActiveTime;
        DateTime BecomeActiveTime;

        int secondesAfterStartTest = 0;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _willResignActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, (obj) =>
            {
                ResignActiveTime = DateTime.Now;

                secondesAfterStartTest = IsTotalTime? (int)(1800 - RestSecondes.TotalSeconds) : (int)(90 - (int)RestSecondes.TotalSeconds);
            });

            _didBecomeActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, async (obj) => 
            {
                BecomeActiveTime = DateTime.Now;

                var diff = (BecomeActiveTime - ResignActiveTime).TotalSeconds;

                if(IsTotalTime)
                {
                    if (diff > RestSecondes.TotalSeconds)
                    {
                        timer.Enabled = false;
                        timer.Stop();
                        timer.Dispose();
                        for (int i = currentIndex; i < AllQuestions?.Count; i++)
                        {
                            await SkipQuestion();
                        }
                        if (AlredyGoToResult)
                            return;
                        AlredyGoToResult = true;
                        await GoToResultPage();
                        return;
                    }
                    await UpdateTimerAndCircular(TimeSpan.FromSeconds(diff));
                }
                else
                {
                    if (currentIndex == AllQuestions?.Count - 1)
                    {
                        if (diff > RestSecondes.TotalSeconds)
                        {
                            timer.Enabled = false;
                            timer.Stop();
                            timer.Dispose();

                            await SkipQuestion();

                            if (AlredyGoToResult)
                                return;
                            AlredyGoToResult = true;
                            await GoToResultPage();
                            return;
                        }
                        else
                            await UpdateTimerAndCircular(TimeSpan.FromSeconds(diff));
                    }
                    else
                    {
                        if(diff > RestSecondes.TotalSeconds)
                        {
                            await SkipQuestion();
                            await UpdateTimerAndCircular(TimeSpan.FromSeconds(diff - RestSecondes.TotalSeconds));
                            return;
                        }
                        await UpdateTimerAndCircular(TimeSpan.FromSeconds(diff));
                    }
                }
            });
        }

    }
}
