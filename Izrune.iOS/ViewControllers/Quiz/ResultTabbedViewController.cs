// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Izrune.iOS
{
	public partial class ResultTabbedViewController : UIViewController
	{
		public ResultTabbedViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ResultTabbedStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var barButton = new UIBarButtonItem(UIBarButtonSystemItem.Action, null)
            {
                Title = "გაზიარება"
            };

            barButton.Clicked += delegate {
                //TODO
            };
            this.NavigationItem.RightBarButtonItem = barButton;

        }
    }
}
