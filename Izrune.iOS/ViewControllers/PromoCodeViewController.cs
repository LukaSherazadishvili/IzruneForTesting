// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using Izrune.iOS.Utils;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using IZrune.PCL.Abstraction.Models;
using System.Linq;
using MpdcViewExtentions;

namespace Izrune.iOS
{
	public partial class PromoCodeViewController : UIViewController
	{
		public PromoCodeViewController (IntPtr handle) : base (handle)
		{
		}

        public IPromoCode PromoInfo { get; set; }

        public static readonly NSString StoryboardId = new NSString("PromoCodeStoryboardId");

        DropDown MonthDropDown = new DropDown();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            confirmBtn.TouchUpInside += delegate {
                //TODO
                //CheckCode()

                CheckCode(promoCodeTf.Text == PromoInfo.PrommoCode);
            };

            InitUI();
            InitGestures();
            InitDropDown();

        }

        private void InitUI()
        {
            //monthTf.MakeRoundedTextField(25, AppColors.TextFieldBackground);
            promoCodeErorLbl.Hidden = true;
            promoCodeTf.Layer.BorderWidth = 2;
            promoCodeTf.Layer.BorderColor = UIColor.FromRGB(243, 243, 243).CGColor;
            promoCodeTf.MakeRoundedTextField(25, UIColor.White, 0);

            HidePromo(string.IsNullOrEmpty(PromoInfo.PrommoCode) || string.IsNullOrWhiteSpace(PromoInfo.PrommoCode));
        }
        private void CheckCode(bool isRight)
        {
            promoCodeTf.TextColor = UIColor.White;
            promoCodeTf.Layer.BorderColor = isRight ? AppColors.Succesful.CGColor : AppColors.ErrorTitle.CGColor;
            promoCodeTf.BackgroundColor = isRight ? AppColors.GreenBg : AppColors.RedBg;
            promoCodeErorLbl.Hidden = false;
            promoCodeErorLbl.TextColor = isRight ? AppColors.Succesful : AppColors.ErrorTitle;
        }

        private void InitDropDown()
        {
            MonthDropDown.AnchorView = new WeakReference<UIView>(monthView);
            MonthDropDown.BottomOffset = new CoreGraphics.CGPoint(0, monthView.Bounds.Height);
            MonthDropDown.Width = this.View.Frame.Width;
            MonthDropDown.Direction = Direction.Bottom;

            var array = PromoInfo.Prices.Select(x => x.months.ToString() + "თვე")?.ToArray();

            MonthDropDown.DataSource = array;

            MonthDropDown.SelectionAction = (nint index, string name) =>
            {
                monthLbl.Text = name;

            };
        }

        private void InitGestures()
        {
            if (monthView.GestureRecognizers == null || monthView.GestureRecognizers?.Length == 0)
            {
                monthView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                    MonthDropDown.Show();
                    InitDropDownUI(MonthDropDown);
                }));
            }
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

        private void HidePromo(bool Hide)
        {
            specialPacketLbl.Hidden = !Hide;

            promoStackView.Hidden = Hide;
            promoCodeErorLbl.Hidden = Hide;
            confirmBtn.Hidden = Hide;
        }
    }
}
