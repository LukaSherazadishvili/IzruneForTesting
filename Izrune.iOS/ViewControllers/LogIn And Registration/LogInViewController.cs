// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using System.Threading.Tasks;
using CoreAnimation;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;

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

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            InitUI();

            InitGestures();
        }

        private void InitGestures()
        {
        
            logInBtn.TouchUpInside += async delegate {
                //ShowLoginAlert();
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
            };

            registrationBtn.TouchUpInside += delegate {

                //TODO
                var registerVc = Storyboard.InstantiateViewController(ParentRegistrationViewController.StoryboardId) as ParentRegistrationViewController;
                this.NavigationController.PushViewController(registerVc, true);
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
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }
    }
}
