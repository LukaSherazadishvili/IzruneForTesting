// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using Izrune.iOS.CollectionViewCells;
using MPDCiOSPages.ViewControllers;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using System.Threading.Tasks;
using IZrune.PCL.Abstraction.Services;
using System.Linq;
using System.Collections.Generic;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using XLPagerTabStrip;
using System.Globalization;

namespace Izrune.iOS
{
	public partial class TestResultsViewController : BaseViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout, IIndicatorInfoProvider
    {
		public TestResultsViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("TestResultsStoryboardId");

        DropDown YearDropDown = new DropDown();
        DropDown MonthDropDown = new DropDown();

        private List<IStudentsStatistic> StudentsStatistics;

        private List<IStudentsStatistic> OriginalList = new List<IStudentsStatistic>();

        public bool Hideheader = true;
        private CultureInfo cultureInfo;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitCollectionViewSettings();

            await LoadDataAsync();

            InitGestures();

            HideHeader(Hideheader);

            resultCollectionView.ReloadData();

            InitHeader();

            mainStackView.Hidden = Hideheader;

        }

        private void InitUI()
        {
            headerView.Layer.CornerRadius = 15;
            viewForShadow.AddShadowToView(3, 15, 0.2f, UIColor.FromRGBA(0, 0, 0, 38.5f));

            var dates = dateStackView.Subviews.ToList();
            var results = testResultStackView.Subviews.ToList();

            for (int i = 0; i < dates.Count; i++)
            {
                dates[i].Layer.CornerRadius = 17;
                results[i].Layer.CornerRadius = 17;
            }
        }

        private void HideAll(bool hide)
        {
            contentView.Hidden = hide;
        }

        public async Task LoadDataAsync()
        {

            HideAll(true);
            ShowLoading();

            var diplomeService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

            StudentsStatistics = (await diplomeService.GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest))?.ToList();

            foreach (var item in StudentsStatistics)
            {
                OriginalList.Add(item);
            }

            EndLoading();
            if (StudentsStatistics == null || StudentsStatistics?.Count == 0)
            {
                HideAll(true);
                ShowAlert();
            }

            else
            {
                InitUI();
                InitDroDown();
                HideAll(false);
            }

            //var firstCell = resultCollectionView.DequeueReusableCell(ResultCollectionViewCell.Identifier, NSIndexPath.FromRowSection(0, 0)) as ResultCollectionViewCell;
            //var lastCell = resultCollectionView.DequeueReusableCell(ResultCollectionViewCell.Identifier, NSIndexPath.FromRowSection((System.nint)(StudentsStatistics?.Count - 1), 0)) as ResultCollectionViewCell;
            //var contentHeight = lastCell.Frame.Y + lastCell.Frame.Height - firstCell.Frame.Y;

