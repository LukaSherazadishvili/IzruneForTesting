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
	[Register ("ResultCollectionViewCell")]
	partial class ResultCollectionViewCell
	{
		[Outlet]
		UIKit.UILabel correctAnswersCountLbl { get; set; }

		[Outlet]
		UIKit.UILabel dateLbl { get; set; }

		[Outlet]
		UIKit.UILabel inCorrectAnswersCountLbl { get; set; }

		[Outlet]
		UIKit.UIView mainView { get; set; }

		[Outlet]
		UIKit.UILabel pointsLbl { get; set; }

		[Outlet]
		UIKit.UILabel skipedQuestionsCountLbl { get; set; }

		[Outlet]
		UIKit.UILabel timeLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (correctAnswersCountLbl != null) {
				correctAnswersCountLbl.Dispose ();
				correctAnswersCountLbl = null;
			}

			if (dateLbl != null) {
				dateLbl.Dispose ();
				dateLbl = null;
			}

			if (inCorrectAnswersCountLbl != null) {
				inCorrectAnswersCountLbl.Dispose ();
				inCorrectAnswersCountLbl = null;
			}

			if (mainView != null) {
				mainView.Dispose ();
				mainView = null;
			}

			if (pointsLbl != null) {
				pointsLbl.Dispose ();
				pointsLbl = null;
			}

			if (skipedQuestionsCountLbl != null) {
				skipedQuestionsCountLbl.Dispose ();
				skipedQuestionsCountLbl = null;
			}

			if (timeLbl != null) {
				timeLbl.Dispose ();
				timeLbl = null;
			}
		}
	}
}
