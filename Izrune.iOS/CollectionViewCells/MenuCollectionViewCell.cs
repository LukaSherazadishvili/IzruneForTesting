using System;
using System.Diagnostics;
using System.Linq;
using Foundation;
using Izrune.iOS.Models;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS.CollectionViewCells
{
    public partial class MenuCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("MenuCollectionViewCell");
        public static readonly UINib Nib;

        public static readonly NSString Identifier = new NSString("MenuCellIdentifier");

        MenuItem _MenuItem;

        public Action<MenuItem> MenuClicked { get; set; }

        static MenuCollectionViewCell()
        {
            Nib = UINib.FromName("MenuCollectionViewCell", NSBundle.MainBundle);
        }

        protected MenuCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void InitData(MenuItem menuItem)
        {
            _MenuItem = menuItem;

            CheckSelected(menuItem);

            titleLbl.Text = menuItem.Title;
            menuImage.Image = menuItem.Image;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            if(transparentView.GestureRecognizers == null || transparentView.GestureRecognizers?.Count() == 0)
            {
                transparentView.AddGestureRecognizer(new UITapGestureRecognizer(() => MenuClicked?.Invoke(_MenuItem)));
            }
        }

        private void CheckSelected(MenuItem menuItem)
        {
            var color = menuItem.IsSelected ? AppColors.TitleColor : AppColors.UnselectedColor;
            menuItem.Image = menuItem.Image.GetImageWithColor(color);
            titleLbl.TextColor = color;
        }
    }
}
