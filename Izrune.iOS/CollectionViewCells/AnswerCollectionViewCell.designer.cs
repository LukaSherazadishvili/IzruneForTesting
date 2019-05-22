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
	[Register ("AnswerCollectionViewCell")]
	partial class AnswerCollectionViewCell
	{
		[Outlet]
		UIKit.UIView answerContentView { get; set; }

		[Outlet]
		UIKit.UILabel answerLbl { get; set; }

		[Outlet]
		UIKit.UIView answerView { get; set; }

		[Outlet]
		UIKit.UILabel numberLbl { get; set; }

		[Outlet]
		UIKit.UIView numberView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (answerLbl != null) {
				answerLbl.Dispose ();
				answerLbl = null;
			}

			if (answerView != null) {
				answerView.Dispose ();
				answerView = null;
			}

			if (numberLbl != null) {
				numberLbl.Dispose ();
				numberLbl = null;
			}

			if (numberView != null) {
				numberView.Dispose ();
				numberView = null;
			}

			if (answerContentView != null) {
				answerContentView.Dispose ();
				answerContentView = null;
			}
		}
	}
}
