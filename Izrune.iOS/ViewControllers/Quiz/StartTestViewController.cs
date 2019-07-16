// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;
using UIKit;
using System.Timers;
using MPDC.iOS.Utils;
using MpdcViewExtentions;
using IZrune.PCL.Abstraction.Services;
using System.Threading.Tasks;
using IZrune.PCL.Enum;
using System.Diagnostics;

namespace Izrune.iOS
{
    public partial class StartTestViewController : UIViewController
    {
        public StartTestViewController (IntPtr handle) : base (handle)
        {
        }

        public static readonly NSString StoryboardId = new NSString("StartTestStoryboardId");


        DropDown UserNameDropDown = new DropDown();

        IParent Parent;
        private TimeSpan examDate;
        private TimeSpan testDate;
        List<IStudent> Students;
        private nint currentIndex;

        IStudent SelectedStudent;

        private bool IsSummTestActive;
        private bool IsExTestActive;

        private bool IsSummSelected;
        private Timer timer;
        private const double timerInterval = 1000;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.LayoutIfNeeded();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);
            InitUI();

            this.NavigationController.NavigationBar.InitTransparencyToNavBar();

            await LoadDataAsync();

            SelectedStudent = Students[0];

            InitGestures();

            InitDroDown();

            View.LayoutIfNeeded();

            UserControl.Instance.SeTSelectedStudent(SelectedStudent.id);

