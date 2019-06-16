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
    [Register ("SelectSchoolViewController")]
    partial class SelectSchoolViewController
    {
        [Outlet]
        UIKit.UICollectionView schoolCollectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (schoolCollectionView != null) {
                schoolCollectionView.Dispose ();
                schoolCollectionView = null;
            }
        }
    }
}