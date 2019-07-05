using System;
using IZrune.PCL.Abstraction.Services;
using UIKit;

namespace Izrune.iOS.Utils
{
    public class AlertDialogService : IAlertService
    {
        public AlertDialogService()
        {
        }

        public void ShowAlerDialog(string Title, string Message)
        {
            var alertVc = UIAlertController.Create(Title, Message, UIAlertControllerStyle.Alert);
            alertVc.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));

            var rootVc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            rootVc.PresentViewController(alertVc, true, null);
        }
    }
}
