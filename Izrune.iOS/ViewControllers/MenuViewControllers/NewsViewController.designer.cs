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
    [Register ("NewsViewController")]
    partial class NewsViewController
    {
        [Outlet]
        UIKit.UICollectionView newsCollectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (newsCollectionView != null) {
                newsCollectionView.Dispose ();
                newsCollectionView = null;
            }
        }
    }
}