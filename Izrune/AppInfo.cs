using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Izrune
{
    public class AppInfo
    {
        private AppInfo() { }

        private static AppInfo _appinfo;
        public static AppInfo Instance => _appinfo ?? (_appinfo = new AppInfo());

        public Context CurrentContext { get; set; }
    }
}