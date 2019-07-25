// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class StudentStatisticViewController : BaseViewController
	{
		public StudentStatisticViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("StudentStatisticStoryboardId");
        DropDown CurentStudentDP = new DropDown();
        private IEnumerable<IDiplomStatistic> diplomeStatistics;
        IStudent CurrentStudent;

        private List<IStudent> Students;
        private int currentStudentIndex;

        private DiplomeViewController diplomeVc;
        private TestResultsViewController resultVc;
        private ExamTabViewController examTabVc;

        private bool IsPacketActive { get; set; }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);
            // this.NavigationController.NavigationBar.InitTransparencyToNavBar();
            this.NavigationController.NavigationBar.Translucent = false;

            InitUI();
            await LoadDataAsync();
            InitForm(CurrentStudent);
            InitViewCOntrollers();

            View.LayoutIfNeeded();

            InitDropDowns();

            var result = CurrentStudent?.PakEndDate - DateTime.Now;

            IsPacketActive = result?.TotalDays > 0;

            InitGestures();
        }

        private void ShowAlert()
        {
            var alert = UIAlertController.Create("ყურადღება", "სტატისტიკის სანახავად განაახლეთ პაკეტი", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void InitViewCOntrollers()
        {
            diplomeVc = Storyboard.InstantiateViewController(DiplomeViewController.StoryboardId) as DiplomeViewController;
            resultVc = Storyboard.InstantiateViewController(TestResultsViewController.StoryboardId) as TestResultsViewController;
            examTabVc = Storyboard.InstantiateViewController(ExamTabViewController.StoryboardId) as ExamTabViewController;
        }

        private async Task LoadDataAsync()
        {
            contentView.Hidden = true;
            ShowLoading();
            Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();

            var statisticService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

            //diplomeStatistics = await statisticService.GetDiplomaStatisticAsync();

            CurrentStudent = Students?[0];
            EndLoading();
            contentView.Hidden = false;
        }

        private void InitForm(IStudent student)
        {
            currentStudentLbl.Text = student.Name + " " + student.LastName;

            bool hide = student?.PakEndDate == null;
            packetDateLbl.Hidden = hide;
            titleLbl.Hidden = hide;

            packetDateLbl.Text = student?.PakEndDate?.ToString("dd/MM/yyyy");
        }

        private void InitUI()
        {

            initView(diplomeView, diplomeShadow);
            initView(sumTestsView, sumShadow);
            initView(exTestView, exShadow);
            paymentHostoryBtn.Layer.CornerRadius = 25;
            this.NavigationController.NavigationBar.InitNavigationBarColorWithNoShadow(UIColor.White);
        }

        private void initView(UIView view, UIView viewForShadow)
        {
            view.Layer.BorderWidth = 3;
            view.Layer.BorderColor = UIColor.White.CGColor;
            view.Layer.CornerRadius = 25;
            view.ApplyGradient(AppColors.PurpleGradient);

            viewForShadow.AddShadowToView(10, 25, 0.8f, AppColors.TitleColor);
        }

        private void InitGestures()
        {
            if (diplomeView.GestureRecognizers == null || diplomeView.GestureRecognizers?.Length == 0)
            {
                diplomeView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    if (IsPacketActive)
                    {
                        diplomeVc.Student = CurrentStudent;
                        diplomeVc.ShouldLoadData = true;
                        this.NavigationController.PushViewController(diplomeVc, true);
                    }
                    else
                        ShowAlert();

                }));
            }

            if (sumTestsView.GestureRecognizers == null || sumTestsView.GestureRecognizers?.Length == 0)
            {
                sumTestsView.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                    if (IsPacketActive)
                        this.NavigationController.PushViewController(resultVc, true);
                    else
                        ShowAlert();
                }));
            }

            if (exTestView.GestureRecognizers == null || exTestView.GestureRecognizers?.Length == 0)
            {
                exTestView.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                    if (IsPacketActive)
                    {
                        examTabVc.HideHeader = false;

                        this.NavigationController.PushViewController(examTabVc, true);
                    }
                    else
                        ShowAlert();
                }));
            }

            paymentHostoryBtn.TouchUpInside += delegate {

                var paymentHistoryVc = Storyboard.InstantiateViewController(PaymentHistoryViewController.StoryboardId) as PaymentHistoryViewController;

                this.NavigationController.PushViewController(paymentHistoryVc, true);
            };

        }

        private void InitDropDowns()
        {
            SetupDropDown(CurentStudentDP, currentStudentView, currentStudentLbl);
            SetupDropDownGesture(CurentStudentDP, currentStudentView);

            var studentsArray = Students?.Select(x => x.Name +" " + x.LastName)?.ToArray();
            CurentStudentDP.DataSource = studentsArray;


            CurentStudentDP.SelectionAction = async (nint index, string name) =>
            {
                if (currentStudentIndex != index)
                {
                    currentStudentIndex = (int)index;
                    CurrentStudent = Students?[(int)index];
                    InitForm(CurrentStudent);
                    try
                    {

                        UserControl.Instance.SeTSelectedStudent(CurrentStudent.id);
                        CurrentStudent = Students?[(int)index];

                        var result = CurrentStudent?.PakEndDate - DateTime.Now;
                        IsPacketActive = result?.Days > 0;

                        await UpdateData();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
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

        private async Task UpdateData()
        {
            diplomeVc.Student = CurrentStudent;
            await diplomeVc.UpdateData();

            await resultVc.UpdateData();
        }
    }
}
