// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using UIKit;
using XLPagerTabStrip;

namespace Izrune.iOS
{
	public partial class ResultTabbedViewController : ButtonBarPagerTabStripViewController, IPagerTabStripDelegate
    {
		public ResultTabbedViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ResultTabbedStoryboardId");

        private List<UIViewController> TabVcs = new List<UIViewController>();

        public List<IQuestion> Questions;

        public IQuisInfo QuisInfo;
        public bool IsExamResult;

        ExamResultViewController ExRes;
        QuestionResultViewController QuestionRes;

        UINavigationController _navc;

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);


            if(_navc!=null)
                _navc.NavigationBar.Translucent = true;
        }

        public override void ViewDidLoad()
        {
            _navc = NavigationController;

            this.NavigationController.NavigationBar.Translucent = false;
            ExRes = Storyboard.InstantiateViewController(ExamResultViewController.StoryboardId) as ExamResultViewController;
            ExRes.QuisInfo = QuisInfo;
            ExRes.AfterExam = IsExamResult;

            QuestionRes = Storyboard.InstantiateViewController(QuestionResultViewController.StoryboardId) as QuestionResultViewController;
            QuestionRes.Questions = Questions;


            TabVcs.Add(ExRes);
            TabVcs.Add(QuestionRes);

            InitBarStyle();

            this.Delegate = this;
            base.ViewDidLoad();

            var barButton = new UIBarButtonItem(UIBarButtonSystemItem.Action, null);

            barButton.Clicked += delegate {
                var url = QuisInfo.DiplomaURl;

                if(!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
                {
                    this.ShareUrl(url);
                }
            };
            this.NavigationItem.RightBarButtonItem = barButton;

        }

        public override UIViewController[] CreateViewControllersForPagerTabStrip(PagerTabStripViewController pagerTabStripViewController)
        {
            return TabVcs.ToArray();
        }

        private void InitBarStyle()
        {
            Settings.Style.ButtonBarBackgroundColor = UIColor.Clear;
            Settings.Style.ButtonBarItemBackgroundColor = UIColor.Clear;
            Settings.Style.SelectedBarBackgroundColor = AppColors.Tint;
            Settings.Style.SelectedBarHeight = 2;

            Settings.Style.ButtonBarItemFont = UIFont.FromName("BPG Mrgvlovani 2010", 16);

            Settings.Style.ButtonBarMinimumLineSpacing = 1;
            Settings.Style.ButtonBarItemTitleColor = AppColors.TitleColor;

            Settings.Style.ButtonBarItemsShouldFillAvailiableWidth = true;
            Settings.Style.ButtonBarLeftContentInset = 15;

            Settings.Style.ButtonBarItemLeftRightMargin = 5;

            Settings.Style.ButtonBarRightContentInset = 0;
            Settings.Style.ButtonBarHeight = 45;
            Settings.Style.ButtonBarMinimumLineSpacing = 0;

        }

    }
}
