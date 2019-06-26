using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;
using MpdcViewExtentions;
using System.Globalization;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class NewsMinCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("NewsMinCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("NewsMinCellIdentifier");

        CultureInfo cultureInfo = new CultureInfo("ka-GE");

        static NewsMinCell()
        {
            Nib = UINib.FromName("NewsMinCell", NSBundle.MainBundle);
        }

        protected NewsMinCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        private INews News;
        public Action<INews> NewsClicked { get; set; }

        public void InitData(INews news)
        {
            News = news;
            newsImageView.InitImageFromWeb(news?.ImageUrl, false, false);

            descriptionLbl.Text = news?.Title;
            dateLbl.Text = news?.date.ToString("dd MMMM yyyy", cultureInfo);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            newsImageView.Layer.CornerRadius = 10;

            if (newsTransparentView.GestureRecognizers == null || newsTransparentView.GestureRecognizers?.Length == 0)
            {
                newsTransparentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    NewsClicked?.Invoke(News);
                }));
            }
        }
    }
}
