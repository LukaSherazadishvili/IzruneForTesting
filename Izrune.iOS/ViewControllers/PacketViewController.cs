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

namespace Izrune.iOS
{
	public partial class PacketViewController : BaseViewController
	{
		public PacketViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("PacketViewControllerStoryboardId");

        PromoCodeViewController PromoVc;
        SelectPacketViewController SelectPacketVc;
        public int SchoolId;
        public bool HideFooter { get; set; }

        IPromoCode PromoCode;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();


            await GetPromoDataAsync();

            SelectHeader();

            InitUI();

            InitGesture();

            SelectPacketVc = Storyboard.InstantiateViewController(SelectPacketViewController.StoryboardId) as SelectPacketViewController;
            SelectPacketVc.SchoolId = SchoolId;
            PromoVc = Storyboard.InstantiateViewController(PromoCodeViewController.StoryboardId) as PromoCodeViewController;
            PromoVc.PromoInfo = PromoCode;

            this.AddVcInView(viewForPeager, SelectPacketVc);

            //footerHeightConstraint.Constant = HideFooter ? 0 : 180;
            //nextBtn.Hidden = HideFooter;
        }

        private async Task GetPromoDataAsync()
        {
            ShowLoading();
            var service = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();
            PromoCode = (await service.GetPromoCodeAsync(SchoolId.ToString()));
            EndLoading();

        }
        private bool IsIndividualSelected = true;

        private void InitUI()
        {
            viewForIndividual.Layer.CornerRadius = 17.5f;
            viewForPromoCode.Layer.CornerRadius = 17.5f;
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
                    //TODO Change Page
                }));
            }

            if (viewForPromoCode.GestureRecognizers == null || viewForPromoCode.GestureRecognizers.Length == 0)
            {
                viewForPromoCode.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    HeaderGesture(false);
                    AddPromoVc();
                    //TODO Change Page
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
    }
}
