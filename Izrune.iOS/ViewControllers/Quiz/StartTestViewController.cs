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

        List<IStudent> Students;
        private nint currentIndex;

        IStudent SelectedStudent;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Parent = await UserControl.Instance.GetCurrentUser();

            Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();

            var item = Students[0];

            Students.Add(item);
            Students.Add(item);
            Students.Add(item); 
            Students.Add(item); 
            Students.Add(item);

            InitTimer();

            InitGestures();

            InitDroDown();
        }

        private void InitDroDown()
        {
            UserNameDropDown.AnchorView = new WeakReference<UIView>(viewForDropDown);
            UserNameDropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDropDown.Bounds.Height);
            UserNameDropDown.Width = this.viewForDropDown.Frame.Width;
            UserNameDropDown.Direction = Direction.Bottom;

            var array = Students?.Select(x => x.Name + x.LastName)?.ToArray();

            UserNameDropDown.DataSource = array;

            UserNameDropDown.SelectionAction = (nint index, string name) =>
            {
                currentIndex = index;
                userNameLbl.Text = name;

                SelectedStudent = Students[(int)index];
            };
        }

        private void InitGestures()
        {
            if (summQuizTransparentView.GestureRecognizers == null || summQuizTransparentView.GestureRecognizers?.Count() == 0)
            {
                summQuizTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                    //TODO Go to examvc
                }));
            }

            if (exQuizTransparentView.GestureRecognizers == null || exQuizTransparentView.GestureRecognizers?.Count() == 0)
            {
                exQuizTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                    //TODO Go to examvc
                }));
            }

            if (userNameTransparentView.GestureRecognizers == null || userNameTransparentView.GestureRecognizers?.Count() == 0)
            {
                userNameTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    InitDropDownUI();

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

        private void InitTimer()
        {
            var seconds = 60;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;



            timer.Elapsed += (sender, e) => {

                var thuersday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
                var today = DateTime.Now;

                var diffrence = thuersday - today;

                var days = diffrence.Days;
                var hours = diffrence.Hours;
                var minutes = diffrence.Minutes;
                var sec = diffrence.Seconds;

                InvokeOnMainThread(() => test1TimerLbl.Text = $"{days} დღე {hours} საათი {minutes} წუთი {sec} წამი");
            };

            timer.Start();
        }
    }
}
