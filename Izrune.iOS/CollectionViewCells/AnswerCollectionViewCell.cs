using System;
using System.Linq;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class AnswerCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("AnswerCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("AnswerCellIdentifier");

        public Action<IAnswer> AnswerClicked { get; set; }


        public UICollectionView Parent { get; set; }
        IAnswer Answer;

        public bool IsResult;

        static AnswerCollectionViewCell()
        {
            Nib = UINib.FromName("AnswerCollectionViewCell", NSBundle.MainBundle);
        }

        protected AnswerCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(IAnswer answer, string number, bool checkAnswer = false)
        {
            Answer = answer;
            answerLbl.Text = GetStringFromHtml(answer.title);
            numberLbl.Text = number;
            InitAnswer(AppColors.Tint);

            answerLbl.TextColor = AppColors.UnselectedColor;

            if (checkAnswer)
                CheckAnswer(answer.IsRight);
        }

        public void InitDataForResult(IAnswer answer, string number, bool iscorrect)
        {

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            answerView.Layer.CornerRadius = 20;
            answerView.Layer.BorderWidth = 2;

            numberView.Layer.CornerRadius = 20;

            if(answerContentView.GestureRecognizers == null || answerContentView.GestureRecognizers?.Count() == 0)
            {
                answerContentView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    if(!IsResult)
                        CheckAnswer(Answer.IsRight);

                    AnswerClicked?.Invoke(Answer);
                }));
            }
        }

        private void InitAnswer(UIColor color)
        {
            answerView.Layer.BorderColor = color.CGColor;
            numberView.BackgroundColor = color;
            answerLbl.TextColor = color;

        }

        public void CheckAnswer(bool IsRight)
        {
            if (!Parent.UserInteractionEnabled)
                return;

            InitAnswer(IsRight ? AppColors.Succesful : AppColors.ErrorTitle);
        }

        private string GetStringFromHtml(string htmlString)
        {
            var attr = new NSAttributedStringDocumentAttributes();

            var nsError = new NSError();

            attr.DocumentType = NSDocumentType.HTML;

            var myHtmlData = NSData.FromString(htmlString, NSStringEncoding.Unicode);

            var data = new NSAttributedString(myHtmlData, attr, ref nsError); //new NSAttributedString($"<span>{htmlString}</span>", attr, ref nsError);

            return data.Value;
        }
    }
}
