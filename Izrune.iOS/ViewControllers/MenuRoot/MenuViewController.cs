// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using Izrune.iOS.Models;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Enum;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
	public partial class MenuViewController : UIViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public MenuViewController (IntPtr handle) : base (handle)
		{
		}

        public bool IsLogedIn;

        public IParent CurrentUser;

        public static readonly NSString StoryboardId = new NSString("MenuViewControllerStoryboardId");

        List<MenuItem> LogOutList = new List<MenuItem>()
        {
            new MenuItem()
            {
                Title = "შესვლა",
                Type = MenuType.LogIn,
                Image = UIImage.FromBundle("1 – 5.png"),
                IsSelected = true
            },
            new MenuItem()
            {
                Title = "სიახლეები",
                Type = MenuType.News,
                Image = UIImage.FromBundle("1 – 7.png")
            },
            new MenuItem()
            {
                Title = "გაიგეთ მეტი",
                Type = MenuType.MoreInfo,
                Image = UIImage.FromBundle("1 – 8.png")
            },
            new MenuItem()
            {
                Title = "კონტაქტი",
                Type = MenuType.Contact,
                Image = UIImage.FromBundle("1 – 9.png")
            },
        };

        List<MenuItem> LogedInList = new List<MenuItem>()
        {
            new MenuItem()
            {
                Title = "მთავარი",
                Type = MenuType.Main,
                Image = UIImage.FromBundle("1 – 13.png"),
                IsSelected = true
            },
            new MenuItem()
            {
                Title = "სტატისტიკა/ისტორია",
                Type = MenuType.Statistic,
                Image = UIImage.FromBundle("1 – 15.png")
            },
            new MenuItem()
            {
                Title = "პაკეტის განახლება",
                Type = MenuType.UpdatePacket,
                Image = UIImage.FromBundle("1 – 14.png")
            },
            new MenuItem()
            {
                Title = "პროფილის რედაქტირება",
                Type = MenuType.EditProfile,
                Image = UIImage.FromBundle("1 – 16.png")
            },
            new MenuItem()
            {
                Title = "სიახლეები",
                Type = MenuType.News,
                Image = UIImage.FromBundle("1 – 7.png")
            },
            new MenuItem()
            {
                Title = "კონტაქტი",
                Type = MenuType.Contact,
                Image = UIImage.FromBundle("1 – 9.png")
            },
            new MenuItem()
            {
                Title = "გასვლა",
                Type = MenuType.LogOut,
                Image = UIImage.FromBundle("1 – 17.png")
            }
        };

        public Action<MenuItem> MainMenuClicked { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitCollectionViewSettings();

            ShowUserInfo(IsLogedIn);
        }

        public void InitUser()
        {
            if(CurrentUser != null)
            {
                userNameLbl.Text = CurrentUser?.Name + " " + CurrentUser?.LastName;
                profileNumberLbl.Text = CurrentUser?.id.ToString();
            }
        }

        public void ShowUserInfo(bool Show)
        {
            userNameLbl.Hidden = !Show;
            profileNumberStackView.Hidden = !Show;
        }

        private void InitCollectionViewSettings()
        {
            menuCollectionView.Delegate = this;
            menuCollectionView.DataSource = this;

            menuCollectionView.RegisterNibForCell(MenuCollectionViewCell.Nib, MenuCollectionViewCell.Identifier);

            menuCollectionView.ReloadData();
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (IsLogedIn)
                return LogedInList?.Count ?? 0;
            return LogOutList?.Count ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(MenuCollectionViewCell.Identifier, indexPath) as MenuCollectionViewCell;

            var currData = IsLogedIn ? LogedInList?[indexPath.Row] : LogOutList?[indexPath.Row];

            cell.InitData(currData);

            cell.MenuClicked = (menu) =>
            {
                SelectItem((IsLogedIn ? LogedInList : LogOutList), menu);
                MainMenuClicked?.Invoke(menu);
                menuCollectionView.ReloadData();
                //TODO Navigate To Page
            };

            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CoreGraphics.CGSize(collectionView.Frame.Width, 50);
        }

        private void SelectItem(List<MenuItem> menus, MenuItem menuItem)
        {
            foreach (var item in menus)
            {
                if(item.Type != MenuType.LogOut)
                {
                    if (item.Type == menuItem.Type)
                        item.IsSelected = true;
                    else
                        item.IsSelected = false;
                }
                else
                {
                    var main = LogedInList?[0];
                    main.IsSelected = true;
                }

            }
        }

        public void ReloadMenu()
        {
            menuCollectionView.ReloadData();
        }
    }
}
