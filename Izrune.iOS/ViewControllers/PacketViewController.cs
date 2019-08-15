// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;
using IZrune.PCL.Implementation.Models;
using FPT.Framework.iOS.UI.DropDown;
using System.Collections.Generic;

namespace Izrune.iOS
{
	public partial class PacketViewController : BaseViewController
	{
		public PacketViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("PacketViewControllerStoryboardId");

        #region Fields

        PromoCodeViewController PromoVc;
        SelectPacketViewController SelectPacketVc;

        public int SchoolId;
        public bool HideFooter { get; set; }

        float HederHeight = 190;
        float FooterHeight = 140;

        DropDown StudentDp = new DropDown();

        public IPromoCode PromoCode;

        public Action<IPrice> PriceSelected { get; set; }

        public Action SendClicked { get; set; }

        public Action NextClicked { get; set; }

        public Action RefreshData { get; set; }

        bool IsPromoSelected;

        IStudent SelectedStudent;

        List<IStudent> Students;

        public bool IsFromMenu = true;
        IPrice SelectedPrice;

        #endregion

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            SelectedStudent = UserControl.Instance.CurrentStudent;

            if (!IsFromMenu)
                await GetPromoDataAsync(SchoolId);

            SelectHeader();

            InitUI();

            InitGesture();

            SelectPacketVc = Storyboard.InstantiateViewController(SelectPacketViewController.StoryboardId) as SelectPacketViewController;

            SelectPacketVc.DataLoaded = () =>
            {
                SelectPacketVc.DisableScroll = true;
                UpdateViewSize(SelectPacketVc.ContentHeight);
                View.LayoutIfNeeded();
            };

            PromoVc = Storyboard.InstantiateViewController(PromoCodeViewController.StoryboardId) as PromoCodeViewController;

            SelectPacketVc.IsFromMenu = IsFromMenu;

            this.AddVcInView(viewForPeager, SelectPacketVc);

            PromoVc.PromoInfo = PromoCode;

            if (IsFromMenu)
            {
                Students = (await UserControl.Instance.GetCurrentUserStudents())?.ToList();
                SelectedStudent = Students?[0];
                SelectPacketVc.SelectedStudent = SelectedStudent;

                await GetPromoDataAsync(SelectedStudent.SchoolId);

                PromoVc.PromoInfo = PromoCode;
                //PromoVc.CheckPromo();
                InitDropDowns();

                nextBtn.TouchUpInside += async delegate {
                    //TODO Call packet methods

                    try
                    {
                        if (SelectPacketVc.SelectedPrice == null && PromoVc.SelectedMont == 0)
                            ShowAlert();

                        else
                        {
                            if (PromoVc.IsPacketSelected)
                            {

                                ShowLoading();
                                SelectedStudent.Promocode = PromoVc.PromoCode;
                                await UserControl.Instance.ReNewPack(SelectedStudent);
                                EndLoading();
                            }

                            else
                            {
                                ShowLoading();
                                var _price = (PromoVc.IsPacketSelected ? PromoVc.SelectedPrice : SelectPacketVc.SelectedPrice);
                                SelectedPrice = _price;
                                SelectedPrice.MonthCount = _price.MonthCount;
                                SelectedStudent.Amount = SelectedPrice.price.Value;
                                SelectedStudent.PackageMonthCount = SelectedPrice.MonthCount.Value;


                                await UserControl.Instance.ReNewPack(SelectedStudent);
                                EndLoading();
                            }

                            var price = (PromoVc.IsPacketSelected ? PromoVc.SelectedPrice : SelectPacketVc.SelectedPrice);

                            SelectedPrice = price;
                            var payInfo = UserControl.Instance.GetPaymentInformation();

                            var payVc = Storyboard.InstantiateViewController(PaymentMethodViewController.StoryboardId) as PaymentMethodViewController;
                            payVc.SelectedPrice = SelectedPrice;
                            payVc.allPrices = (int)SelectedPrice?.price.Value;
                            payVc.PayInfo = payInfo;
                            payVc.HideTitle = true;

                            this.NavigationController.PushViewController(payVc, true);
                        }
                    }

                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }; ;

                var scrollView = SelectPacketVc.View.OfType<UIScrollView>().FirstOrDefault();
                SelectPacketVc.View.LayoutIfNeeded();
                scrollView.LayoutIfNeeded();

                SetContentHeight(scrollView.ContentSize.Height);
            }

