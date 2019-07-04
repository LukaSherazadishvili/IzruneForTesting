// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.CollectionViewCells;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using IZrune.PCL.Implementation.Models;
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
        private IStatisticServices diplomeService;
        private List<IStudentsStatistic> StudentsStatistics;
        DropDown YearDropDown = new DropDown();

        private ExamResultViewController diplomeDetailVc;
        private List<IDiplomStatistic> diplomeYears;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            InitUI();

            await LoadDataAsync();

            InitCollectionViewSettings();

            backBtn.TouchUpInside += delegate {
                this.NavigationController.PopViewController(true);

            };

            InitDropDowns();
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            await UpdateData();
            diplomeCollectionView.ReloadData();
        }
        private void InitUI()
        {
            viewForDropDown.Layer.CornerRadius = 20;
        }

        private async Task LoadDataAsync()
        {
            HideHeader(true);
            ShowLoading();

            var service = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();
            diplomeYears = (await service.GetDiplomaStatisticAsync())?.ToList();

            await UpdateData();

            if (StudentsStatistics == null || StudentsStatistics?.Count == 0)
            {
                HideHeader(true);
                ShowAlert();
            }
            else
                HideHeader(false);
            EndLoading();
            diplomeCollectionView.ReloadData();
        }

        public async Task UpdateData()
        {
            diplomeService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

            StudentsStatistics = (await diplomeService.GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam))?.Where(x => x.DiplomaUrl != null)?.ToList();
        }

        private void HideHeader(bool hide)
        {
            headerImageView.Hidden = hide;
            headerLbl.Hidden = hide;
            viewForDropDown.Hidden = hide;
        }

        private void ShowAlert()
        {
            var alert = UIAlertController.Create("შეცდომა", "ინფორმაცია ვერ მოიძებნა.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, (s) => { this.NavigationController.PopViewController(true); }));
            this.PresentViewController(alert, true, null );
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return StudentsStatistics?.Count ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = diplomeCollectionView.DequeueReusableCell(DiplomeCollectionViewCell.Identifier, indexPath) as DiplomeCollectionViewCell;

            var data = StudentsStatistics?[indexPath.Row];

            cell.CellClicked = async (studentStatistic) =>
            {
                ShowLoading();

                diplomeDetailVc = Storyboard.InstantiateViewController(ExamResultViewController.StoryboardId) as ExamResultViewController;
                diplomeDetailVc.Student = Student;
                diplomeDetailVc.ShowShare = true;

                try
                {
                    var quisInfo = await UserControl.Instance.GetQuisInfo(studentStatistic.Id);
                    diplomeDetailVc.QuisInfo = quisInfo;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    EndLoading();
                }

                this.NavigationController.PushViewController(diplomeDetailVc, true);
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

        #region DropDown
        private void InitDropDowns()
        {
            SetupDropDown(YearDropDown, viewForDropDown, diplomeLbl);
            SetupDropDownGesture(YearDropDown, viewForDropDown);

            var studentsArray = diplomeYears?.Select(x => x.DiplomaDate)?.ToArray();
            YearDropDown.DataSource = studentsArray;

            YearDropDown.SelectionAction = (nint index, string name) =>
            {
                try
                {
                    diplomeLbl.Text = name + "სასწავლო წელი";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            };

        }

        private void SetupDropDown(DropDown dropDown, UIView viewForDpD, UILabel dropDownLbl)
        {
            dropDown.AnchorView = new WeakReference<UIView>(viewForDpD);
            dropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDpD.Bounds.Height);
            dropDown.Width = View.Frame.Width;
            dropDown.Direction = Direction.Bottom;
        }

        private void InitDropDownUI(DropDown dropDown)
        {
            dropDown.BackgroundColor = UIColor.FromRGB(243, 243, 243);
            dropDown.SelectionBackgroundColor = AppColors.TitleColor;
            DPDConstants.UI.TextColor = AppColors.TitleColor;
            DPDConstants.UI.SelectedTextColor = UIColor.White;

            dropDown.TextFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 15);
            dropDown.ClipsToBounds = true;
            dropDown.Layer.CornerRadius = 20;
        }

        private void SetupDropDownGesture(DropDown dropDown, UIView viewforDpD)
        {
            if (viewforDpD.GestureRecognizers == null || viewforDpD.GestureRecognizers?.Length == 0)
            {
                viewforDpD.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    dropDown.Show();
                    InitDropDownUI(dropDown);
                }));
            }

        }
        #endregion
    }
}
