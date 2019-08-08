using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class ActivityPaymentCategory : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutPaymentCategory;

        [MapControl(Resource.Id.CardPayButton)]
        LinearLayout CardButton;

        [MapControl(Resource.Id.BankPayButton)]
        LinearLayout BankButton;

        [MapControl(Resource.Id.PayBoxButton)]
        LinearLayout PayBocButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        [MapControl(Resource.Id.Gradueition)]
        TextView grad;

        private string InnerIncome;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var res = Intent.GetStringExtra("notreg");

            if (string.IsNullOrEmpty(res))
            {
                grad.Visibility = ViewStates.Invisible;
            }

            InnerIncome = Intent.GetStringExtra("Inner");

            CardButton.Click += (s, e) =>
            {

                Intent intent = new Intent(this,typeof(OnlinePayActivity));
                StartActivity(intent);

            };
            BankButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this,typeof(PaymentCategoryTwoActivity));
                StartActivity(intent);

            };

            PayBocButton.Click += (s, e) =>
            {
                var uri = Android.Net.Uri.Parse("http://www.izrune.ge/images/tbcpay_image2.png");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

            };

            BackButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
           
            OnBackPressed();
        }


        public override void OnBackPressed()
        {
            if (String.IsNullOrEmpty(InnerIncome))
            {

                Intent intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
                UserControl.Instance.Resetregistration();
                StartActivity(intent);
                this.Finish();
            }
            else
            {
                this.Finish();
            }
        }
    }
}