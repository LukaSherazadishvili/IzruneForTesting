using System;
using System.Collections.Generic;
using IZrune.PCL.Enum;
using MpdcViewExtentions;
using SidebarNavigation;
using UIKit;

namespace Izrune.iOS.ViewControllers
{
    public partial class MenuRootViewController : UIViewController
    {

        UIStoryboard _storyBoard;

        public SidebarController SideBarController { get; private set; }

        static public Dictionary<MenuType, Func<UIViewController>> menuViewControllerCreations;

        public static MenuType CurrentMenu { get; private set; }

        public MenuType SelectedMenu { get; set; } = MenuType.LogIn;

        private MenuViewController menuVc;

        #region ViewControllerStoryboardIds

        const string MenuViewControllerStoryboardId = "MenuViewControllerStoryboardId";
        const string LogInStoryboardId = "LogInViewControllerStoryboardId";

        #endregion

        public MenuRootViewController()
        {
            _storyBoard = UIStoryboard.FromName("Main", null);

            menuViewControllerCreations = new Dictionary<MenuType, Func<UIViewController>>()
            {
                {MenuType.LogIn, () => CreateViewControllerByStoryboard(LogInStoryboardId)},
                {MenuType.News, () => CreateViewControllerByStoryboard(NewsViewController.StoryboadrId)},
                {MenuType.MoreInfo, () => CreateViewControllerByStoryboard(MoreInfoViewController.StoryboardId)},
                {MenuType.Contact, () => CreateViewControllerByStoryboard(ContactViewController.StoryboardId)},
                {MenuType.Main, () => CreateViewControllerByStoryboard(TestChooseViewController.StoryboardId)},
                {MenuType.Statistic, () => CreateViewControllerByStoryboard(NewsViewController.StoryboadrId)},
                {MenuType.UpdatePacket, () => CreateViewControllerByStoryboard(NewsViewController.StoryboadrId)},
                {MenuType.EditProfile, () => CreateViewControllerByStoryboard(NewsViewController.StoryboadrId)},
                {MenuType.LogOut, () => CreateViewControllerByStoryboard(NewsViewController.StoryboadrId)},
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            menuVc = GetMenuViewController() as MenuViewController;

            var mainVc = GetMainViewController();


            SideBarController = new SidebarController(this, mainVc, menuVc)
            {
                MenuLocation = MenuLocations.Left,
                HasShadowing = true,
                MenuWidth = 280
            };


            menuVc.MainMenuClicked = (menu) =>
            {
                #region MenuClicks

                if (menu.Type == MenuType.Main)
                {
                    SideBarController.ChangeMenuView(mainVc);
                }

                if (menu.Type == MenuType.LogIn)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                if (menu.Type == MenuType.News)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                if (menu.Type == MenuType.MoreInfo)
                {
                    var moreInfoVc = _storyBoard.InstantiateViewController(MoreInfoViewController.StoryboardId) as MoreInfoViewController;

                    var navVc = PutVcInNav(moreInfoVc);
                    SideBarController.ChangeMenuView(navVc);
                }

                if (menu.Type == MenuType.Contact)
                {
                    var aboutUsVc = _storyBoard.InstantiateViewController(ContactViewController.StoryboardId) as ContactViewController;

                    var navVc = PutVcInNav(aboutUsVc);
                    SideBarController.ChangeMenuView(navVc);
                }

                if (menu.Type == MenuType.Statistic)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                if (menu.Type == MenuType.UpdatePacket)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                if (menu.Type == MenuType.EditProfile)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                if (menu.Type == MenuType.LogOut)
                {
                    //TODO
                    SideBarController.ChangeMenuView(null);
                }

                #endregion

                CurrentMenu = menu.Type;

                SideBarController.ChangeContentView(menuViewControllerCreations[menu.Type].Invoke());
            };

        }

        private void ShowMenu()
        {
            SideBarController.ToggleMenu();
        }

        private UIViewController GetMainViewController()
        {
            //var mainVc = _storyBoard.InstantiateViewController(TestChooseViewController.StoryboardId) as TestChooseViewController;

            var MainPageVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu,UIImage.FromBundle("4"),UIColor.Red);

            var barImage = UIImage.FromBundle("ichamburger.png");
            var barButton = new UIBarButtonItem();
            barButton.Clicked += (sender, e) => ToggleMenu();

            MainPageVc.ViewControllers[0].NavigationItem.LeftBarButtonItem = barButton;

            return MainPageVc;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private MenuViewController GetMenuViewController()
        {
            var menuVC = _storyBoard.InstantiateViewController(MenuViewControllerStoryboardId) as MenuViewController;
            return menuVC;
        }

        UIViewController CreateVcWithNavByStoryboard(string storyboardId)
        {
            return PutVcInNav(_storyBoard.InstantiateViewController(storyboardId));
        }


        UIViewController PutVcInNav(UIViewController vc)
        {

            var navVc = vc.CreateWithNavigationControllerWithMenu(ToggleMenu, UIImage.FromBundle("icMenu.png"), UIColor.Blue, false);
            navVc.NavigationBar.TintColor = UIColor.Red;

            return navVc;
        }

        void ToggleMenu() => SideBarController.ToggleMenu();

        UIViewController CreateViewControllerByStoryboard(string storyboardId)
        {
            //var navVc = _storyBoard.InstantiateViewController(storyboardId).CreateWithNavigationControllerWithMenu(ToggleMenu);

            var navVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu, UIImage.FromBundle("4"), UIColor.Red);
            //var notifyBarButton = new NotifyBarButtonItem("ichamburger.png", ToggleMenu, UIColor.Red, ThemeColors.BarAccentColor.RgbToUIColor());


            // initNotifyWithIconOnNavBar(navVc.ViewControllers[0].NavigationItem,"ichamburger.png","0",ToggleMenu);

            return navVc;
        }
    }
}

