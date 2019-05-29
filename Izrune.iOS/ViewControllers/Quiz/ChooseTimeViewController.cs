// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Enum;
using IZrune.PCL.Helpers;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class ChooseTimeViewController : UIViewController
	{
		public ChooseTimeViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ChooseTimeStoryboardId");

        public IStudent SelectedStudent;
        public QuezCategory SelectedCategory;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitGestures();

            View.LayoutIfNeeded();

            InitUI();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            totalTimeView.ApplyGradient(AppColors.PurpleGradient);
            derivedTimeView.ApplyGradient(AppColors.PurpleGradient);
        }

        private void InitUI()
        {
       
            totalTimeShadowView.AddShadowToView(10, 25, 0.8f, AppColors.TitleColor);
            derivedTimeShadowView.AddShadowToView(10, 25, 0.8f, AppColors.TitleColor);

            totalTimeView.ApplyGradient(AppColors.PurpleGradient);
            derivedTimeView.ApplyGradient(AppColors.PurpleGradient);

            popUpView.Layer.CornerRadius = 25;
            popUpView.Layer.BorderWidth = 5;
            popUpView.Layer.BorderColor = AppColors.TitleColor.CGColor;
        }

        private void InitGestures()
        {
            if(totalTimeView.GestureRecognizers == null || totalTimeView.GestureRecognizers?.Length == 0)
            {
                totalTimeView.AddGestureRecognizer(new UITapGestureRecognizer(async() =>
                {
                    //TODO
                    var data = (await GetQuiz(SelectedStudent.id, SelectedCategory)).ToList();

                    var testVc = Storyboard.InstantiateViewController(TestViewController.StoryboardId) as TestViewController;
                    testVc.AllQuestions = data;
                    testVc.IsTotalTime = true;

                    this.NavigationController.PushViewController(testVc, true);
                }));
            }

            if (derivedTimeView.GestureRecognizers == null || derivedTimeView.GestureRecognizers?.Length == 0)
            {
                derivedTimeView.AddGestureRecognizer(new UITapGestureRecognizer(async() =>
                {
                    //TODO
                    var data = (await GetQuiz(SelectedStudent.id, SelectedCategory)).ToList();

                    var testVc = Storyboard.InstantiateViewController(TestViewController.StoryboardId) as TestViewController;
                    testVc.AllQuestions = data;
                    testVc.IsTotalTime = false;

                    this.NavigationController.PushViewController(testVc, true);
                }));
            }

            if (closePopUpView.GestureRecognizers == null || closePopUpView.GestureRecognizers?.Length == 0)
            {
                closePopUpView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    popUpView.Fade(false, 0.3f, () => { popUpView.Hidden = true; });
                }));
            }
        }

        private async Task<List<IQuestion>> GetQuiz(int id, QuezCategory quizCategory)
        {
            UserControl.Instance.SeTSelectedStudent(id);

            var service = ServiceContainer.ServiceContainer.Instance.Get<IQuezServices>();

            var data = (await service.GetQuestionsAsync(quizCategory))?.ToList();

            return data;
        }
    }
}
