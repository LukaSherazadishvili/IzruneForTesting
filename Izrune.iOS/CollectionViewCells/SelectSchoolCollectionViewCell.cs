using System;

using Foundation;
using IZrune.PCL.Abstraction.Models;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class SelectSchoolCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("SelectSchoolCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("SelectSchoolCellIdentifier");

        static SelectSchoolCollectionViewCell()
        {
            Nib = UINib.FromName("SelectSchoolCollectionViewCell", NSBundle.MainBundle);
        }

        protected SelectSchoolCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public Action<ISchool> SchoolSelected { get; set; }

        ISchool Schcool;
        public void InitData(ISchool school)
        {
            Schcool = school;
            schoolLbl.Text = school.title;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            if(mainView.GestureRecognizers == null || mainView.GestureRecognizers?.Length == 0)
            {
                mainView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                    SchoolSelected?.Invoke(Schcool);
                }));
            }
        }
    }
}
