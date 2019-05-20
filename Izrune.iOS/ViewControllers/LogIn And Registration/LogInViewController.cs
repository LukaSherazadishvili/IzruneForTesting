// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using CoreAnimation;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Services;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class LogInViewController : UIViewController
	{
		public LogInViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("LogInViewControllerStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();

            InitGestures();
        }

        private void InitGestures()
        {
        
            logInBtn.TouchUpInside += delegate {
                //ShowLoginAlert();
                try
                {
                    var userName = userNameTextField.Text;
                    var passord = passwordTextField.Text;

                    var loginSevice = ServiceContainer.ServiceContainer.Instance.Get<ILoginServices>();
                    var isLogedIn = loginSevice.LoginUser(userName, passord).Result;

                    if(isLogedIn)
                    {
                        var testVc = Storyboard.InstantiateViewController(TestViewController.StoryboardId) as TestViewController;
                        this.NavigationController.PushViewController(testVc, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };

            registrationBtn.TouchUpInside += delegate {

                //TODO
                var alert = UIAlertController.Create("Attention", "RegistrationClicked", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
                this.PresentViewController(alert, true, null);
            };
        }

        private void InitUI()
        {

            logInBtn.ToCardView(25, 10, 0.1f, UIColor.FromRGBA(0,0,0,0));
            registrationBtn.ToCardView(25, 3, 0.2f, AppColors.Tint);

            userNameTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 17);
            passwordTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 17);
        }

        private void ShowLoginAlert()
        {
            var alert = UIAlertController.Create("შეცდომა", "მომხმარებელი ან პაროლი არასორია.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        }
    }
}
