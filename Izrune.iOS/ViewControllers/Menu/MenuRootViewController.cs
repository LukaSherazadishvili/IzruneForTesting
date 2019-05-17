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

        Dictionary<MenuType, Func<UIViewController>> menuViewControllerCreations;

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
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            menuVc = GetMenuViewController() as MenuViewController;

            var mainVc = GetMainViewController();

            menuVc.MainMenuClicked = (menu, b) =>
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


            };

            SideBarController = new SidebarController(this, mainVc, menuVc)
            {
                MenuLocation = MenuLocations.Left,
                HasShadowing = true,
                MenuWidth = 280
            };
        }

        private void ShowMenu()
        {
            SideBarController.ToggleMenu();
        }

        private UIViewController GetMainViewController()
        {
            var mainVc = _storyBoard.InstantiateViewController(TestChooseViewController.StoryboardId) as TestChooseViewController;

            return mainVc;
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
    }
}

