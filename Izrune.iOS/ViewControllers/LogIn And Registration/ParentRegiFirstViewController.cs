// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MpdcViewExtentions;
using System.Linq;
using Izrune.iOS.Utils;
using MPDC.iOS.Utils;
using IZrune.PCL.Helpers;
using System.Collections.Generic;
using IZrune.PCL.Abstraction.Models;
using FPT.Framework.iOS.UI.DropDown;
using System.Globalization;

namespace Izrune.iOS
{
	public partial class ParentRegiFirstViewController : UIViewController
	{
		public ParentRegiFirstViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("ParentRegiFirstStoryboardId");
        private DateTime date = default(DateTime);
        public Action SendClicked { get; set; }

        DropDown CityDropDown = new DropDown();

        public List<IRegion> CityList;

        private string City="";

        CultureInfo cultureInfo = new CultureInfo("ka-GE");

        public string UserName { get; set; }
        public bool IsFormFilled()
        {
            var res = (firstNameTextfield.Text.IsEmtyOrNull() && lastNameTextField.Text.IsEmtyOrNull() && date.Year > 0001 && City.IsEmtyOrNull());
            return res;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();

            InitGestures();

            InitDroDown();

            SendClicked = () => {
                SenData();
                UserName = firstNameTextfield.Text + " " + lastNameTextField.Text;
            };
        }

        private void InitUI()
        {
            var textFields = textFieldsStackView.Subviews?.Where(x => x is UITextField)?.Select(x => x as UITextField);

            foreach (var item in textFields)
            {
                item.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            }

            transparentDateTextfield.MakeRoundedTextField(20.0f, UIColor.Clear, 10);
            daylLbl.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            monthLbl.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            yearLbl.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
        }

        private void InitGestures()
        {
            transparentDateTextfield.EditingDidBegin += (sender, e) =>
            {
                ShowDatePicker();
            };

            if(cityView.GestureRecognizers == null || cityView.GestureRecognizers?.Length == 0)
            {
                cityView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                    CityDropDown.Show();
                    InitDropDownUI(CityDropDown);
                }));
            }
        }

        private void ShowDatePicker()
        {
            var datePicker = new UIDatePicker();

            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.Locale = new NSLocale("ka-GE");

            var toolBar = new UIToolbar();
            toolBar.SizeToFit();
            var doneButton = new UIBarButtonItem("არჩევა", UIBarButtonItemStyle.Plain, (sender, e) => {

                date = datePicker.Date.NSDateToDateTime();
                InitDate(date);
                this.View.EndEditing(true);
            });

            var spaceButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null);

            var cancelButton = new UIBarButtonItem("დახურვა", UIBarButtonItemStyle.Plain, (s,e) => { this.View.EndEditing(true); });

            toolBar.SetItems(new UIBarButtonItem[] { cancelButton , spaceButton, doneButton }, false);

            transparentDateTextfield.InputAccessoryView = toolBar;
            transparentDateTextfield.InputView = datePicker;
        }

        private void SenData()
        {
            UserControl.Instance.RegistrationParrentPartOne(
                firstNameTextfield.Text,
                lastNameTextField.Text,
                date,
                cityLbl.Text,
                villageTextField.Text
                );
        }

        private void InitDroDown()
        {
            CityDropDown.AnchorView = new WeakReference<UIView>(cityView);
            CityDropDown.BottomOffset = new CoreGraphics.CGPoint(0, cityView.Bounds.Height);
            CityDropDown.Width = this.View.Frame.Width;
            CityDropDown.Direction = Direction.Top;

            var array = CityList?.Select(x => x.title)?.ToArray();

            CityDropDown.DataSource = array;

            CityDropDown.SelectionAction = (nint index, string name) =>
            {
                cityLbl.Text = name;
                City = name;
            };
        }

        private void InitDropDownUI(DropDown dropDown)
        {
            dropDown.BackgroundColor = UIColor.FromRGB(243, 243, 243);
            dropDown.SelectionBackgroundColor = AppColors.TitleColor;
            DPDConstants.UI.TextColor = AppColors.TitleColor;
            DPDConstants.UI.SelectedTextColor = UIColor.White;

            dropDown.TextFont = UIFont.FromName("BPG Mrgvlovani Caps 2010", 15);
            dropDown.ClipsToBounds = true;
            dropDown.Layer.CornerRadius = 20;
        }

        private void InitDate(DateTime _date)
        {
            daylLbl.Text = _date.Day.ToString();
            monthLbl.Text = _date.ToString("MMMM", cultureInfo);
            yearLbl.Text = _date.Year.ToString();
        }
    }
}
