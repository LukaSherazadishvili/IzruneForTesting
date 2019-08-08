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
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class ContactFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutContact;

        [MapControl(Resource.Id.MobilePhone)]
        LinearLayout Phone;

        [MapControl(Resource.Id.SmsContainer)]
        LinearLayout SmsContainer;

        [MapControl(Resource.Id.FbContainer)]
        LinearLayout FbContainer;


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Phone.Click += (s, e) =>
            {
                MakePhoneCall("577683232");
            };

            SmsContainer.Click += (s, e) =>
            {

                string mailto = "mailto:bob@example.org";
       

                var emailIntent = new Intent(Android.Content.Intent.ActionSend);
              
                emailIntent.PutExtra(Android.Content.Intent.ExtraEmail, new[] { "info@izrune.ge" });
                emailIntent.PutExtra(Android.Content.Intent.ExtraCc, new[] { "info@izrune.ge" });
                emailIntent.PutExtra(Android.Content.Intent.ExtraSubject, "title");
                emailIntent.SetType("message/rfc822");
                emailIntent.PutExtra(Android.Content.Intent.ExtraText, "e-mail body");
                StartActivity(Intent.CreateChooser(emailIntent, "Send e-mail"));
            };

            FbContainer.Click += (s, e) =>
            {
                var uri = Android.Net.Uri.Parse("https://www.facebook.com/izrune.ge/");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

           
        }


        public void MakePhoneCall(string number)
        {
            var uri = Android.Net.Uri.Parse($"tel:{number}");
            var intent = new Intent(Intent.ActionDial, uri);
            StartActivity(intent);
        }

    }
}