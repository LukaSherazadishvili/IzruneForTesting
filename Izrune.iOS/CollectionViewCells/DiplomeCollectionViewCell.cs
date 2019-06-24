using System;
using System.Globalization;
using Foundation;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class DiplomeCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("DiplomeCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("DiplomeCellIdentifier");

        public Action<IStudentsStatistic> CellClicked { get; set; }

        private IStudentsStatistic StudentsStatistic;

        DateTimeFormatInfo ge = new CultureInfo("ka-GE", false).DateTimeFormat;

        static DiplomeCollectionViewCell()
        {
            Nib = UINib.FromName("DiplomeCollectionViewCell", NSBundle.MainBundle);
        }

        protected DiplomeCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IStudentsStatistic studentsStatistic)
        {
            //TODO

            StudentsStatistic = studentsStatistic;

            dateLbl.Text = studentsStatistic.ExamDate.ToString(ge.ShortDatePattern);

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            mainView.Layer.BorderWidth = 2;
            mainView.Layer.BorderColor = UIColor.White.CGColor;

            mainView.Layer.CornerRadius = 20;

            viewForShadow.AddShadowToView(5, 20, 0.6f, UIColor.FromRGBA(203, 135, 214, 153));

            if(mainView.GestureRecognizers == null || mainView.GestureRecognizers?.Length == 0)
            {
                mainView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    CellClicked?.Invoke(StudentsStatistic);
                }));
            }
        }
    }
}
