﻿using System;
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
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class PaymentCategoryTwoActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.Sabanko_Gadmoricxvis;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.UserNameLasetName)]
        TextView Name;

        [MapControl(Resource.Id.ProfileNumber)]
        TextView ProfileNumber;

        [MapControl(Resource.Id.AmounTxt)]
        TextView Amount;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BackButton.Click += (s, e) =>
            {
                OnBackPressed();
            };

            Startloading();

            var user = UserControl.Instance.RegistrationUser;

            Name.Text = $"{user.Name} {user.LastName}";

            Amount.Text = UserControl.Instance.GetAllPackagePrice().ToString();

            MpdcContainer.Instance.Get<ILoginServices>().LoginUser(user.UserName, user.Password);

            ProfileNumber.Text = UserControl.Instance.Parent.ProfileNumber.ToString();

            StopLoading();

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

    }
}