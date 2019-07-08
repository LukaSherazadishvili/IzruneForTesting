using System;
using System.Globalization;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class PaymentHistoryCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("PaymentHistoryCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("PaymentHistoryIdentifier");

        static PaymentHistoryCell()
        {
            Nib = UINib.FromName("PaymentHistoryCell", NSBundle.MainBundle);
        }

        protected PaymentHistoryCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IPaymentHistory paymentHistory)
        {
            userNameLbl.Text = paymentHistory?.StudentName;
            dateLbl.Text = paymentHistory?.Date?.ToString("MMMM", new CultureInfo("ka-GE"));
            priceLbl.Text = $"{paymentHistory?.Amount} + ₾";
        }
    }
}
