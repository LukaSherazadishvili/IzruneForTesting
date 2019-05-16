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
	[Register ("ParentRegistrationViewController")]
	partial class ParentRegistrationViewController
	{
		[Outlet]
		UIKit.UIButton nextBtn { get; set; }

		[Outlet]
		UIKit.UIButton prewBtn { get; set; }

		[Outlet]
		UIKit.UIView viewForPager { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (nextBtn != null) {
				nextBtn.Dispose ();
				nextBtn = null;
			}

			if (prewBtn != null) {
				prewBtn.Dispose ();
				prewBtn = null;
			}

			if (viewForPager != null) {
				viewForPager.Dispose ();
				viewForPager = null;
			}
		}
	}
}
