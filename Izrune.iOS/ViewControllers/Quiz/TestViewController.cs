// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MPDC.iOS.Utils;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //await LoadDataAsync();

            skipQuestionBtn.Layer.CornerRadius = 20;

            skipQuestionBtn.TouchUpInside += delegate
            {

                Questions.Clear();
                MoveToQuestions();
                questionCollectionView.ReloadData();
            };

            InitCollectionView();
            MoveToQuestions();

            questionCollectionView.ReloadData();
        }

        private void MoveToQuestions()
        {
            if(AllQuestions.Count != 0)
            {
                Questions.Add(AllQuestions?[0]);
                AllQuestions.RemoveAt(0);
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
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = questionCollectionView.DequeueReusableCell(TestCollectionViewCell.Identifier, indexPath) as TestCollectionViewCell;

            var data = Questions?[0];

            cell.imagesCollectioHeight = imagesHeight;
            cell.answersCollectioHeight = answersHeight + 80;

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

            SetCellHeight(Questions[0]);

            return new CoreGraphics.CGSize(collectionView.Frame.Width, totalHeight + 60);
        }

        void SetCellHeight(IQuestion question)
        {
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
                //answersHeight += item.title.GetStringHeight((float)questionCollectionView.Frame.Width, 77, 15);

                var height = item.title.GetStringHeight((float)questionCollectionView.Frame.Width, 64, 15);

                answersHeight += height > 40 ? height + 30 : height;
            }

            totalHeight = titleHeight + imagesHeight + answersHeight + spaceSumBetweenAnswers + 50;

            //return totalHeight;
        }
    }
}
