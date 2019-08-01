// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using MPDC.iOS.Utils;
using System.Collections.Generic;
using IZrune.PCL.Abstraction.Models;
using System.Linq;
using IZrune.PCL.Helpers;
using System.Diagnostics;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.Abstraction.Services;

namespace Izrune.iOS
{
	public partial class StudentRegSecondViewController : UIViewController
	{
		public StudentRegSecondViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("StudentRegSecondStoryboardId");

        DropDown CityDpD = new DropDown();
        DropDown SchoolDpD = new DropDown();
        DropDown ClassDpD = new DropDown();

        public Action SendClicked { get; set; }
        public Action<ISchool> SchoolSelected { get; set; }
        public Action CitySelected { get; set; }

        public string[] schoolArrray { get; private set; }

        public List<IRegion> CityList;

        private int SelectedCityindex;
        private string[] cityArray;

        IRegion region;
        ISchool _school;
        int ClassId;
        private SelectSchoolViewController ScholVc;

        public bool IsAllSelected { get; set; }

        public IStudent Student;

        public Action StudentSelected { get; set; }
        public Action AddStudentClicked { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            ScholVc = Storyboard.InstantiateViewController(SelectSchoolViewController.StoryboardId) as SelectSchoolViewController;

            ScholVc.SchoolSelected = async(school) =>
            {
                _school = school;
                SchoolSelected?.Invoke(school);
                schoolLbl.Text = school.title;
                var userService = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();
                var Promo = await userService.GetPromoCodeAsync(school.id);
                if(Promo != null && !string.IsNullOrEmpty(Promo?.PrommoCode))
                {
                    var promoSchoolVc = Storyboard.InstantiateViewController(PromoSchoolViewController.StoryboardId) as PromoSchoolViewController;
                    promoSchoolVc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
                    this.NavigationController.PresentViewController(promoSchoolVc, true, null);
                }
            };

            villageTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);
            DropDownInit();

            if (selectSchoolView.GestureRecognizers == null || selectSchoolView.GestureRecognizers?.Length == 0)
            {
                selectSchoolView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                    this.NavigationController.PushViewController(ScholVc, true);
                }));
            }

            SendClicked = () =>
            {
            
                if(_school!= null && !string.IsNullOrEmpty(ClassDpD.SelectedItem))
                {
                    IsAllSelected = true;
                    SenData();
                }
                else
                    IsAllSelected = false;
            };

            StudentSelected = () =>
            {
                Student = new Student()
                {
                    RegionId = region.id,
                    Village = villageTextField.Text,
                    SchoolId = _school.id,
                    Class = ClassId
                };
            };

        }

        public bool IsFormFilled()
        {
            var res = (region != null && _school != null && ClassId > 0);
            return res;
        }

        private void DropDownInit()
        {
            SetupDropDownGesture(CityDpD, cityView);
            SetupDropDownGesture(ClassDpD, classView);

            SetupDropDown(CityDpD, cityView, cityLbl, Direction.Bottom);
            SetupDropDown(ClassDpD, classView, classLbl, Direction.Top);

            //SetupDropDown(CityDpD, cityView, cityLbl);
            //SetupDropDown(ClassDpD, classView, classLbl);

            InitDropDowns();
        }

        private void InitDropDowns()
        {
            //SetupDropDown(CityDpD, cityView, cityLbl, Direction.Bottom);
            //SetupDropDown(ClassDpD, classView, classLbl, Direction.Bottom);

            //SetupDropDownGesture(CityDpD, cityView);
            //SetupDropDownGesture(ClassDpD, classView);

            cityArray = CityList?.Select(x => x.title)?.ToArray();
            CityDpD.DataSource = cityArray;

            ClassDpD.DataSource = Enumerable.Range(1, 12).Select(x => x.ToString() + " კლასი")?.ToArray();

            CityDpD.SelectionAction = (nint index, string name) =>
            {
                //TODO
                CitySelected?.Invoke();
                SelectedCityindex = (int)index;
                IsAllSelected = false;
                region = CityList[SelectedCityindex];

                selectSchoolView.UserInteractionEnabled = true;
                ScholVc.SchoolList?.Clear();
                ScholVc.SchoolList = CityList?[SelectedCityindex].Schools?.OrderBy(x => x.title)?.ToList();

                cityLbl.Text = name;
                _school = null;
                schoolLbl.Text = "აირჩიეთ სკოლა";

            };

             
            SchoolDpD.SelectionAction = (nint index, string name) =>
            {

                classView.UserInteractionEnabled = true;
            };

            ClassDpD.SelectionAction = (nint index, string name) =>
            {
                ClassId = (int)(index + 1);
                classLbl.Text = name;
            };
        }

        private void SetupDropDown(DropDown dropDown, UIView viewForDpD, UILabel dropDownLbl, Direction direction)
        {
            dropDown.AnchorView = new WeakReference<UIView>(viewForDpD);
            dropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDpD.Bounds.Height);
            dropDown.Width = View.Frame.Width;
            dropDown.Direction = direction;
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

        private void SenData()
        {
            try
            {
                UserControl.Instance.RegistrationStudentPartTwo(
                region.id,
                _school.id,
                ClassId,
                 villageTextField.Text
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
