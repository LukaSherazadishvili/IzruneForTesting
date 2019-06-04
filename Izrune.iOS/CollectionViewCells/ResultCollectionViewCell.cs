using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class ResultCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ResultCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("ResultCellIdentifier");

        static ResultCollectionViewCell()
        {
            Nib = UINib.FromName("ResultCollectionViewCell", NSBundle.MainBundle);
        }

        protected ResultCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        private void InitData(IStudentsStatistic studentsStatistic)
        {
            dateLbl.Text = studentsStatistic.ExamDate.ToString();
            correctAnswersCountLbl.Text = studentsStatistic.CorrectAnswersCount.ToString();
            inCorrectAnswersCountLbl.Text = studentsStatistic.IncorrectAnswersCount.ToString();
            skipedQuestionsCountLbl.Text = studentsStatistic.SkippedQuestionsCount.ToString();
            timeLbl.Text = $"{studentsStatistic.TestTimeInSecconds/60}წთ {studentsStatistic.TestTimeInSecconds%60}წმ";
            pointsLbl.Text = studentsStatistic.Point.ToString();

        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            mainView.AddShadowToView(3, 15, 0.8f, UIColor.FromRGBA(0,0,0, 0.15f));
        }
    }
}
