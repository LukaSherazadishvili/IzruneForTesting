﻿using System;
using System.Collections.Generic;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Enum;
using IZrune.PCL.Helpers;
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
        private IParent CurrentUser;
        UIImage barImage = UIImage.FromBundle("icmenuizrune.png");
        #region ViewControllerStoryboardIds

        const string MenuViewControllerStoryboardId = "MenuViewControllerStoryboardId";
        const string LogInStoryboardId = "LogInViewControllerStoryboardId";
        const string NewsStoryboardId = "NewsViewControllerStoryboardId";
        const string ContactStoryboardId = "ContactViewControllerStoryboardId";
        const string MoreInfoStoryboardId = "MoreInfoViewControllerStoryboardId";
        const string StatisticStoryboardId = "StudentStatisticStoryboardId";
        const string StartTestStoryboardId = "StartTestStoryboardId";
        const string UpdatePacketStoryboardId = "PacketViewControllerStoryboardId";
        const string EditProfileStoryboardId = "EditProfileStoryboardId";

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
                {MenuType.Main, () => CreateViewControllerByStoryboard(StartTestStoryboardId)},
                {MenuType.Statistic, () => CreateViewControllerByStoryboard(StatisticStoryboardId)},
                {MenuType.UpdatePacket, () => CreateViewControllerByStoryboard(UpdatePacketStoryboardId)},
                {MenuType.EditProfile, () => CreateViewControllerByStoryboard(EditProfileStoryboardId)},
                {MenuType.LogOut, () => CreateViewControllerByStoryboard(LogInStoryboardId)},
            };
        }
        public void GoToPacketVC()
        {
            CurrentMenu = MenuType.UpdatePacket;

            var currentVc = menuViewControllerCreations[CurrentMenu]?.Invoke();
            menuVc.SetSelectionBudget(CurrentMenu);

            SideBarController.ChangeContentView(currentVc);
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            menuVc = GetMenuViewController() as MenuViewController;

            var mainVc = GetMainViewController();

            SideBarController = new SidebarController(this, mainVc, menuVc)
            {
                MenuLocation = MenuLocations.Left,
                HasShadowing = false,
                MenuWidth = 280
            };

            menuVc.MainMenuClicked = (menu) =>
            {
                try
                {
                    if (CurrentMenu == menu.Type)
                        return;

                    CurrentMenu = menu.Type;

                    var currentVc = menuViewControllerCreations[menu.Type]?.Invoke();

                    if (menu.Type == MenuType.LogOut)
                    {
                        UserControl.Instance.LogOut();
                        menuVc.IsLogedIn = false;
                        menuVc.ShowUserInfo(false);
                        menuVc.ReloadMenu();
                        var navVc = currentVc as UINavigationController;
                        var loginVc = navVc.ViewControllers[0] as LogInViewController;

                        loginVc.LogedIn = async (logedIn) =>
                        {
                            await UpdateCurrentUser();
                            CurrentMenu = MenuType.Main;
                            menuVc.IsLogedIn = logedIn;
                            menuVc.ShowUserInfo(logedIn);
                            menuVc.ReloadMenu();
                            SideBarController.ChangeContentView(menuViewControllerCreations[MenuType.Main].Invoke());
                        };
                    }

                    if (menu.Type == MenuType.LogIn)
                    {
                        var navVc = currentVc as UINavigationController;
                        var loginVc = navVc.ViewControllers[0] as LogInViewController;
                        loginVc.LogedIn = async (logedIn) =>
                        {
                            await UpdateCurrentUser();
                            menuVc.IsLogedIn = logedIn;
                            menuVc.ShowUserInfo(logedIn);
                            menuVc.ReloadMenu();
                            SideBarController.ChangeContentView(menuViewControllerCreations[MenuType.Main].Invoke());
                        };
                    }

                    SideBarController.ChangeContentView(currentVc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            };

        }

        private async System.Threading.Tasks.Task UpdateCurrentUser()
        {
            CurrentUser = await IZrune.PCL.Helpers.UserControl.Instance.GetCurrentUser();
            menuVc.CurrentUser = CurrentUser;
            menuVc.InitUser();
        }

        private void ShowMenu()
        {
            SideBarController.ToggleMenu();
        }

        private UIViewController GetMainViewController()
        {
            //var mainVc = _storyBoard.InstantiateViewController(TestChooseViewController.StoryboardId) as TestChooseViewController;

            var MainPageVc = _storyBoard.InstantiateViewController(LogInViewController.StoryboardId).CreateWithNavigationControllerWithMenu(ToggleMenu,barImage,  AppColors.Tint, false);

            MainPageVc.NavigationBar.TintColor = AppColors.Tint;

            
            var barButton = new UIBarButtonItem();
            barButton.Image = barImage;
            barButton.Clicked += (sender, e) => ToggleMenu();

            MainPageVc.ViewControllers[0].NavigationItem.LeftBarButtonItem = barButton;

            var loginVc = MainPageVc.ViewControllers[0] as LogInViewController;

            loginVc.LogedIn = async (logedIn) =>
            {
                await UpdateCurrentUser();
                menuVc.IsLogedIn = logedIn;
                menuVc.ShowUserInfo(logedIn);
                menuVc.ReloadMenu();
                SideBarController.ChangeContentView(menuViewControllerCreations[MenuType.Main].Invoke());
            };

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

            var navVc = vc.CreateWithNavigationControllerWithMenu(ToggleMenu, barImage, AppColors.Tint, false);
                      
            vc.NavigationController.NavigationBar.TintColor = UIColor.White;

            return navVc;
        }

        void ToggleMenu() => SideBarController.ToggleMenu();

        UIViewController CreateViewControllerByStoryboard(string storyboardId)
        {
            //var navVc = _storyBoard.InstantiateViewController(storyboardId).CreateWithNavigationControllerWithMenu(ToggleMenu);

            var navVc = _storyBoard.InstantiateViewController(storyboardId).CreateWithNavigationControllerWithMenu(ToggleMenu, barImage, AppColors.Tint, false);

            navVc.NavigationBar.InitNavigationBarColorWithNoShadow(UIColor.White);

            navVc.NavigationBar.TintColor = AppColors.Tint;

            return navVc;
        }

    }
}

