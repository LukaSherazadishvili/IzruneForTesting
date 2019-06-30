// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Foundation;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using UIKit;
using XLPagerTabStrip;

namespace Izrune.iOS
{
	public partial class ExamTabViewController : ButtonBarPagerTabStripViewController, IPagerTabStripDelegate
    {
		public ExamTabViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ExamTabStoryboardId");


        private List<UIViewController> TabVcs = new List<UIViewController>();

        private TestResultsViewController TesResultVc;
        private DiagramViewController DiagramVc;

        UINavigationController _navc;

        public bool HideHeader = true;
        public bool IsStatistic;

        public override void ViewDidLoad()
        {
            _navc = NavigationController;
            

            //this.NavigationController.NavigationBar.Translucent = false;
            //this.NavigationController.NavigationBar.InitNavigationBarColorWithNoShadow(UIColor.White);

            TesResultVc = Storyboard.InstantiateViewController(TestResultsViewController.StoryboardId) as TestResultsViewController;
            TesResultVc.Hideheader = HideHeader;

            DiagramVc = Storyboard.InstantiateViewController(DiagramViewController.StoryboardId) as DiagramViewController;
            
            TabVcs.Add(TesResultVc);
            TabVcs.Add(DiagramVc);

            InitBarStyle();

            this.Delegate = this;

            base.ViewDidLoad();

            this.NavigationController.NavigationBar.Translucent = false;
            NavigationController.View.BackgroundColor = UIColor.White;
            NavigationController.NavigationBar.InitNavigationBarColorWithNoShadow(UIColor.White);
            
            EdgesForExtendedLayout = UIRectEdge.None;
            View.LayoutIfNeeded();
            var barButton = new UIBarButtonItem(UIBarButtonSystemItem.Action, null);

            barButton.Clicked += delegate {
                var url = "";

                if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
                {
                    this.ShareUrl(url);
                }
            };
            this.NavigationItem.RightBarButtonItem = barButton;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);


            if (_navc != null)
                _navc.NavigationBar.Translucent = true;
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
