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
using Izrune.Fragments;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;


namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ForgotPasswordOrUserNameActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.ForgotPassword;

        [MapControl(Resource.Id.PassText)]
        TextView Pastxt;

        [MapControl(Resource.Id.SendButton)]
        LinearLayout SendButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.PhoneEditext)]
        EditText ForgotPassword;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var result = Convert.ToBoolean(Intent.GetStringExtra("IsPasswordOrNot"));

           


            if (!result)
            {
                Pastxt.Text = "მომხმარებლის სახელის აღდგენა";
            }

            SendButton.Click += async(s, e) =>
            {

                bool Result;

                if (!result)
                {
                   Result=  await MpdcContainer.Instance.Get<IUserServices>().RecoverPasswordAsync(ForgotPassword.Text);                   
                }
                else
                {
                    Result = await MpdcContainer.Instance.Get<IUserServices>().RecoverUserNamedAsync(ForgotPassword.Text);
                }


                if (!Result)
                {
                    ForgotPassword.SetBackgroundResource(Resource.Drawable.RedEditTextBorder);
                }
                else
                {
                    ChangeFragmentPage(new SaccesFragment(), Container.Id);

                }
            };

            BotBackButton.Click += BotBackButton_Click;
        }

        private void BotBackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}