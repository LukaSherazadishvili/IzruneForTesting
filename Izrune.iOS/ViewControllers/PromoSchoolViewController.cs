// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Izrune.iOS
{
	public partial class PromoSchoolViewController : UIViewController
	{
		public PromoSchoolViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("PromoSchoolStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.Clear;
            this.DefinesPresentationContext = true;
            this.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            if(closeImageView.GestureRecognizers == null || closeImageView.GestureRecognizers?.Length == 0)
            {
                closeImageView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    CloseCard();
                }));
            }

            if (mainView.GestureRecognizers == null || mainView.GestureRecognizers?.Length == 0)
            {
                mainView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    CloseCard();
                }));
            }
        }

        private void CloseCard()
        {
            UIView.Animate(0.5f, () => {

                mainView.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
            }, () => this.DismissViewController(true, null));

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            UIView.Animate(1, () => {

                this.mainView.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 60);
            });
        }
    }
}