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
	[Register ("AddStudentViewController")]
	partial class AddStudentViewController
	{
		[Outlet]
		UIKit.UIButton addNewStudentBtn { get; set; }

		[Outlet]
		UIKit.UIView agreeView { get; set; }

		[Outlet]
		UIKit.UIImageView arrowImageView { get; set; }

		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UILabel finishLbl { get; set; }

		[Outlet]
		UIKit.UIButton nextBtn { get; set; }

		[Outlet]
		UIKit.UILabel privacyLbl { get; set; }

		[Outlet]
		UIKit.UIStackView privacyStackView { get; set; }

		[Outlet]
		UIKit.UIView selectedAgreeView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (addNewStudentBtn != null) {
				addNewStudentBtn.Dispose ();
				addNewStudentBtn = null;
			}

			if (agreeView != null) {
				agreeView.Dispose ();
				agreeView = null;
			}

			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (nextBtn != null) {
				nextBtn.Dispose ();
				nextBtn = null;
			}

			if (privacyLbl != null) {
				privacyLbl.Dispose ();
				privacyLbl = null;
			}

			if (selectedAgreeView != null) {
				selectedAgreeView.Dispose ();
				selectedAgreeView = null;
			}

			if (finishLbl != null) {
				finishLbl.Dispose ();
				finishLbl = null;
			}

			if (privacyStackView != null) {
				privacyStackView.Dispose ();
				privacyStackView = null;
			}

			if (arrowImageView != null) {
				arrowImageView.Dispose ();
				arrowImageView = null;
			}
		}
	}
}
