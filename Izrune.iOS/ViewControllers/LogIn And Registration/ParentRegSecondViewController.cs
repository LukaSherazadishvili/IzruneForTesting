// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using Foundation;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class ParentRegSecondViewController : UIViewController
	{
		public ParentRegSecondViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ParentRegSecondStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //InitUI();
        }

        private void InitUI()
        {
            var textFields = textFieldsStackView.Subviews?.Select(x => x as UITextField);

            foreach (var item in textFields)
            {
                item.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            }
        }
    }
}