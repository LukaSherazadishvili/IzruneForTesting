// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using UIKit;
using System.Linq;
using XLPagerTabStrip;
using System.Collections.Generic;
using Airbnb.Lottie;
using Plugin.SimpleAudioPlayer;
using Izrune.iOS.CollectionViewCells;
using System.Threading.Tasks;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using IZrune.PCL.Helpers;

namespace Izrune.iOS
{
	public partial class ExamResultViewController : UIViewController, IIndicatorInfoProvider, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout

    {
		public ExamResultViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ExamResultStoryboardId");

        public IQuisInfo QuisInfo;

        public IStudent Student;

        public bool ShowShare;

        public bool AfterExam;

        public bool IsExam;

        LOTAnimationView fireworksAnimation = new LOTAnimationView();

        Dictionary<int, string> ExamResultVoices = new Dictionary<int, string>()
        {
            {1, "სავარჯიშო 1 ვარსკვლავი.mp3"},
            {2, "სავარჯიშო 2 ვარსკვლავი.mp3"},
            {3, "სავარჯიშო 3 ვარსკვლავი.mp3"},
            {4, "სავარჯიშო 4 ვარსკვლავი.mp3"},
            {5, "სავარჯიშო 5 ვარსკვლავი.mp3"}
        };

        Dictionary<int, string> SummResultVoices = new Dictionary<int, string>()
        {
            {1, "შემაჯამებელი 1 ვარსკვლავი.mp3"},
            {2, "შემაჯამებელი 2 ვარსკვლავი.mp3"},
            {3, "შემაჯამებელი 3 ვარსკვლავი.mp3"},
            {4, "შემაჯამებელი 4 ვარსკვლავი.mp3"},
            {5, "შემაჯამებელი 5 ვარსკვლავი.mp3"}
        };

        List<IBadges> Badges;

        ISimpleAudioPlayer player;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();

            InitCollectionView();

            await LoadDataAsync();
            InitResult();

            //if(ShowShare)
            //{
            //    if(!string.IsNullOrEmpty(QuisInfo.DiplomaURl))
            //    {
            //        var barButton = new UIBarButtonItem(UIBarButtonSystemItem.Action, null);

            //        barButton.Clicked += delegate {
            //            var url = QuisInfo.DiplomaURl;

            //            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
            //            {
            //                this.ShareUrl(url);
            //            }
            //            else
            //            {
            //                barButton.Image = null;
            //                return;
            //            }
            //        };
            //        this.NavigationItem.RightBarButtonItem = barButton;
            //    }
            //}

            if (AfterExam)
            {
                var voiceUrl = string.Empty;
                var starCount = QuisInfo.QueisResult.Stars;

                if(starCount > 0)
                {
                    if (!IsExam)
                    {
                        ExamResultVoices.TryGetValue(starCount, out voiceUrl);
                    }
                    else
                        SummResultVoices.TryGetValue(starCount, out voiceUrl);

                    player = CrossSimpleAudioPlayer.Current;
                    player.Load(voiceUrl);
                    player.Play();

                }

                if(starCount == 5)
                {
                    InitLottie();
                    fireworksAnimation.ContentMode = UIViewContentMode.ScaleToFill;
                    fireworksAnimation.AnimationSpeed = 0.5f;

                    fireworksAnimation.Play();
                }
            }

            if (!string.IsNullOrEmpty(QuisInfo?.EgmuUrl) && !string.IsNullOrWhiteSpace(QuisInfo?.EgmuUrl))
            {
                if (egmuImageView?.GestureRecognizers == null || egmuImageView?.GestureRecognizers?.Length == 0)
                {
                    egmuImageView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                    {
                        if (UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString(QuisInfo?.EgmuUrl)))
                            UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(QuisInfo?.EgmuUrl));
                    }));
                }
            }
            else
                egmuImageView.Hidden = true;
        }

        private void InitCollectionView()
        {
            badgesCollectionView.RegisterNibForCell(BadgeCell.Nib, BadgeCell.Identifier);
            badgesCollectionView.Delegate = this;
            badgesCollectionView.DataSource = this;
        }

        private async Task LoadDataAsync()
        {
            //ShowLoading();

            var service = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();

            Badges = (await service.GetBadgesAsync())?.ToList();

            badgesCollectionView.ReloadData();

            Student = UserControl.Instance.CurrentStudent;
            //EndLoading();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if(AfterExam)
                player.Stop();
        }

        private void InitUI()
        {
            pointView.AddShadowToView(4, 46, 0.8f, UIColor.FromRGBA(0, 0, 0, 0.35f));
            timeShadowView.AddShadowToView(4, 46f, 0.8f, UIColor.FromRGBA(0, 0, 0, 0.35f));
        }

        private void InitResult()
        {

            if (!string.IsNullOrEmpty(QuisInfo?.DiplomaURl) && !string.IsNullOrWhiteSpace(QuisInfo.DiplomaURl))
                diplomeImageView.InitImageFromWeb(QuisInfo?.DiplomaURl, false, false);
            else
                diplomeImageView.Hidden = true;

            var ratingImages = ratingStackView.Subviews.Select(x => x as UIImageView);

            for (int i = 0; i < QuisInfo.QueisResult.Stars; i++)
            {
                ratingImages.ElementAt(i).Image = UIImage.FromBundle("1 – 24.png");
            }

            userNameLbl.Text = Student?.Name + " " + Student?.LastName;
            resultQualityLbl.Text = QuisInfo.QueisResult.text_title;
            resultTextLbl.Text = QuisInfo.QueisResult.text_description;

            totalPointLbl.Text = QuisInfo.QueisResult.Score;
            examTimeLbl.Text = FormatTimeSpan(TimeSpan.FromSeconds(QuisInfo.QueisResult.Duration));//$"{QuisInfo.QueisResult.Duration / 60}:{QuisInfo.QueisResult.Duration % 60}";

            finalResultLbl.Text = $"სწორი პასუხი {QuisInfo.QueisResult.RightAnswer}, არასწორი პასუხი {QuisInfo.QueisResult.WronAnswers} გამოტოვებული კითხვა {QuisInfo.QueisResult.SkipedAnswers}";
        }

        public IndicatorInfo IndicatorInfoForPagerTabStrip(PagerTabStripViewController pagerTabStripController)
        {
            return new IndicatorInfo("შედეგი");
        }

        private string FormatTimeSpan(TimeSpan time)
        {
            return ((time < TimeSpan.Zero) ? "-" : "") + time.ToString(@"mm\:ss");
        }

        private void InitLottie()
        {
            fireworksAnimation = LOTAnimationView.AnimationWithFilePath("fireWorksAnimation.json");
            fireworksAnimation.ContentMode = UIViewContentMode.ScaleAspectFill;
            fireworksAnimation.Frame = new CoreGraphics.CGRect(0, 0, viewForLottie.Frame.Width, viewForLottie.Frame.Height);
            fireworksAnimation.UserInteractionEnabled = true;
            viewForLottie.AddSubview(fireworksAnimation);
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return Badges?.Count ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = badgesCollectionView.DequeueReusableCell(BadgeCell.Identifier, indexPath) as BadgeCell;

            var data = Badges?[indexPath.Row];

            cell.InitData(data);

            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CoreGraphics.CGSize(30, 40);
        }
    }
}
