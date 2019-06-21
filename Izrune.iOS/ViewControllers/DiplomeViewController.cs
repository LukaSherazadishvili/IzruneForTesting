// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using UIKit;

namespace Izrune.iOS
{
	public partial class DiplomeViewController : BaseViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public DiplomeViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("DiplomeStoryboardId");

        public IStudent Student;

        private List<IStudentsStatistic> StudentsStatistics;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();

            await LoadDataAsync();

            InitCollectionViewSettings();

        }

        private void InitUI()
        {
            viewForDropDown.Layer.CornerRadius = 20;
        }

        public async Task LoadDataAsync()
        {

            ShowLoading();

            var diplomeService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

            StudentsStatistics = (await diplomeService.GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam))?.Where(x => x.DiplomaUrl != null)?.ToList();

            EndLoading();
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return StudentsStatistics?.Count ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = diplomeCollectionView.DequeueReusableCell(DiplomeCollectionViewCell.Identifier, indexPath) as DiplomeCollectionViewCell;

            var data = StudentsStatistics?[indexPath.Row];

            cell.CellClicked = () =>
            {
                //TODO
            };

            cell.InitData(data);

            return cell;
        }

        private void InitCollectionViewSettings()
        {
            diplomeCollectionView.RegisterNibForCell(DiplomeCollectionViewCell.Nib, DiplomeCollectionViewCell.Identifier);

            diplomeCollectionView.Delegate = this;
            diplomeCollectionView.DataSource = this;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CoreGraphics.CGSize(collectionView.Frame.Width * 0.45, 50);
        }
    }
}
