// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using Izrune.iOS.Utils;
using UIKit;
using FPT.Framework.iOS.UI.DropDown;
using IZrune.PCL.Abstraction.Models;
using System.Linq;
using MpdcViewExtentions;
using IZrune.PCL.Helpers;
using System.Globalization;

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

        public Action<string, int> PromoCodeSelected { get; set; }

        CultureInfo cultureInfo = new CultureInfo("ka-GE");

        public int SelectedMont;

        public string PromoCode = "";
        public int month;

        public IPrice SelectedPrice;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //monthView.UserInteractionEnabled = false;
            
            confirmBtn.TouchUpInside += delegate {

                CheckCode(promoCodeTf.Text == PromoInfo.PrommoCode);

                var result = string.Equals(promoCodeTf.Text, PromoInfo.PrommoCode);
                //16295166
                //17756347
                if (result)
                {
                    monthView.UserInteractionEnabled = true;
                    //InitDropDown();
                    SelectPromoPack(0);
                    PromoCode = PromoInfo.PrommoCode;
                }
                else
                {
                    priceTitleLbl.Text = "";
                    MonthDropDown.DeselectRow(0);
                    monthLbl.Text = "თვე";
                }
            };

            //InitDropDown();
            //InitUI();
            //InitGestures();
        }

        private void InitUI()
        {
            //monthTf.MakeRoundedTextField(25, AppColors.TextFieldBackground);
            promoCodeErorLbl.Hidden = true;

            promoCodeTf.Layer.BorderWidth = 2;
            promoCodeTf.Layer.BorderColor = UIColor.FromRGB(243, 243, 243).CGColor;
            promoCodeTf.MakeRoundedTextField(25, UIColor.White, 0);

            //CheckPromo();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            InitDropDown();
            InitUI();
            InitGestures();
            CheckPromo();
        }

        public void UpdateDropdownDataSource()
        {
            if(MonthDropDown != null)
            {
                var array = PromoInfo?.Prices?.Select(x => x.Period)?.ToArray();

                MonthDropDown.DataSource = array;
            }
        }

        public void CheckPromo()
        {
            HidePromo(string.IsNullOrEmpty(PromoInfo?.PrommoCode) || string.IsNullOrWhiteSpace(PromoInfo?.PrommoCode));
        }
        private void CheckCode(bool isRight)
        {
            promoCodeTf.TextColor = UIColor.White;
            promoCodeTf.Layer.BorderColor = isRight ? AppColors.Succesful.CGColor : AppColors.ErrorTitle.CGColor;
            promoCodeTf.BackgroundColor = isRight ? AppColors.GreenBg : AppColors.RedBg;
            promoCodeErorLbl.Hidden = false;
            promoCodeErorLbl.Text = isRight ? "კოდი სწორია" : "კოდი არასწორია";

            promoCodeErorLbl.TextColor = isRight ? AppColors.Succesful : AppColors.ErrorTitle;
        }

        private void InitDropDown()
        {
            try
            {
                MonthDropDown.AnchorView = new WeakReference<UIView>(monthView);
                MonthDropDown.BottomOffset = new CoreGraphics.CGPoint(0, monthView.Bounds.Height);
                MonthDropDown.Width = this.View.Frame.Width;
                MonthDropDown.Direction = Direction.Bottom;

                var array = PromoInfo?.Prices?.Select(x => x.Period)?.ToArray();

                MonthDropDown.DataSource = array;

                MonthDropDown.SelectionAction = (nint index, string name) =>
                {
                    monthLbl.Text = name;
                    SelectPromoPack(index);
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SelectPromoPack(nint index)
        {
            monthLbl.Text = PromoInfo?.Prices?.ElementAt((int)index).Period;
            SelectedMont = (PromoInfo.Prices.ElementAt((int)index).MonthCount.Value);
            SelectedPrice = PromoInfo?.Prices?.ElementAt((int)index);

            PromoCodeSelected?.Invoke(PromoInfo.PrommoCode, SelectedMont);
            priceTitleLbl.Text = $"{PromoInfo?.Prices?.ElementAt((int)index).Period} - {PromoInfo?.Prices?.ElementAt((int)index)?.price} ლარი";
            MonthDropDown.SelectRow(index);
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
            //promoCodeErorLbl.Hidden = Hide;
            confirmBtn.Hidden = Hide;
        }
    }
}
