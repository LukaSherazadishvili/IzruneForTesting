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
	[Register ("DiagramViewController")]
	partial class DiagramViewController
	{
		[Outlet]
		UIKit.UIView pointChartView { get; set; }

		[Outlet]
		UIKit.UIView testChartView { get; set; }

		[Outlet]
		UIKit.UIView timeChartView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (timeChartView != null) {
				timeChartView.Dispose ();
				timeChartView = null;
			}

			if (pointChartView != null) {
				pointChartView.Dispose ();
				pointChartView = null;
			}

			if (testChartView != null) {
				testChartView.Dispose ();
				testChartView = null;
			}
		}
	}
}
