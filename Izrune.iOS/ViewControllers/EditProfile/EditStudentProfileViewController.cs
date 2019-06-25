// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using IZrune.PCL.Abstraction.Models;
using System.Collections.Generic;
using System.Linq;
using IZrune.PCL.Helpers;
using System.Threading.Tasks;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using MPDC.iOS.Utils;
using System.Globalization;

namespace Izrune.iOS
{
    public partial class EditStudentProfileViewController : BaseViewController
    {
        public EditStudentProfileViewController (IntPtr handle) : base (handle)
        {
        }

        public static readonly NSString StoryboardId = new NSString("EditStudentStoryboardId");


        DropDown CurentStudentDP = new DropDown();
        DropDown CityDP = new DropDown();

        IStudent CurrentStudent;

        CultureInfo cultureInfo = new CultureInfo("ka-GE");
            
        private List<IRegion> Regions;
        private List<ISchool> Schools;

        private List<IStudent> Students;

        private int currentStudentIndex;
        private int currentRegionIndex;

        private int RegionId;
        private int SchoolId;

        private SelectSchoolViewController SchoolVc;
        private DateTime date;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SchoolVc = Storyboard.InstantiateViewController(SelectSchoolViewController.StoryboardId) as SelectSchoolViewController;

            await LoadDataAsync();

            InitUI();

            InitGestures();

            SetupDropDowns();

        }

        private async Task LoadDataAsync()
        {
            contentView.Hidden = true;
            ShowLoading();
            Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();

            CurrentStudent = Students?.First();

            var registerService = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();
            Regions = (await registerService.GetRegionsAsync())?.ToList();

            GetSchools();
            InitGestures();

            InitForm(CurrentStudent);
            EndLoading();
            contentView.Hidden = false;
        }

        private void InitGestures()
        {
            saveBtn.TouchUpInside += async delegate
           {
               UpdateStudenProfile(nameLbl.Text, lastNameLbl.Text, new DateTime(), phoneTf.Text, emailTf.Text, RegionId, villageTf.Text);

               await UserControl.Instance.EditStudentprofile(emailTf.Text, phoneTf.Text, RegionId, villageTf.Text, SchoolId);
           };

            backBtn.TouchUpInside += delegate {
                this.NavigationController.PopViewController(true);
            };

            if(schoolLbl.GestureRecognizers == null || schoolLbl.GestureRecognizers?.Length == 0)
            {
                schoolLbl.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    SchoolVc.SchoolList = Schools;

                    SchoolVc.SchoolSelected = (school) => { schoolLbl.Text = school.title; CurrentStudent.SchoolId = school.id; };
                    this.NavigationController.PushViewController(SchoolVc, true);

                }));
            }

            dateTransparentTf.EditingDidBegin += (sender, e) =>
            {
                ShowDatePicker();
            };

        }

        private void GetSchools()
        {
            var region = (Regions?.FirstOrDefault(x => x.id == CurrentStudent?.RegionId));
            var scool = region?.Schools?.ToList();
            Schools = scool;
        }

        private void InitUI()
        {
            cityView.Layer.CornerRadius = 20;
            curentStudentView.Layer.CornerRadius = 20;

            backBtn.Layer.CornerRadius = 25;

            dateTransparentTf.MakeRoundedTextField(20.0f, UIColor.Clear, 20);
            villageTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);

            pnTf.MakeRoundedTextField(20.0f, UIColor.Clear, 20);
            phoneTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            emailTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);

            dayTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            monthTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
            yearTf.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 0);
        }
         
        private void InitForm(IStudent student)
        {
            currentStudentLbl.Text = student.Name;
            nameLbl.Text = student?.Name;
            lastNameLbl.Text = student?.LastName;

            InitDate(student.Bdate);

            pnTf.Text = student?.PersonalNumber;
            phoneTf.Text = student?.Phone;
            emailTf.Text = student?.Email;
            cityLbl.Text = Regions?.FirstOrDefault(x => x.id == student?.RegionId).title;
            villageTf.Text = student?.Village;
            //schoolLbl.Text = Schools?.FirstOrDefault(x => x.id == student?.SchoolId).title;
            classLbl.Text = student?.Class.ToString();

            Schools = Regions?.FirstOrDefault(x => x.id == student.RegionId)?.Schools?.ToList();
        }

        private void UpdateStudenProfile(string firstName, string lastName, DateTime birthDate, string phoneNumber, string email, int regionId, string village)
        {
            CurrentStudent.Name = firstName;
            CurrentStudent.LastName = lastName;
            CurrentStudent.Bdate = birthDate;
            CurrentStudent.Phone = phoneNumber;
            CurrentStudent.Email = email;
            CurrentStudent.RegionId = regionId;
            CurrentStudent.Village = village;

        }

        private void SetupDropDowns()
        {
            SetupDropDownGesture(CurentStudentDP, curentStudentView);
            SetupDropDownGesture(CityDP, cityView);

            InitDropDowns();
        }

        private void InitDropDowns()
        {
            SetupDropDown(CurentStudentDP, curentStudentView, currentStudentLbl);
            SetupDropDown(CityDP, cityView, cityLbl);

            var studentsArray = Students?.Select(x => x.Name)?.ToArray();
            CurentStudentDP.DataSource = studentsArray;

            var regionsArray = Regions?.Select(x => x.title)?.ToArray();
            CityDP.DataSource = regionsArray;

            CurentStudentDP.SelectionAction = async (nint index, string name) =>
            {
                if (currentStudentIndex != index)
                {
                    RegionId = Regions[(int)index].id;

                    currentStudentIndex = (int)index;

                    CurrentStudent = Students?[(int)index];

                    InitForm(CurrentStudent);

                    Schools = Regions?[(int)index].Schools?.ToList();
                }
            };

            CityDP.SelectionAction = (nint index, string name) =>
            {
                if(currentRegionIndex != index)
                {
                    currentRegionIndex = (int)index;

                    cityLbl.Text = Regions?[(int)index].title;
                }
            };
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
