// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class ContactViewController : UIViewController
	{
		public ContactViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ContactViewControllerStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitUI();
        }

        private void InitUI()
        {
            phoneView.ToCardView(26, 3, 0.2f, UIColor.Black);
            mailView.ToCardView(26, 3, 0.2f, UIColor.Black);
            facebookView.ToCardView(26, 3, 0.2f, UIColor.Black);


        }
    }
}
