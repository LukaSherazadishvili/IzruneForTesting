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
	[Register ("SelectSchoolCollectionViewCell")]
	partial class SelectSchoolCollectionViewCell
	{
		[Outlet]
		UIKit.UIView mainView { get; set; }

		[Outlet]
		UIKit.UILabel schoolLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (schoolLbl != null) {
				schoolLbl.Dispose ();
				schoolLbl = null;
			}
		}
	}
}
