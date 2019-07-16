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
                SendNotification(message.GetNotification().Title, message.GetNotification().Body);
            else
                SendNotification(message.Data);
        }

        private void SendNotification(IDictionary<string, string> data)
        {
            data.TryGetValue("title", out string title);
            data.TryGetValue("body", out string body);
            SendNotification(title, body);
        }

        private void SendNotification(string title, string body)
        {
            NotificationManager NotManager = (NotificationManager)GetSystemService(Context.NotificationService);
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                NotificationChannel NotChanel = new NotificationChannel(NOTIFICATION_CHANEL_ID, "Notification Chanel", Android.App.NotificationImportance.Default);
                NotChanel.Description = "Izrune";
                NotChanel.EnableLights(true);
                NotChanel.LightColor = Color.Red;
                NotChanel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });
                
                NotManager.CreateNotificationChannel(NotChanel);
            }
            NotificationCompat.Builder notiBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANEL_ID);
            notiBuilder.SetAutoCancel(true)
                .SetDefaults(-1)
                .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                .SetContentTitle(title)
                .SetContentText(body)
                .SetVibrate(new long[] {0,1000,500,1000})
                .SetSmallIcon(Resource.Drawable.logo)
                .SetContentInfo("info");

            NotManager.Notify(new Random().Next(), notiBuilder.Build());
                
        }

       
    }
}