using System;
using System.Collections.Generic;
using IZrune.PCL.Enum;
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


        #region ViewControllerStoryboardIds

        const string MenuViewControllerStoryboardId = "MainMenuStoryboardId";
        const string LogInStoryboardId = "LogInViewControllerStoryboardId";

        #endregion

        public MenuRootViewController()
        {
            _storyBoard = UIStoryboard.FromName("Main", null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