            UpdateCollectionViewHeight();
            resultCollectionView.ReloadData();
        }

        private void UpdateCollectionViewHeight()
        {
            var totalHeight = (System.nfloat)(305 + ((StudentsStatistics?.Count - 1) * 220));
            resultCollectionViewHeightConstraint.Constant = totalHeight;
        }

        private void InitCollectionViewSettings()
        {
            resultCollectionView.RegisterNibForCell(ResultCollectionViewCell.Nib, ResultCollectionViewCell.Identifier);
            resultCollectionView.Delegate = this;
            resultCollectionView.DataSource = this;
        }

        public void HideHeader(bool hide)
        {
            viewForShadow.Hidden = hide;
            resultStackView.Hidden = hide;
            headerView.Hidden = hide;
            headerLine.Hidden = hide;
            titleLbl.Hidden = !hide;
        }

        #region CollectionView

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return StudentsStatistics?.Count ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var resultCell = resultCollectionView.DequeueReusableCell(ResultCollectionViewCell.Identifier, indexPath) as ResultCollectionViewCell;

            var data = StudentsStatistics?[indexPath.Row];

            resultCell.InitData(data);

            return resultCell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CoreGraphics.CGSize(collectionView.Frame.Width, 210);
        }

        #endregion

        private void InitDroDown()
        {
            YearDropDown.AnchorView = new WeakReference<UIView>(yearDropdownView);
            YearDropDown.BottomOffset = new CoreGraphics.CGPoint(0, yearDropdownView.Bounds.Height);
            YearDropDown.Width = this.View.Frame.Width;
            YearDropDown.Direction = Direction.Bottom;

            var minYear = StudentsStatistics?.Min(x => x.ExamDate);
            var maxYear = StudentsStatistics?.Max(x => x.ExamDate);

            var yearArray = GetYears(minYear, maxYear);

            YearDropDown.DataSource = yearArray;

            YearDropDown.SelectionAction = (nint index, string name) =>
            {
                ShowLoading();
                yearLbl.Text = name;

                if(index == 0)
                {
                    StudentsStatistics = OriginalList;
                    resultCollectionView.ReloadData();

                    InitHeader();
                    return;
                }

                StudentsStatistics = OriginalList?.Where(x => x.ExamDate.Year == Convert.ToInt32(name))?.ToList();
                UpdateCollectionViewHeight();
                resultCollectionView.ReloadData();
                EndLoading();

                if (StudentsStatistics == null || StudentsStatistics?.Count == 0)
                {
                    ShowAlert();
                }
            };

            MonthDropDown.AnchorView = new WeakReference<UIView>(monthDropdownView);
            MonthDropDown.BottomOffset = new CoreGraphics.CGPoint(0, monthDropdownView.Bounds.Height);
            MonthDropDown.Width = this.View.Frame.Width;
            MonthDropDown.Direction = Direction.Bottom;

            var monthArray = GetMonth();

            MonthDropDown.DataSource = monthArray;
            MonthDropDown.SelectionAction = (nint index, string name) =>
            {
                monthLbl.Text = name;

                ShowLoading();
                if(index == 0)
                {
                    StudentsStatistics = OriginalList;
                    resultCollectionView.ReloadData();
                    return;
                }

                StudentsStatistics = OriginalList?.Where(x => x.ExamDate.Month == index)?.ToList();
                UpdateCollectionViewHeight();
                resultCollectionView.ReloadData();

                EndLoading();
                if(StudentsStatistics == null || StudentsStatistics?.Count == 0)
                {
                    ShowAlert();
                }
            };
        }

        private void ShowAlert()
        { 
            var alert = UIAlertController.Create("შეცდომა", "ინფორმაცია ვერ მოიძებნა.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void InitDropDownUI()
        {
            YearDropDown.BackgroundColor = UIColor.FromRGB(243, 243, 243);
            YearDropDown.SelectionBackgroundColor = AppColors.TitleColor;
            DPDConstants.UI.TextColor = AppColors.TitleColor;
            DPDConstants.UI.SelectedTextColor = UIColor.White;

            //YearDropDown.TextFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 15);
            YearDropDown.ClipsToBounds = true;
            YearDropDown.Layer.CornerRadius = 20;

            MonthDropDown.BackgroundColor = UIColor.FromRGB(243, 243, 243);
            MonthDropDown.SelectionBackgroundColor = AppColors.TitleColor;
            DPDConstants.UI.TextColor = AppColors.TitleColor;
            DPDConstants.UI.SelectedTextColor = UIColor.White;

            //MonthDropDown.TextFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 15);
            MonthDropDown.ClipsToBounds = true;
            MonthDropDown.Layer.CornerRadius = 20;
        }

        private void InitGestures()
        {
            if (yearDropdownView.GestureRecognizers == null || yearDropdownView.GestureRecognizers?.Length == 0)
            {
                yearDropdownView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    InitDropDownUI();

                    YearDropDown.Show();
                }));
            }

            if (monthDropdownView.GestureRecognizers == null || monthDropdownView.GestureRecognizers?.Length == 0)
            {
                monthDropdownView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    InitDropDownUI();

                    MonthDropDown.Show();
                }));
            }
        }

        public IndicatorInfo IndicatorInfoForPagerTabStrip(PagerTabStripViewController pagerTabStripController)
        {
            return new IndicatorInfo("შედეგები");
        }

        private string[] GetYears(DateTime? minDate, DateTime? maxDate)
        {
            var minYear = minDate?.Year;
            var maxYear = maxDate?.Year;

            if (minDate?.Year != maxDate?.Year)
            {
                var years = Enumerable.Range(minYear.Value, maxYear.Value)?.Select(x => x.ToString())?.ToList();
                years.Insert(0, "წელი");
                return years.ToArray();
            }

            return new string[] { minYear.Value.ToString() };
        }

        private string[] GetMonth()
        {
            cultureInfo = new CultureInfo("ka-GE");

            var month = cultureInfo.DateTimeFormat.MonthNames.ToList();

            month.Insert(0, "თვე");

            return month?.ToArray();
        }

        private void InitHeader()
        {
            var maxPoint = StudentsStatistics?.OrderByDescending(x => x.Point)?.FirstOrDefault();
            var minTime = StudentsStatistics?.OrderBy(x => x.TestTimeInSecconds)?.FirstOrDefault();

            var testCounts = StudentsStatistics?.GroupBy(x => x.ExamDate);

            var maxTests = testCounts?.OrderBy(x => x.Count());

            System.Diagnostics.Debug.WriteLine(testCounts);


            var GroupdExams = StudentsStatistics.GroupBy(c =>
                                    c.ExamDate.Day
                                  ).Select(i => i.Select(o => o.ExamDate.ToShortDateString()).ToList()).ToList();

            if (GroupdExams.Count() > 0)
            {
                var GroupdResult = GroupdExams.OrderByDescending(i => i.Count).FirstOrDefault();


                testDate.Text = GroupdResult[0];
                testLbl.Text = GroupdResult.Count.ToString() + " ტესტი";
            }
        }
    }
}
