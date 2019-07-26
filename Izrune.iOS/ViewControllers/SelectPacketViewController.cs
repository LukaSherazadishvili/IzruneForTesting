// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MPDCiOSPages.ViewControllers;
using UIKit;

namespace Izrune.iOS
{
	public partial class SelectPacketViewController : BaseViewController, IUICollectionViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
	{
		public SelectPacketViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("SelectPacketStoryboardId");

        private int SelectedPriceIndex;

        private List<IPrice> PriceList;
        public int SchoolId;
        public Action<IPrice> PriceSelected { get; set; }
        public Action SendClicked { get; set; }

        public Action DataLoaded { get; set; }

        public Action RefrehData { get; set; }

        public IPrice SelectedPrice;

        public IStudent SelectedStudent;

        public bool IsFromMenu;

        public nfloat ContentHeight { get; set; }

        public bool DisableScroll = false;
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CollectionViewSettings();

            await LoadDataAsync();

            SendClicked = () => SendData();

            RefrehData = async () => 
            {
                PriceList?.Clear();
                await LoadDataAsync(); 

                };
            View.LayoutIfNeeded();

            SelectedPrice = PriceList?[0];

            //PriceSelected?.Invoke(SelectedPrice);
        }

        private void SendData()
        {
            //TODO
        }

        private async Task LoadDataAsync()
        {
            ShowLoading();
            //TODO ProceList = ?

            try
            {
                if(IsFromMenu)
                {
                    var students = await UserControl.Instance.GetCurrentUserStudents();

                    SelectedStudent = students?.ElementAt(0);
                }

                if (SelectedStudent != null)
                    SchoolId = SelectedStudent.id;
                var service = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();
                 
                var data = (await service.GetPromoCodeAsync(IsFromMenu ? 0 : SchoolId));

                PriceList = data?.Prices?.ToList();

                packetCollectionView.ReloadData();

                EndLoading();

                if (PriceList?.Count > 0)
                {
                    var contentHeight = ((PriceList?.Count) * 70) + 135;
                    packetCollectionHeightConstraint.Constant = (System.nfloat)contentHeight;
                    ContentHeight = (System.nfloat)(contentHeight);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            DataLoaded?.Invoke();
        }

        private void CollectionViewSettings()
        {
            packetCollectionView.Delegate = this;
            packetCollectionView.DataSource = this;

            packetCollectionView.ScrollEnabled = !DisableScroll;

            packetCollectionView.RegisterNibForCell(PacketCollectionViewCell.Nib, PacketCollectionViewCell.Identifier);
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = packetCollectionView.DequeueReusableCell(PacketCollectionViewCell.Identifier, indexPath) as PacketCollectionViewCell;

            var data = PriceList?[indexPath.Row];

            cell.PriceSelected = (priice) =>
            {
                SelectedPriceIndex = PriceList.IndexOf(PriceList?.FirstOrDefault(x => x.price == priice.price));

                SelectedPrice = priice;

                packetCollectionView.ReloadData();

                PriceSelected?.Invoke(priice);
            };

            if (indexPath.Row == SelectedPriceIndex)
                cell.InitData(data, true);
            else
                cell.InitData(data);
            return cell;

        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return PriceList?.Count?? 0 ;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CoreGraphics.CGSize(collectionView.Frame.Width, 70);
        }

    }
}
