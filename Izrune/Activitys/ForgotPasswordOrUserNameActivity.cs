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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var result = Convert.ToBoolean(Intent.GetStringExtra("IsPasswordOrNot"));

            SendButton.Click += (s, e) =>
            {
                ChangeFragmentPage(new SaccesFragment(), Container.Id);
            };


            if (!result)
            {
                Pastxt.Text = "მომხმარებლის სახელის აღდგენა";
            }


            
        }
    }
}