            UserNameDropDown.SelectRow(0);
        }

        private async Task LoadDataAsync()
        {
            contentView.Hidden = true;

            Parent = await UserControl.Instance.GetCurrentUser();

            examDate = (await QuezControll.Instance.GetExamDate(QuezCategory.QuezExam));

            testDate = (await QuezControll.Instance.GetExamDate(QuezCategory.QuezTest));

            InitSummTimer();

            Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();

            contentView.Hidden = false;
        }

        private bool IsExamActive(TimeSpan span)
        {
            //Debug.WriteLine($"{span.Hours} {span.Minutes} {span.Seconds}");
            var res = (span.Hours <= 0 && span.Minutes <= 0 && span.Seconds <= 0);
            return res;
            
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            summTestContentView.ApplyGradient(AppColors.PurpleGradient);
            exTestContentView.ApplyGradient(AppColors.YellowGradient);
        }

        private void InitUI()
        {
            viewForSummerShadow.AddShadowToView(10, 25, 0.8f, AppColors.TitleColor);
            viewForExShadow.AddShadowToView(10, 25, 0.8f, UIColor.FromRGB(242, 153, 52));
            shadowViewForDropDown.AddShadowToView(5, 20, 0.3f, UIColor.FromRGB(0, 0, 0));

            summTestContentView.ApplyGradient(AppColors.PurpleGradient);
            exTestContentView.ApplyGradient(AppColors.YellowGradient);
        }

        private void InitDroDown()
        {

            userNameLbl.Text = SelectedStudent?.Name + " " + SelectedStudent?.LastName;

            UserNameDropDown.AnchorView = new WeakReference<UIView>(viewForDropDown);
            UserNameDropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDropDown.Bounds.Height);
            UserNameDropDown.Width = this.viewForDropDown.Frame.Width;
            UserNameDropDown.Direction = Direction.Bottom;

            var array = Students?.Select(x => x.Name + " " + x.LastName)?.ToArray();

            UserNameDropDown.DataSource = array;

            UserNameDropDown.SelectionAction = (nint index, string name) =>
            {
                currentIndex = index;
                userNameLbl.Text = name;
                SelectedStudent = Students[(int)index];

                UserControl.Instance.SeTSelectedStudent(SelectedStudent.id);
            };
        }

        private void InitGestures()
        {
            if (summQuizTransparentView.GestureRecognizers == null || summQuizTransparentView.GestureRecognizers?.Count() == 0)
            {
                summQuizTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    if(Parent.IsAdmin)
                    {
                        IsSummSelected = true;
                        var smsVc = Storyboard.InstantiateViewController(SmsVerificationViewController.StoryboardId) as SmsVerificationViewController;
                        smsVc.SelectedStudent = SelectedStudent;

                        //chooseTimeVc.IsSumtTest = IsSummSelected;
                        //chooseTimeVc.SelectedStudent = SelectedStudent;
                        //chooseTimeVc.SelectedCategory = QuezCategory.QuezTest;

                        this.NavigationController.PushViewController(smsVc, true);
                    }
                    else
                    {
                        if (IsSummTestActive)
                        {
                            IsSummSelected = true;
                            var smsVc = Storyboard.InstantiateViewController(SmsVerificationViewController.StoryboardId) as SmsVerificationViewController;
                            smsVc.SelectedStudent = SelectedStudent;

                            //chooseTimeVc.IsSumtTest = IsSummSelected;
                            //chooseTimeVc.SelectedStudent = SelectedStudent;
                            //chooseTimeVc.SelectedCategory = QuezCategory.QuezTest;

                            this.NavigationController.PushViewController(smsVc, true);
                        }
                        else
                            ShowAlert();
                    }
                }));
            }

            if (exQuizTransparentView.GestureRecognizers == null || exQuizTransparentView.GestureRecognizers?.Count() == 0)
            {
                exQuizTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    //Sesacvleliiiiii !!!!!!
                    if (!IsExTestActive)
                    {
                        IsSummSelected = false;
                        var chooseTimeVc = Storyboard.InstantiateViewController(ChooseTimeViewController.StoryboardId) as ChooseTimeViewController;
                        chooseTimeVc.IsSumtTest = IsSummSelected;
                        chooseTimeVc.SelectedStudent = SelectedStudent;
                        chooseTimeVc.SelectedCategory = QuezCategory.QuezTest;

                        this.NavigationController.PushViewController(chooseTimeVc, true);
                    }
                    else
                        ShowAlert();
                }));
            }

            if (userNameTransparentView.GestureRecognizers == null || userNameTransparentView.GestureRecognizers?.Count() == 0)
            {
                userNameTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    InitDropDownUI();

                    UserNameDropDown.Layer.CornerRadius = 20;


                    UserNameDropDown.Show();
                }));
            }
        }

        private void InitDropDownUI()
        {
            UserNameDropDown.BackgroundColor = UIColor.FromRGB(243, 243, 243);
            UserNameDropDown.SelectionBackgroundColor = AppColors.TitleColor;
            DPDConstants.UI.TextColor = AppColors.TitleColor;
            DPDConstants.UI.SelectedTextColor = UIColor.White;

            UserNameDropDown.TextFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 15);
            UserNameDropDown.ClipsToBounds = true;
            UserNameDropDown.Layer.CornerRadius = 20;
        }

        private void InitSummTimer()
        {
            //testDate = new TimeSpan(0, 1, 5);

            timer = new Timer();
            timer.Interval = timerInterval;
            timer.Enabled = true;


            var Test1Date = examDate;
            var Test2Date = testDate;

            timer.Elapsed += (sender, e) =>
            {

                Test2Date = Test2Date.Subtract(TimeSpan.FromSeconds(1));
                //Debug.WriteLine($"{Test2Date.Minutes} : {Test2Date.Seconds}");
                if (Test2Date.Seconds == 0)
                    Test2Date = Test2Date.Add(new TimeSpan(0, -1, 0));
                //if(Test2Date.Minutes == 0)
                    //Test2Date.Subtract(TimeSpan.FromHours(1));
                
                InvokeOnMainThread(() =>
                {
                    test1TimerLbl.Text = GetStringDate(Test1Date);
                    test2TimerLbl.Text = GetStringDate(Test2Date);

                });

                if (Test1Date.Days == 0 && Test1Date.Hours == 0 && Test1Date.Minutes == 0)
                {
                    IsSummTestActive = true;
                    ChangeTestStatus(timeStackView, summQuisActiveStatusLbl, IsSummTestActive);
                    timer.Stop();
                    timer.Dispose();
                }
                else
                {
                    IsSummTestActive = false;
                    ChangeTestStatus(timeStackView, summQuisActiveStatusLbl, IsSummTestActive);
                }

                if (Test2Date.Days <= 0 && Test2Date.Hours <= 0 && Test2Date.Minutes <= 0)
                {
                    IsExTestActive = true;
                    ChangeTestStatus(exTimeStackView, exQuizActiveStatusLbl, IsExTestActive);
                    timer.Stop();
                    timer.Dispose();
                }
                else
                {
                    IsExTestActive = false;
                    ChangeTestStatus(exTimeStackView, exQuizActiveStatusLbl, IsExTestActive);
                }
               
            };

            timer.Start();
        }

        private void UpdateExamStatus(TimeSpan date, UIStackView stackView, UILabel statusLabel)
        {
            var res = IsExamActive(date);

            if (res)
            {
                IsSummTestActive = true;
                ChangeTestStatus(stackView, statusLabel, IsSummTestActive);
                timer.Dispose();
            }

            else
            {
                IsSummTestActive = false;
                ChangeTestStatus(stackView, statusLabel, IsSummTestActive);
            }
        }

        private string GetStringDate(TimeSpan date)
        {
            if (date.Days <= 0)
            {
                if(date.Hours <= 0)
                {
                    return $"{date.Minutes} წუთი";
                }
                return $"{date.Hours} საათი {date.Minutes} წუთი";
            }

            return $"{date.Days} დღე {date.Hours} საათი {date.Minutes} წუთი";
        }

        private DateTime? GetLastDayInMonth(int year, int month, DayOfWeek lastDay)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = daysInMonth; day > 0; day--)
            {
                DateTime currentDateTime = new DateTime(year, month, day);

                if (currentDateTime.DayOfWeek == lastDay)
                    return currentDateTime;
            }

            return null;
        }

        private void ShowAlert()
        {
            var alert = UIAlertController.Create("ყურადღება", "ტესტი დროებით მიუწვდომელია", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void ChangeTestStatus(UIStackView stackView, UILabel label, bool isActive)
        {
            try
            {
                InvokeOnMainThread(() =>
                {
                    stackView.Hidden = isActive;
                    label.Hidden = !isActive;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}