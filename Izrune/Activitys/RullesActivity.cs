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
using Com.Airbnb.Lottie;
using Izrune.Attributes;
using Izrune.Fragments.DialogFrag;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class RullesActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutEndRegistration;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.Checker)]
        LottieAnimationView checker;

        [MapControl(Resource.Id.EndRegistrationButton)]
        LinearLayout EndRegistrationButton;

        [MapControl(Resource.Id.AddStudentButton)]
        LinearLayout AddStudentButton;

        [MapControl(Resource.Id.RullesText)]
        TextView RullesText;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;


        bool isChecked = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            checker.Progress = 0;
            checker.Click += Checker_Click;
            EndRegistrationButton.Click += EndRegistrationButton_Click;
            AddStudentButton.Click += AddStudentButton_Click;



            RullesText.Click += RullesText_Click;
            BotBackButton.Click += BotBackButton_Click;
            BackButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void BotBackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private async void RullesText_Click(object sender, EventArgs e)
        {
           

            var Result = await MpdcContainer.Instance.Get<IRegistrationServices>().GetAgreement();

            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            RullesDialogFragment RullesDialog = new RullesDialogFragment(Result);
            RullesDialog.Show(transcation, "Dialog Fragment");

        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            UserControl.Instance.RegistrationStudent = null;
            Intent intent = new Intent(this, typeof(RegistrationStudentActivity));
            StartActivity(intent);
        }

        private async void EndRegistrationButton_Click(object sender, EventArgs e)
        {
            if (isChecked)
            {
                Startloading(true);
              var Result=await UserControl.Instance.FinishRegistration();

                StopLoading();
                if (Result!=null)
                {
                   
                    Intent intent = new Intent(this, typeof(ActivityPaymentCategory));
                    intent.PutExtra("notreg", "sdds");
                    StartActivity(intent);
                }
                else
                {
                    //Toast.MakeText(this, "მოხდა შეცდომა", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "მონიშნეთ „ვეთანხმები მომსახურების წესებს და პირობებს“", ToastLength.Long).Show();
            }
        }

        private void Checker_Click(object sender, EventArgs e)
        {
            if (!isChecked)
            {
                checker.PlayAnimation();

               // checker.Progress = 100;

                isChecked = true;
            }
            else
            {
                checker.Progress = 0;
                isChecked = false;
            }
        }

        public override void OnBackPressed()
        {
            //Intent intent = new Intent(this,typeof(MainActivity));
            //intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask  |ActivityFlags.ClearTop );
            //StartActivity(intent);
            UserControl.Instance.CurrentStudent = UserControl.Instance.MyRegistrationStudent.LastOrDefault();
            UserControl.Instance.MyRegistrationStudent.Remove(UserControl.Instance.MyRegistrationStudent.LastOrDefault());
            Finish();
    }
    }
}