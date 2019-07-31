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
using MPDC.iOS.Utils;
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
        private List<IQuisInfo> StudentsStatistics;
        private List<IDiplomStatistic> AllStatistic = new List<IDiplomStatistic>();

        DropDown YearDropDown = new DropDown();

        private ResultTabbedViewController diplomeDetailVc;
        private List<IDiplomStatistic> diplomeYears;

        public bool ShouldLoadData { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            InitUI();

            //await LoadDataAsync();

            InitCollectionViewSettings();

            backBtn.TouchUpInside += delegate {
                this.NavigationController.PopViewController(true);

            };
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                if (ShouldLoadData)
                {
                    await LoadDataAsync();

                    InitDropDowns();

                    diplomeCollectionView.Hidden = false;
                    diplomeLbl.Text = diplomeYears?[0]?.DiplomaDate + " სასწავლო წელი";

                    ShouldLoadData = false;
                }
            }
            else
                this.ShowConnectionAlert();
        }
        private void InitUI()
        {
            viewForDropDown.Layer.CornerRadius = 20;
        }

        private async Task LoadDataAsync()
        {
            HideHeader(true);
            ShowLoading();

            await UpdateData();

            var diplomes = diplomeYears?.FirstOrDefault();
            StudentsStatistics = diplomes?.DiplomaStatistic?.ToList();

            AllStatistic = diplomeYears;
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
            HideHeader(true);
            diplomeCollectionView.Hidden = true;

            diplomeService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

            diplomeYears = (await diplomeService.GetDiplomaStatisticAsync())?.ToList();

            HideHeader(false);
            diplomeCollectionView.Hidden = false;
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

            cell.CellClicked = (studentStatistic) =>
            {
                ShowLoading();

                diplomeDetailVc = Storyboard.InstantiateViewController(ResultTabbedViewController.StoryboardId) as ResultTabbedViewController;
                diplomeDetailVc.Student = Student;

                //diplomeDetailVc.ShowShare = true;

                try
                {
                    //var quisInfo = await UserControl.Instance.GetQuisInfo(studentStatistic.Id);
                    diplomeDetailVc.QuisInfo = studentStatistic;
                    diplomeDetailVc.Questions = studentStatistic?.QuestionResult?.ToList();
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
                    diplomeLbl.Text = name + " სასწავლო წელი";

                    var years = diplomeYears?[(int)index];

                    StudentsStatistics = diplomeYears?[(int)index].DiplomaStatistic?.ToList();

                    diplomeCollectionView.ReloadData();
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
