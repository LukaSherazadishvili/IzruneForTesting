using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace Izrune
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class MyFireBaseMessagingService:FirebaseMessagingService
    {
        private readonly string NOTIFICATION_CHANEL_ID= "Izrune.Izrune";

        public override void OnMessageReceived(RemoteMessage message)
        {
            if (!message.Data.GetEnumerator().MoveNext())
                SendNotification( message.GetNotification().Body);
            //else
            //    SendNotification(message.Data);
        }

        //private void SendNotification(IDictionary<string, string> data)
        //{
        //    data.TryGetValue("title", out string title);
        //    data.TryGetValue("body", out string body);
        //   // SendNotification(title, body);
        //}

        private void SendNotification( string body)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingintent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var defaultsoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.logo)
                .SetContentTitle("izrune")
                .SetContentText(body)
                .SetAutoCancel(true)
                .SetSound(defaultsoundUri)
                .SetContentIntent(pendingintent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());

        }

       
    }
}