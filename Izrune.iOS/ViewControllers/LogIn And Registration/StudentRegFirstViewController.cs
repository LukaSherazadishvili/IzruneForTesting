// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Helpers;
using MPDC.iOS.Utils;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class StudentRegFirstViewController : UIViewController
	{
		public StudentRegFirstViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("StudentRegFirstStoryboardId");
        private DateTime date;

        public Action SendClicked { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();

            InitGestures();

            SendClicked = () => { SenData(); };
        }

        private void InitGestures()
        {
            transparentTextField.EditingDidBegin += (sender, e) =>
            {
                ShowDatePicker();
            };
        }

        private void InitUI()
        {
            var textFields = textFieldsStackView.Subviews?.Where(x => x is UITextField)?.Select(x => x as UITextField);

            foreach (var item in textFields)
            {
                item.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            }

            transparentTextField.MakeRoundedTextField(20.0f, UIColor.Clear, 10);
            dayTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            monthTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            yearTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
        }

        private void ShowDatePicker()
        {
            var datePicker = new UIDatePicker();

            datePicker.Mode = UIDatePickerMode.Date;

            var toolBar = new UIToolbar();
            toolBar.SizeToFit();
            var doneButton = new UIBarButtonItem("არჩევა", UIBarButtonItemStyle.Plain, (sender, e) => {

                date = datePicker.Date.NSDateToDateTime();
                dayTextField.Text = date.Day.ToString();
                monthTextField.Text = date.Month.ToString();
                yearTextField.Text = date.Year.ToString();
                this.View.EndEditing(true);
            });

            var spaceButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null);

            var cancelButton = new UIBarButtonItem("დახურვა", UIBarButtonItemStyle.Plain, (s, e) => { this.View.EndEditing(true); });

            toolBar.SetItems(new UIBarButtonItem[] { cancelButton, spaceButton, doneButton }, false);

            transparentTextField.InputAccessoryView = toolBar;
            transparentTextField.InputView = datePicker;
        }

        private void SenData()
        {
            UserControl.Instance.RegistrationStudentPartOne(
                firstNameTf.Text,
                lastNameLTf.Text,
                date,
                privateNumberTf.Text,
                phoneTf.Text,
                emailTf.Text
                );
        }
    }
}
