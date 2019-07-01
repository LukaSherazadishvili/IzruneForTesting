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
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;


namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ChangePasswordActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutChangePassword;

        [MapControl(Resource.Id.OldPasswordEditText)]
        EditText MainPassword;

        [MapControl(Resource.Id.NewsPasswordEditText)]
        EditText NewsPassword;


        [MapControl(Resource.Id.RepNewsPasswordEditText)]
        EditText RepNewsPassword;

        [MapControl(Resource.Id.SaveButton)]
        LinearLayout SaveButton;

        [MapControl(Resource.Id.BotBackButton)]
        LinearLayout BotBack;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButtom;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SaveButton.Click += SaveButton_Click;

            NewsPassword.TextChanged += (s, e) =>
            {
                NewsPassword.SetBackgroundResource(Resource.Drawable.izrune_editext_back);

            };

            RepNewsPassword.TextChanged += (s, e) =>
            {
                RepNewsPassword.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            };

            BotBack.Click += (s, e) =>
            {
                OnBackPressed();
            };

            BackButtom.Click += (s, e) =>
            {
                OnBackPressed();
            };
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (NewsPassword.Text != RepNewsPassword.Text)
            {
                NewsPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                RepNewsPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            else
            {
                var Result = await MpdcContainer.Instance.Get<IUserServices>().EditePassword(MainPassword.Text, NewsPassword.Text);

                if (Result)
                {
                    Toast.MakeText(this, "წარმატებით მოხდა პაროლის შეცვლა", ToastLength.Long).Show();
                    this.Finish();
                }
                else
                    Toast.MakeText(this, "სწორად შეიყვანეთ თავდაპირველი პაროლი", ToastLength.Long).Show();
               
            }


        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}