            else
            {
                SelectPacketVc.SchoolId = SchoolId;
                //SelectPacketVc.PriceSelected = (price) =>
                //{

                //};

                PromoVc.PromoInfo = PromoCode;

                this.AddVcInView(viewForPeager, SelectPacketVc);

                SendClicked = () => SelectPacketVc.SendClicked?.Invoke();
                RefreshData = () => SelectPacketVc.RefrehData?.Invoke();

                nextBtn.TouchUpInside += delegate {

                    if (SelectPacketVc.SelectedPrice == null && PromoVc.SelectedMont == 0)
                    {
                        ShowAlert();
                    }

                    else
                    {
                        var price = (PromoVc.IsPacketSelected ? new Price() { price = PromoVc.SelectedPrice?.price, MonthCount = PromoVc.SelectedMont } : SelectPacketVc.SelectedPrice);
                        PriceSelected?.Invoke(price);
                        if (PromoVc.IsPacketSelected)
                        {
                            //PriceSelected?.Invoke(price);
                            UserControl.Instance.SetPromoPack(PromoVc.SelectedMont, (int)PromoVc.SelectedPrice?.price.Value, PromoVc.PromoCode);

                        }

                        else
                        {
                            UserControl.Instance.SetPromoPack(SelectPacketVc.SelectedPrice.MonthCount.Value, SelectPacketVc.SelectedPrice.price.Value);
                        }

                        //var price = (IsPromoSelected ? new Price() { price = PromoVc.SelectedMont, MonthCount = PromoVc.SelectedMont } : SelectPacketVc.SelectedPrice);

                        PriceSelected?.Invoke(price);
                        NextClicked?.Invoke();
                        this.NavigationController.PopViewController(true);
                    }
                };

                PromoVc.PromoCodeSelected = (promoCode, month) =>
                {
                    if (month > 0)
                    {
                        IsPromoSelected = true;
                    }
                    else
                        IsPromoSelected = false;

                };

                SelectPacketVc.DataLoaded = () =>
                {
                    UpdateViewSize(SelectPacketVc.ContentHeight);
                };
            }

        }

        private async Task GetPromoDataAsync(int schoolId)
        {
            //ShowLoading();
            var service = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();
            PromoCode = (await service.GetPromoCodeAsync(schoolId));
            //EndLoading();

        }

        private bool IsIndividualSelected = true;

        private void InitUI()
        {

            selectStudentDP.Hidden = !IsFromMenu;

            viewForIndividual.Layer.CornerRadius = 17.5f;
            viewForPromoCode.Layer.CornerRadius = 17.5f;

            individualLbl.TextColor = UIColor.White;
            promoLbl.TextColor = UIColor.FromRGB(184, 184, 184);

            individualLbl.Text = "ინდივიდუალური";
            promoLbl.Text = "პრომო კოდი";

            //nextBtn.AddShadowToView(5, 25, 0.8f, AppColors.TitleColor);
        }

