// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;
using UIKit;

namespace Izrune.iOS
{
	public partial class AddStudentViewController : UIViewController
	{
		public AddStudentViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("AddStudentViewControllerStoryboardId");

        public Action SendClicked { get; set; }

        public Action<IPay> DataSent { get; set; }

        public Action AddMoreStudentClicked { get; set; }

        public string PaymenUrl;

        public IPay Pay { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitGestures();

            InitUI();

            SendClicked = async () => await SendData();

            addNewStudentBtn.TouchUpInside += delegate {
                AddMoreStudentClicked?.Invoke();
            };
        }

        private async Task SendData()
        {
            if(IsMarked)
            {
                try
                {
                    var ipay = (await UserControl.Instance.FinishRegistration());
                    DataSent?.Invoke(ipay);
                    Pay.CurrentUserPayURl = ipay.CurrentUserPayURl;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            else
            {
                var alert = UIAlertController.Create("ყურადღება", "დასრულებისთვის აუცილებელია დაეთანხმოთ პირობებს", UIAlertControllerStyle.Alert);

                alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Cancel, null));
                this.PresentViewController(alert, true, null);
            }
        }

        private void InitUI()
        {
            addNewStudentBtn.Layer.CornerRadius = 25;
            agreeView.Layer.CornerRadius = 17.5f;
            selectedAgreeView.Layer.CornerRadius = 12.5f;
           
        }

        bool IsMarked;

        private void InitGestures()
        {
            addNewStudentBtn.TouchUpInside += delegate {

                //TODO
            };

            if (privacyLbl.GestureRecognizers == null || privacyLbl.GestureRecognizers?.Length == 0)
            {
                privacyLbl.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                    var privacyVc = Storyboard.InstantiateViewController(PrivacyViewController.StoryboardId) as PrivacyViewController;

                    privacyVc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
                    this.PresentViewController(privacyVc, true, null);

                }));
            }

            if (agreeView.GestureRecognizers == null || agreeView.GestureRecognizers?.Length == 0)
            {
                agreeView.AddGestureRecognizer(new UITapGestureRecognizer(() => {

                    IsMarked = !IsMarked;
                    selectedAgreeView.Hidden = !IsMarked;
                }));
            }
        }
    }
}
