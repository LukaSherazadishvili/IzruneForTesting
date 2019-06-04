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
	[Register ("ExamResultViewController")]
	partial class ExamResultViewController
	{
		[Outlet]
		UIKit.UIView pointView { get; set; }

		[Outlet]
		UIKit.UIView timeShadowView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pointView != null) {
				pointView.Dispose ();
				pointView = null;
			}

			if (timeShadowView != null) {
				timeShadowView.Dispose ();
				timeShadowView = null;
			}
		}
	}
}
