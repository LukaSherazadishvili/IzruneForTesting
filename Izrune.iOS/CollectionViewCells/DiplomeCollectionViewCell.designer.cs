// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Izrune.iOS.CollectionViewCells
{
	[Register ("DiplomeCollectionViewCell")]
	partial class DiplomeCollectionViewCell
	{
		[Outlet]
		UIKit.UILabel dateLbl { get; set; }

		[Outlet]
		UIKit.UIView mainView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (dateLbl != null) {
				dateLbl.Dispose ();
				dateLbl = null;
			}
		}
	}
}
