// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MpdcViewExtentions;
using System.Linq;
using Izrune.iOS.Utils;

namespace Izrune.iOS
{
	public partial class ParentRegiFirstViewController : UIViewController
	{
		public ParentRegiFirstViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ParentRegiFirstStoryboardId");

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();
        }

        private void InitUI()
        {
            var textFields = textFieldsStackView.Subviews?.Where(x => x is UITextField)?.Select(x => x as UITextField);

            foreach (var item in textFields)
            {
                item.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            }
        }
    }
}
