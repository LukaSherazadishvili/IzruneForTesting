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
		UIKit.UILabel answerLbl { get; set; }

		[Outlet]
		UIKit.UIView answerView { get; set; }

		[Outlet]
		UIKit.UILabel numberLbl { get; set; }

		[Outlet]
		UIKit.UIView numberView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (answerView != null) {
				answerView.Dispose ();
				answerView = null;
			}

			if (numberView != null) {
				numberView.Dispose ();
				numberView = null;
			}

			if (numberLbl != null) {
				numberLbl.Dispose ();
				numberLbl = null;
			}

			if (answerLbl != null) {
				answerLbl.Dispose ();
				answerLbl = null;
			}
		}
	}
}
