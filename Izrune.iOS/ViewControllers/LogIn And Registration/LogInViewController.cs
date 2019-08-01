// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using System.Threading.Tasks;
using CoreAnimation;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;
using UserNotifications;

namespace Izrune.iOS
{
	public partial class LogInViewController : BaseViewController
	{
		public LogInViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("LogInViewControllerStoryboardId");

        public Action<bool> LogedIn { get; set; }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitNotifications();

            userNameTextField.Text = "tikitiki";
            passwordTextField.Text = "samisami";

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            InitUI();

            InitGestures();

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 3) && !System.Diagnostics.Debugger.IsAttached)
                StoreKit.SKStoreReviewController.RequestReview();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //UserControl.Instance.Resetregistration();
        }
        private void InitGestures()
        {
        
            logInBtn.TouchUpInside += async delegate {

                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        ShowLoading();
                        var userName = userNameTextField.Text;
                        var passord = passwordTextField.Text;
                        //await Task.Delay(10000);

                        var loginSevice = ServiceContainer.ServiceContainer.Instance.Get<ILoginServices>();
                        var isLogedIn = (await loginSevice.LoginUser(userName, passord));
                        EndLoading();

                        if (isLogedIn)
                        {

                            LogedIn?.Invoke(isLogedIn);
                        }
                        else
                        {
                            ShowLoginAlert();
                        }

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                else
                    ShowConnectionAlert();
                

            };

            registrationBtn.TouchUpInside += delegate {

                //TODO
                if(Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                {
                    var registerVc = Storyboard.InstantiateViewController(ParentRegistrationViewController.StoryboardId) as ParentRegistrationViewController;
                    this.NavigationController.PushViewController(registerVc, true);
                }
                else
                    ShowConnectionAlert();
            };

            forgotPasswordLbl.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                var recoveryVc = Storyboard.InstantiateViewController(PasswordRecoveryViewController.StoryboardId) as PasswordRecoveryViewController;

                this.NavigationController.PushViewController(recoveryVc, true);

            }));

            forgotUserNameLbl.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                var recoveryVc = Storyboard.InstantiateViewController(PasswordRecoveryViewController.StoryboardId) as PasswordRecoveryViewController;

                recoveryVc.IsPassworPage = false;

                this.NavigationController.PushViewController(recoveryVc, true);
            }));
        }

        private void InitUI()
        {

            logInBtn.ToCardView(25, 10, 0.1f, UIColor.FromRGBA(0,0,0,0));
            registrationBtn.ToCardView(25, 3, 0.2f, AppColors.Tint);

            userNameTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 17);
            passwordTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 17);

            logInBtn.AddShadowToView(10, 25, 0.8f, AppColors.Succesful);
            registrationBtn.AddShadowToView(10, 25, 0.8f, AppColors.TitleColor);

        }

        private void ShowLoginAlert()
        {
            var alert = UIAlertController.Create("შეცდომა", "მომხმარებელი ან პაროლი არასორია.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        private void ShowConnectionAlert()
        {
            var alert = UIAlertController.Create("შეცდომა", "შეამოწმეთ ინტერნეტთან კავშირი", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }

        const string NotificationPageShowedKey = "notificationPageShowedKey";

        private void InitNotifications()
        {
            var isShown = NSUserDefaults.StandardUserDefaults.BoolForKey(NotificationPageShowedKey);

            if(!isShown)
            {
                if(UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

                    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (arg1, arg2) =>
                     {
                         if(arg1)
                         {

                         }
                     });
                }
                else
                {
                    var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                }
            }
        }
    }
}
