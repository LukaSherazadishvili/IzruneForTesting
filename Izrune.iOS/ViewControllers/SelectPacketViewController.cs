// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.CollectionViewCells;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
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
        }


        private void SendData()
        {
            //TODO
        }

        private async Task LoadDataAsync()
        {
            ShowLoading();
            //TODO ProceList = ?

            var service = ServiceContainer.ServiceContainer.Instance.Get<IUserServices>();

            var data = (await service.GetPromoCodeAsync(SchoolId));

            PriceList = data.Prices?.ToList();

            packetCollectionView.ReloadData();

            EndLoading();

            var firstCell = packetCollectionView.DequeueReusableCell(PacketCollectionViewCell.Identifier, NSIndexPath.FromRowSection(0, 0)) as PacketCollectionViewCell;
            var lastCell = packetCollectionView.DequeueReusableCell(PacketCollectionViewCell.Identifier, NSIndexPath.FromRowSection((System.nint)(PriceList?.Count - 1), 0)) as PacketCollectionViewCell;

            var contentHeight = lastCell.Frame.Y + lastCell.Frame.Height - firstCell.Frame.Y;

            packetCollectionHeightConstraint.Constant = contentHeight;

            DataLoaded?.Invoke();

        }
        private void CollectionViewSettings()
        {
            packetCollectionView.Delegate = this;
            packetCollectionView.DataSource = this;

            packetCollectionView.RegisterNibForCell(PacketCollectionViewCell.Nib, PacketCollectionViewCell.Identifier);
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = packetCollectionView.DequeueReusableCell(PacketCollectionViewCell.Identifier, indexPath) as PacketCollectionViewCell;

            var data = PriceList?[indexPath.Row];

            cell.PriceSelected = (priice) =>
            {
                SelectedPriceIndex = PriceList.IndexOf(PriceList?.FirstOrDefault(x => x.price == priice.price));

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
