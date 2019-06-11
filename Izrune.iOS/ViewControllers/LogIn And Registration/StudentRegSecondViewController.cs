// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using Izrune.iOS.Utils;
using MpdcViewExtentions;

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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            villageTextField.MakeRoundedTextField(20.0f, AppColors.TextFieldBackground, 20);

            InitDropDowns();

        }

        private void InitDropDowns()
        {
            SetupDropDown(CityDpD, cityView, cityLbl);
            SetupDropDown(SchoolDpD, schoolView, schoolLbl);
            SetupDropDown(ClassDpD, classView, classLbl);

            SetupDropDownGesture(CityDpD, cityView);
            SetupDropDownGesture(SchoolDpD, schoolView);
            SetupDropDownGesture(ClassDpD, classView);
        }

        private void SetupDropDown(DropDown dropDown, UIView viewForDpD, UILabel dropDownLbl)
        {
            dropDown.AnchorView = new WeakReference<UIView>(viewForDpD);
            dropDown.BottomOffset = new CoreGraphics.CGPoint(0, viewForDpD.Bounds.Height);
            dropDown.Width = viewForDpD.Frame.Width;
            dropDown.Direction = Direction.Bottom;

            //var array = Students?.Select(x => x.Name + " " + x.LastName)?.ToArray();

            //CityDpD.DataSource = array;

            CityDpD.SelectionAction = (nint index, string name) =>
            {
                //TODO
                dropDownLbl.Text = name;
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

        private void SetupDropDownGesture(DropDown dropDown, UIView viewforDpD)
        {
            if (viewforDpD.GestureRecognizers == null || viewforDpD.GestureRecognizers?.Length == 0)
            {
                viewforDpD.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    InitDropDownUI(dropDown);

                    dropDown.Show();
                }));
            }
        }
    }
}
