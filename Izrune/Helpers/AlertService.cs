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
using IZrune.PCL.Abstraction.Services;

namespace Izrune.Helpers
{
    class AlertService : IAlertService
    {
        public Action<string, string> AlertEVent { get; set; }

        public void ShowAlerDialog(string Title, string Message)
        {
            AlertEVent?.Invoke(Title, Message);
        }
    }
}