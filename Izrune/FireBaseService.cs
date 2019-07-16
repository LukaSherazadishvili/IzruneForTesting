using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;

namespace Izrune
{
    [Service]
    [IntentFilter(new[] {"com.google.firebase.INSTANCE_ID_EVENT" })]
    class FireBaseService:FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            base.OnTokenRefresh();
            var RefreshToken = FirebaseInstanceId.Instance.Token;
            SendTokenService(RefreshToken);
        }

        private void SendTokenService(string refreshToken)
        {
            Log.Debug(PackageName, refreshToken);
        }
    }
}