        private void InitGesture()
        {
            if(viewForIndividual.GestureRecognizers == null || viewForIndividual.GestureRecognizers.Length == 0)
            {
                viewForIndividual.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    HeaderGesture(true);
                    AddPacketVc();
                    IsPromoSelected = false;
                }));
            }

            if (viewForPromoCode.GestureRecognizers == null || viewForPromoCode.GestureRecognizers.Length == 0)
            {
                viewForPromoCode.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    HeaderGesture(false);
                    AddPromoVc();
                    IsPromoSelected = true;
                }));
            }
        }

        private void SelectHeader()
        {
            viewForIndividual.BackgroundColor = IsIndividualSelected ? AppColors.TitleColor : UIColor.Clear;
            individualLbl.TextColor = IsIndividualSelected ? UIColor.White : AppColors.UnselectedColor;

            viewForPromoCode.BackgroundColor = IsIndividualSelected ? UIColor.Clear : AppColors.TitleColor;
            promoLbl.TextColor = IsIndividualSelected ? AppColors.UnselectedColor : UIColor.White;
        }

        private void HeaderGesture(bool individualSelected)
        {
            IsIndividualSelected = individualSelected;
            SelectHeader();
        }

        private void AddPromoVc()
        {
            SelectPacketVc.WillMoveToParentViewController(this);
            SelectPacketVc.View.RemoveFromSuperview();
            SelectPacketVc.RemoveFromParentViewController();

            this.AddVcInView(viewForPeager, PromoVc);
        }

        private void AddPacketVc()
        {
            PromoVc.WillMoveToParentViewController(this);
            PromoVc.View.RemoveFromSuperview();
            PromoVc.RemoveFromParentViewController();

            this.AddVcInView(viewForPeager, SelectPacketVc);
        }

        private void SetContentHeight(nfloat scrollviewContentHeight)
        {

            UIEdgeInsets safeAreaSize = new UIEdgeInsets();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                safeAreaSize = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            }

            var currentContentHeight = this.View.Frame.Height - (HederHeight + FooterHeight + safeAreaSize.Top + safeAreaSize.Bottom);
            var diffrenceWithContents = currentContentHeight - scrollviewContentHeight;

            if (diffrenceWithContents >= 0)
                contentHeightConstraint.Constant = currentContentHeight;
                
            else
                contentHeightConstraint.Constant = scrollviewContentHeight;
                

            View.LayoutIfNeeded();

            viewForPeager.Frame = new CoreGraphics.CGRect(viewForPeager.Frame.X, viewForPeager.Frame.Y,
                viewForPeager.Frame.Width, contentHeightConstraint.Constant);

            View.LayoutIfNeeded();
        }

        private void ShowAlert()
        {
            var alertVc = UIAlertController.Create("ყურადღება!", "აუცილებელია პაკეტის არჩევა", UIAlertControllerStyle.Alert);
            alertVc.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alertVc, true, null);
        }

        private void SetupDropDown()
        {
            StudentDp.AnchorView = new WeakReference<UIView>(selectStudentDP);
            StudentDp.BottomOffset = new CoreGraphics.CGPoint(0, selectStudentDP.Bounds.Height);
            StudentDp.Width = View.Frame.Width;
            StudentDp.Direction = Direction.Bottom;
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

            SetupDropDown();

            SetupDropDownGesture(StudentDp, selectStudentDP);

            SelectedStudent = UserControl.Instance.CurrentStudent;

            selectedStudentLbl.Text = SelectedStudent.Name + " " + SelectedStudent.LastName;

            var studentsArray = Students?.Select(x => x.Name + " " + x.LastName)?.ToArray();
            StudentDp.DataSource = studentsArray;

            StudentDp.SelectionAction = async (nint index, string name) =>
            {
                //TODO
                SelectedStudent = Students?[(int)index];

                selectedStudentLbl.Text = SelectedStudent.Name + " " + SelectedStudent.LastName;

                await GetPromoDataAsync(SelectedStudent.SchoolId);


                PromoVc.PromoInfo = PromoCode;
                PromoVc.UpdateDropdownDataSource();
                if(IsPromoSelected)
                    PromoVc.CheckPromo();
            };
        }

        private void UpdateViewSize(nfloat packetHeight)
        {
            UIEdgeInsets safeAreaSize = new UIEdgeInsets();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                safeAreaSize = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            }

            var visibleHeight = this.View.Frame.Height - (HederHeight + FooterHeight + safeAreaSize.Top + safeAreaSize.Bottom);

            nfloat delta = visibleHeight - packetHeight;

            if (delta > 0)
                contentHeightConstraint.Constant = visibleHeight;
            else
                contentHeightConstraint.Constant = packetHeight;
                
        }
    }
}
