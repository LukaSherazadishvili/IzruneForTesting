using System;
using IZrune.PCL.Enum;
using UIKit;

namespace Izrune.iOS.Models
{
    public class MenuItem
    {
        public MenuType Type { get; set; }
        public string Title { get; set; }
        public UIImage Image { get; set; }
        public bool IsSelected { get; set; }

        public MenuItem()
        {
        }

    }
}
