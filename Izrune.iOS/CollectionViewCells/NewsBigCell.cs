using System;
using System.Globalization;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;
using MpdcViewExtentions;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class NewsBigCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("NewsBigCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("NewsBigCellIdentifier");

        CultureInfo cultureInfo = new CultureInfo("ka-GE");
        static NewsBigCell()
        {
            Nib = UINib.FromName("NewsBigCell", NSBundle.MainBundle);
        }

        protected NewsBigCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }


        private INews News;
        public Action<INews> NewsClicked { get; set; }

        public void InitData(INews news)
        {
            News = news;
            newsImageView.InitImageFromWeb(news?.ImageUrl, false, false);

            titleLbl.Text = news?.Title;
            dateLbl.Text = news?.date.ToString("dd MMMM yyyy", cultureInfo);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            newsImageView.Layer.CornerRadius = 10;
        }
    }
}
