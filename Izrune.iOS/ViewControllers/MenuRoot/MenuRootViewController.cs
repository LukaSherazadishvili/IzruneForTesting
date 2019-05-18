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
                {MenuType.News, () => CreateViewControllerByStoryboard(NewsViewController.StoryboardId)},
                {MenuType.MoreInfo, () => CreateViewControllerByStoryboard(MoreInfoViewController.StoryboardId)},
                {MenuType.Contact, () => CreateViewControllerByStoryboard(ContactViewController.StoryboardId)},
                {MenuType.Main, () => CreateViewControllerByStoryboard(TestChooseViewController.StoryboardId)},
                {MenuType.Statistic, () => CreateViewControllerByStoryboard(StatisticViewController.StoryboardId)},
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

            var MainPageVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu,UIImage.FromBundle("ichamburger.png"),UIColor.Red);

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

            var navVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu, UIImage.FromBundle("ichamburger.png"), UIColor.Red);
            //var notifyBarButton = new NotifyBarButtonItem("ichamburger.png", ToggleMenu, UIColor.Red, ThemeColors.BarAccentColor.RgbToUIColor());


            // initNotifyWithIconOnNavBar(navVc.ViewControllers[0].NavigationItem,"ichamburger.png","0",ToggleMenu);

            return navVc;
        }
    }
}

