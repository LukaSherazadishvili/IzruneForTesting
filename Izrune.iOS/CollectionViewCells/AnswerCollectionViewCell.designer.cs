// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
            if (answerContentView != null) {
                answerContentView.Dispose ();
                answerContentView = null;
            }

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
        }
    }
}