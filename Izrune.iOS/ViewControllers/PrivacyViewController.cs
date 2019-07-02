// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using Foundation;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class PrivacyViewController : BaseViewController, IUIWebViewDelegate
	{
		public PrivacyViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("PrivacyStoryboardId");

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.Clear;
            this.DefinesPresentationContext = true;
            this.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            privacyWebView.Delegate = this;

            await LoadDataAsync();

            this.View.AddGestureRecognizer(new UITapGestureRecognizer(CloseCard));

            mainBgView.Layer.CornerRadius = 25;
            //mainBgView.ToCardView(25, 3, 0.2f, UIColor.FromRGBA(0, 0, 0, 153));
            mainBgView.AddShadowToView(3, 25, 0.7f, UIColor.FromRGBA(0, 0, 0, 153));
            privacyWebView.ClipsToBounds = true;
            privacyWebView.Layer.CornerRadius = 25;

            if(closeImageView.GestureRecognizers == null || closeImageView.GestureRecognizers?.Length == 0)
            {
                closeImageView.AddGestureRecognizer(new UITapGestureRecognizer(CloseCard));
            }

            privacyWebView.Layer.MaskedCorners = CoreAnimation.CACornerMask.MinXMaxYCorner | CoreAnimation.CACornerMask.MaxXMaxYCorner;
        }

        private void CloseCard()
        {
            UIView.Animate(0.4f, () => {

                this.View.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
            }, () => this.DismissViewController(true, null));

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            UIView.Animate(1, () => {

                this.View.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 100);
            });
        }

        private async Task LoadDataAsync()
        {
            try
            {
                ShowLoading();
                var service = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();

                var htlmString = await service.GetAgreement();

                privacyWebView.LoadHtmlString(htlmString, NSUrl.FromString("https://www.google.com/"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                EndLoading();
            }
        }
    }
}
