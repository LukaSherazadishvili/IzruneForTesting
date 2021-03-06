// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDC.iOS.Utils;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class EditParentProfileViewController : BaseViewController
	{
		public EditParentProfileViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("EditParentStoryboardId");

        CultureInfo cultureInfo = new CultureInfo("ka-GE");

        private IParent Parent;
        DropDown CityDP = new DropDown();

        private List<IRegion> Regions;
        private int currentRegionIndex;
        private int RegionId;
        private DateTime date;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                await LoadDataAsync();
            else
            {
                this.ShowConnectionAlert();
                InitUI();
                return;
            }

            InitGestures();
            InitDropDowns();
            InitUI();
        }

        private async Task LoadDataAsync()
        {
            contentView.Hidden = true;
            ShowLoading();
            Parent = await UserControl.Instance.GetCurrentUser();

            var registerService = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();
            Regions = (await registerService.GetRegionsAsync())?.ToList();

            InitForm(Parent);
            EndLoading();
            contentView.Hidden = false;
        }

        //qalqi, sofeli, telefoni, elfosta, 
        private void UpdateStudenProfile(string phoneNumber, string email, string city, string village)
        {
            Parent.Phone = phoneNumber;
            Parent.Email = email;
            Parent.City = city;
        }

        private void InitGestures()
        {
            saveBtn.TouchUpInside += async delegate
            {
                UpdateStudenProfile(phoneTf.Text, emailTf.Text, cityLbl.Text, villageTf.Text);

                await UserControl.Instance.EditParrentProfile(emailTf.Text, phoneTf.Text, cityLbl.Text, villageTf.Text);

                this.NavigationController.PopViewController(true);
            };

            backBtn.TouchUpInside += delegate {
                this.NavigationController.PopViewController(true);
            };

        }

        private void InitUI()
        {
            backBtn.Layer.CornerRadius = 25;

            cityView.Layer.CornerRadius = 20;
            dateTransparentTf.MakeRoundedTextField(20.0f, UIColor.Clear, 20);
            villageTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            phoneTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            emailTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            dayTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            monthTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            yearTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
        }

        private void InitForm(IParent parent)
        {
            parentNameLbl.Text = parent?.Name;
            parentLastNameLbl.Text = parent?.LastName;

            if (parent.bDate.HasValue)
            {
                InitDate(parent.bDate.Value);
            }

            phoneTf.Text = parent?.Phone;
            emailTf.Text = parent?.Email;
            cityLbl.Text = parent?.City;
            villageTf.Text = parent?.Vilage;
        }

        private void SetupDropDown(DropDown dropDown, UIView viewForDpD, UILabel dropDownLbl)
        {
            dropDown.AnchorView = new WeakReference<UIView>(viewForDpD);
            dropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDpD.Bounds.Height);
            dropDown.Width = View.Frame.Width;
            dropDown.Direction = Direction.Bottom;
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

        private void SetupDropDownGesture(DropDown dropDown, UIView viewforDpD)
        {
            if (viewforDpD.GestureRecognizers == null || viewforDpD.GestureRecognizers?.Length == 0)
            {
                viewforDpD.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    dropDown.Show();
                    InitDropDownUI(dropDown);
                }));
            }
        }

        private void InitDropDowns()
        {

            SetupDropDown(CityDP, cityView, cityLbl);
            SetupDropDownGesture(CityDP, cityView);

            var regionsArray = Regions?.Select(x => x.title)?.ToArray();
            CityDP.DataSource = regionsArray;

            CityDP.SelectionAction = (nint index, string name) =>
            {
                if (currentRegionIndex != index)
                {
                    currentRegionIndex = (int)index;

                    cityLbl.Text = Regions?[(int)index].title;
                }
            };
        }

        private void ShowDatePicker()
        {
            var datePicker = new UIDatePicker();

            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.Locale = new NSLocale("ka-GE");

            var toolBar = new UIToolbar();
            toolBar.SizeToFit();
            var doneButton = new UIBarButtonItem("არჩევა", UIBarButtonItemStyle.Plain, (sender, e) =>
            {

                date = datePicker.Date.NSDateToDateTime();
                InitDate(date);
                this.View.EndEditing(true);
            });

            var spaceButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null);

            var cancelButton = new UIBarButtonItem("დახურვა", UIBarButtonItemStyle.Plain, (s, e) => { this.View.EndEditing(true); });

            toolBar.SetItems(new UIBarButtonItem[] { cancelButton, spaceButton, doneButton }, false);

            dateTransparentTf.InputAccessoryView = toolBar;
            dateTransparentTf.InputView = datePicker;
        }

        private void InitDate(DateTime _date)
        {
            dayTf.Text = _date.Day.ToString();
            monthTf.Text = _date.ToString("MMMM", cultureInfo);
            yearTf.Text = _date.Year.ToString();
        }
    }
}
