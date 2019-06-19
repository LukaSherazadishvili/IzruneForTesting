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

namespace Izrune.iOS
{
    public partial class EditStudentProfileViewController : UIViewController
    {
        public EditStudentProfileViewController (IntPtr handle) : base (handle)
        {
        }

        public static readonly NSString StoryboardId = new NSString("EditStudentStoryboardId");


        DropDown CurentStudentDP = new DropDown();
        DropDown CityDP = new DropDown();

        IStudent CurrentStudent;

        private List<IRegion> Regions;
        private List<ISchool> Schools;

        private List<IStudent> Students;

        private int currentStudentIndex;
        private int currentRegionIndex;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            await LoadDataAsync();

            CurrentStudent = Students?.First();

            InitUI();

            InitForm(CurrentStudent);

            SetupDropDowns();
        }

        private async Task LoadDataAsync()
        {
            Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();

            var registerService = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();
            Regions = (await registerService.GetRegionsAsync())?.ToList();
        }

        private void InitUI()
        {
            cityView.Layer.CornerRadius = 20;
            curentStudentView.Layer.CornerRadius = 20;

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
            nameLbl.Text = student?.Name;
            lastNameLbl.Text = student?.LastName;

            dayTf.Text = student?.Bdate.Day.ToString();
            monthTf.Text = student?.Bdate.Month.ToString();
            yearTf.Text = student?.Bdate.Year.ToString();

            pnTf.Text = student?.PersonalNumber;
            phoneTf.Text = student?.Phone;
            emailTf.Text = student?.Email;
            cityLbl.Text = Regions?.FirstOrDefault(x => x.id == student?.RegionId).title;
            villageTf.Text = student?.Village;
            schoolLbl.Text = Schools?.FirstOrDefault(x => x.id == student?.SchoolId).title;
            classLbl.Text = student?.Class.ToString();
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

            CurentStudentDP.SelectionAction = (nint index, string name) =>
            {
                if (currentStudentIndex != index)
                {
                    currentStudentIndex = (int)index;

                    CurrentStudent = Students?[(int)index];

                    InitForm(CurrentStudent);
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
    }
}
