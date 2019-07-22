using Firebase.CloudMessaging;
using Foundation;
using Izrune.iOS.Utils;
using Izrune.iOS.ViewControllers;
using UIKit;
using Xamarin;
using Firebase.Core;
using UserNotifications;
using System;

namespace Izrune.iOS
{

    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            IQKeyboardManager.SharedManager.Enable = true;
            IQKeyboardManager.SharedManager.ToolbarDoneBarButtonItemText = "დახურვა";
            IQKeyboardManager.SharedManager.ToolbarTintColor = AppColors.Tint;

            UIViewController rootvc = null;

            App.Configure();

            Firebase.CloudMessaging.Messaging.SharedInstance.Subscribe("all");

            UNUserNotificationCenter.Current.Delegate = this;
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            Messaging.SharedInstance.Delegate = this;

            IZrune.PCL.AppCore.Instance.InitServices();

            IZrune.PCL.AppCore.Instance.Alertdialog = new AlertDialogService();

            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);


            rootvc = new MenuRootViewController();

            Window.RootViewController = rootvc;

            Window.MakeKeyAndVisible();


            return true;
        }



        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            //base.RegisteredForRemoteNotifications(application, deviceToken);
#if DEBUG
            Messaging.SharedInstance.SetApnsToken(deviceToken, Firebase.CloudMessaging.ApnsTokenType.Sandbox);
#else
            Messaging.SharedInstance.SetApnsToken(deviceToken, ApnsTokenType.Production);
#endif
            var token = Messaging.SharedInstance.FcmToken ?? ""; ;

        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Console.WriteLine($"Firebase registration token: {fcmToken}");

            // TODO: If necessary send token to application server.
            // Note: This callback is fired at each app startup and whenever a new token is generated.
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message)
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive.
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

