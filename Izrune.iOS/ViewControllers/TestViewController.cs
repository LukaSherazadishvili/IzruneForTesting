// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cavea.iOS.Utils;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using UIKit;

namespace Izrune.iOS
{
	public partial class TestViewController : UIViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public TestViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("TestViewControllerStoryboardId");

        List<IQuestion> Questions;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            await LoadDataAsync();

            skipQuestionBtn.Layer.CornerRadius = 20;

            skipQuestionBtn.TouchUpInside += delegate {

                //var indexPath = questionCollectionView.IndexPathsForVisibleItems[0];

                //var currIndex = indexPath.Row;

                //if(currIndex < 6)
                //{
                //    questionCollectionView.ScrollToItem(NSIndexPath.FromItemSection(currIndex+1, 0), UICollectionViewScrollPosition.Right, true);
                //};

                questionCollectionView.ReloadData();
            };

            InitCollectionView();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var testService = ServiceContainer.ServiceContainer.Instance.Get<IQuezServices>();

                var userService = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();

                var user = await userService.GetUserAsync();

                var data = (await testService.GetQuestionsAsync(user.id, IZrune.PCL.Enum.QuezCategory.QuezExam))?.ToList();

                Questions = data;

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
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = questionCollectionView.DequeueReusableCell(TestCollectionViewCell.Identifier, indexPath) as TestCollectionViewCell;

            var data = Questions?[indexPath.Row];

            cell.InitData(data);
            return cell;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return 1;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {

            //TODO Calculate CellHeight

            var data = Questions?[indexPath.Row];

            var appFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 17);

            var titleHeight = (float)data.title.GetSizeByText(appFont).Height;

            var ImagesCount = data?.images?.Count();

            float imagesHeight = 0;

            if (ImagesCount == 0)
                imagesHeight = 0;
            else if (ImagesCount > 0 || ImagesCount <= 2)
                imagesHeight = 90;
            else
                imagesHeight = 180;

            var spaceSumBetweenAnswers = 80;

            float answersHeight = 0;
            foreach (var item in data?.Answers)
            {
                answersHeight += (float)item?.title.GetSizeByText(appFont).Height;
            }



            return new CoreGraphics.CGSize(collectionView.Frame.Width, collectionView.Frame.Height * 0.5);
        }
    }
}
