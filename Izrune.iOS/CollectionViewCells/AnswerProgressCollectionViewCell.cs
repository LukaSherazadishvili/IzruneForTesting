using System;

using Foundation;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public enum AnswerStatus
    {
        Checked,
        Current,
        Unknown
    }

    public partial class AnswerProgressCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("AnswerProgressCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("AnswerProgressCellIdentifier");
        public AnswerStatus AnswerStatus = AnswerStatus.Unknown;

        static AnswerProgressCollectionViewCell()
        {
            Nib = UINib.FromName("AnswerProgressCollectionViewCell", NSBundle.MainBundle);
        }

        protected AnswerProgressCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData()
        {

        }
    }
}
