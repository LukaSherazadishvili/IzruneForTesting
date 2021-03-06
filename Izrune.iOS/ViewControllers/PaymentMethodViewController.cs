// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;
using UIKit;

namespace Izrune.iOS
{
	public partial class PaymentMethodViewController : UIViewController
	{
		public PaymentMethodViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("PaymentMethodStoryboardId");
        private PaymentViewController paymentVc;
        public string PaymentUrl { get; set; }

        public IPay PayInfo { get; set; }
        public IPrice SelectedPrice;
        public Action GoToLogin { get; set; }
        public IStudent SelectedStudent;
        public bool HideTitle { get; set; }

        public int allPrices { get; set; }

        public string UserName { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            paymentVc = Storyboard.InstantiateViewController(PaymentViewController.StoryboardId) as PaymentViewController;
            paymentVc.PayInfo = PayInfo;

            InitGestures();

            paymentVc.GoToLogin = () => {
                this.NavigationController.PopViewController(false);
                GoToLogin?.Invoke(); 
                };

            titleLbl.Hidden = HideTitle;
        }

        private void InitGestures()
        {
            payViaBankBtn.TouchUpInside += async delegate {
                var payBankVc = Storyboard.InstantiateViewController(PayViaBankViewController.StoryboardId) as PayViaBankViewController;
                payBankVc.allPrices = allPrices;
                payBankVc.SelectedPrice = SelectedPrice;
                var user = await IZrune.PCL.Helpers.UserControl.Instance.GetCurrentUser();
                payBankVc.CurrentUser = user;
                payBankVc.UserName = UserName;

                payBankVc.UserPricesSum = UserControl.Instance.GetAllPackagePrice();
                this.NavigationController.PushViewController(payBankVc, true);
            };

            payViaCardBtn.TouchUpInside += delegate {
                GoToPayment();
            };

            payViaPayBoxBtn.TouchUpInside += delegate {
                PaymentUrl = "http://www.izrune.ge/images/tbcpay_image2.png";
                //await UserControl.Instance.ReNewPack(SelectedStudent.id, PromoVc.SelectedMont, PromoVc.SelectedPrice.price.Value, PromoVc.PromoCode);
                if (UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString(PaymentUrl)))
                    UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(PaymentUrl));
            };
        }

        private void GoToPayment()
        {
            this.NavigationController.PushViewController(paymentVc, true);
        }
    }
}
