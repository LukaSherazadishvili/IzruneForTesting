using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class NewsBigCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("NewsBigCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("NewsBigCellIdentifier");


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

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
