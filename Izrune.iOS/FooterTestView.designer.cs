// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Izrune.iOS
{
    [Register ("FooterTestView")]
    partial class FooterTestView
    {
        [Outlet]
        UIKit.UIButton skipBtn { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (skipBtn != null) {
                skipBtn.Dispose ();
                skipBtn = null;
            }
        }
    }
}