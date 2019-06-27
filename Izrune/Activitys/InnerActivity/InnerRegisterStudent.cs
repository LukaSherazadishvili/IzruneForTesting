﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Helpers;
using Java.Util;
using static Android.App.DatePickerDialog;

namespace Izrune.Activitys.InnerActivity
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class InnerRegisterStudent : MPDCBaseActivity, IOnDateSetListener
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutRegistrationStudentFirst;


        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.StdNameTxt)]
        EditText StudName;

        [MapControl(Resource.Id.LastNametxt)]
        EditText StudLastName;

        [MapControl(Resource.Id.StudentBDay)]
        TextView BDayDay;

        [MapControl(Resource.Id.StudMonth)]
        TextView StudentBdaymonth;

        [MapControl(Resource.Id.StudentBdayYear)]
        TextView StudentBdayYear;

        [MapControl(Resource.Id.StudentPersonalId)]
        EditText StudentPersonalId;

        [MapControl(Resource.Id.StudentPhoneNumberTxt)]
        EditText StudentPhone;

        [MapControl(Resource.Id.StudentEmail)]
        EditText StudentEmail;

        int Day, Year, Month;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            NextButton.Click += NextButton_Click;

            BDayDay.Click += BDayDay_Click;
            StudentBdaymonth.Click += BDayDay_Click;
            StudentBdayYear.Click += BDayDay_Click;
        }

        private void BDayDay_Click(object sender, EventArgs e)
        {
            ShowDialog(1);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {


            UserControl.Instance.RegistrationStudentPartOne(StudName.Text, StudLastName.Text, new DateTime(Year, Month, Day), StudentPersonalId.Text, StudentPhone.Text, StudentEmail.Text);

            Intent intent = new Intent(this, typeof(InnerRegisterStudentPartTwo));
            StartActivity(intent);

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

        }

        private void BotBackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            DateTime date = new DateTime(year, (month + 1), dayOfMonth);

            Day = dayOfMonth;
            Month = month + 1;
            Year = year;

            BDayDay.Text = dayOfMonth.ToString();
            StudentBdaymonth.Text = month.ToString();
            StudentBdayYear.Text = year.ToString();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            if (id == 1)
            {
                Calendar cal = Calendar.GetInstance(Java.Util.TimeZone.Default);
                Year = cal.Get(Calendar.Year);
                Month = cal.Get(Calendar.Month);
                Day = cal.Get(Calendar.DayOfYear);

                DatePickerDialog dialog = new DatePickerDialog(this,
                    Android.Resource.Style.ThemeHoloDialogNoActionBarMinWidth,
                    this,
                    Year, Month, Day);
                dialog.Window.SetBackgroundDrawable(new Android.Graphics.Drawables.ColorDrawable(Color.Transparent));

                return dialog;
            }
            else
            {
                return null;
            }

        }
    }
}