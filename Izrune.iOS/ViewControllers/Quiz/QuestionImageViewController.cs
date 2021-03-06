// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class QuestionImageViewController : UIViewController, IUIScrollViewDelegate
	{
		public QuestionImageViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("QuestionImageStoryboardId");

        public string ImageUrl;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.Clear;

            this.DefinesPresentationContext = true;

            this.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            questionImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            questionImageView.InitImageFromWeb(ImageUrl, false, false);

            imageScrollView.Delegate = this;
            imageScrollView.MaximumZoomScale = 3f;

            closeBtn.TouchUpInside += delegate {
                CloseVc();
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            UIView.Animate(1, () => {

                this.mainBgView.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 50);
            });
        }

        private void CloseVc()
        {
            UIView.Animate(0.5f, () => mainBgView.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0), () => DismissViewController(true, null));
        }

        [Export("viewForZoomingInScrollView:")]
        public UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            return questionImageView;
        }

        private void UpdateMinZoomScaleForSize(CoreGraphics.CGSize size)
        {
            var widthScale = size.Width / questionImageView.Bounds.Width;
            var heightScale = size.Height / questionImageView.Bounds.Height;

            var minScale = Math.Min(widthScale, heightScale);

            imageScrollView.MinimumZoomScale = (System.nfloat)minScale;
            imageScrollView.ZoomScale = (System.nfloat)minScale;
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            UpdateMinZoomScaleForSize(View.Bounds.Size);
        }

        [Export("scrollViewDidZoom:")]
        public void DidZoom(UIScrollView scrollView)
        {
            UpdateConstraintsForSize(View.Bounds.Size);
        }

        private void UpdateConstraintsForSize(CoreGraphics.CGSize size)
        {
            var yOffset = (nfloat)Math.Max(0, (size.Height - questionImageView.Frame.Height) / 2);
            imageViewTop.Constant = yOffset;
            imageViewBottom.Constant = yOffset;

            var xOffset = (nfloat)Math.Max(0, (size.Width - questionImageView.Frame.Width) / 2);
            imageViewLeading.Constant = xOffset;
            imageViewTrailing.Constant = xOffset;

            View.LayoutIfNeeded();
        }
    }
}
