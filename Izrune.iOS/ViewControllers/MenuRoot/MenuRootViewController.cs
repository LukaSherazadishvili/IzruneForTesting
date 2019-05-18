using System;
using System.Collections.Generic;
using Izrune.iOS.Utils;
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
        const string NewsStoryboardId = "NewsViewControllerStoryboardId";
        const string ContactStoryboardId = "ContactViewControllerStoryboardId";
        const string MoreInfoStoryboardId = "MoreInfoViewControllerStoryboardId";
        const string StatisticStoryboardId = "StatisticStoryboardId";
        #endregion

        public MenuRootViewController()
        {
            _storyBoard = UIStoryboard.FromName("Main", null);

            menuViewControllerCreations = new Dictionary<MenuType, Func<UIViewController>>()
            {
                {MenuType.LogIn, () => CreateViewControllerByStoryboard(LogInStoryboardId)},
                {MenuType.News, () => CreateViewControllerByStoryboard(NewsStoryboardId)},
                {MenuType.MoreInfo, () => CreateViewControllerByStoryboard(MoreInfoStoryboardId)},
                {MenuType.Contact, () => CreateViewControllerByStoryboard(ContactStoryboardId)},
                {MenuType.Main, () => CreateViewControllerByStoryboard(TestChooseViewController.StoryboardId)},
                {MenuType.Statistic, () => CreateViewControllerByStoryboard(StatisticStoryboardId)},
                {MenuType.UpdatePacket, () => CreateViewControllerByStoryboard(UpdatePacketViewController.StoryboardId)},
                {MenuType.EditProfile, () => CreateViewControllerByStoryboard(EditProfileViewController.StoryboardId)},
                {MenuType.LogOut, () => CreateViewControllerByStoryboard(NewsViewController.StoryboardId)},
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

                try
                {
                    CurrentMenu = menu.Type;

                    SideBarController.ChangeContentView(menuViewControllerCreations[menu.Type].Invoke());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };

        }

        private void ShowMenu()
        {
            SideBarController.ToggleMenu();
        }

        private UIViewController GetMainViewController()
        {
            //var mainVc = _storyBoard.InstantiateViewController(TestChooseViewController.StoryboardId) as TestChooseViewController;

            var MainPageVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu,UIImage.FromBundle("ichamburger.png"),  AppColors.Tint, false);

            var barImage = UIImage.FromBundle("ichamburger.png");
            var barButton = new UIBarButtonItem();
            barButton.Image = barImage;
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

            var navVc = vc.CreateWithNavigationControllerWithMenu(ToggleMenu, UIImage.FromBundle("icMenu.png"), AppColors.Tint, false);

            //vc.NavigationController.NavigationBar.InitNavigationBarColorWithNoShadow(UIColor.Orange);
            //vc.NavigationItem.InitLogoToNav(UIImage.FromBundle("4.png"));

            return navVc;
        }

        void ToggleMenu() => SideBarController.ToggleMenu();

        UIViewController CreateViewControllerByStoryboard(string storyboardId)
        {
            //var navVc = _storyBoard.InstantiateViewController(storyboardId).CreateWithNavigationControllerWithMenu(ToggleMenu);

            var navVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu, UIImage.FromBundle("ichamburger.png"), AppColors.Tint, false);

            return navVc;
        }
    }
}

