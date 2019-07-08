// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Izrune.iOS
{
	[Register ("PaymentHistoryViewController")]
	partial class PaymentHistoryViewController
	{
		[Outlet]
		UIKit.UICollectionView paymentHistoryCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (paymentHistoryCollectionView != null) {
				paymentHistoryCollectionView.Dispose ();
				paymentHistoryCollectionView = null;
			}
		}
	}
}
