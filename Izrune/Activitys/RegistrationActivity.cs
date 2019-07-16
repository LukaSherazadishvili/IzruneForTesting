using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using Izrune.Fragments;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using Java.Util;
using static Android.App.DatePickerDialog;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class RegistrationActivity : MPDCBaseActivity,IOnDateSetListener
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutParentRegistration;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout ContinueButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.ParentUserName)]
        EditText UserName;

        [MapControl(Resource.Id.ParentSurName)]
        EditText LastName;

        [MapControl(Resource.Id.ParentBDayDay)]
        TextView BdayDay;

        [MapControl(Resource.Id.ParentBdayMonth)]
        TextView BDayMonth;

        [MapControl(Resource.Id.ParentBdayYear)]
        TextView BdayYear;

        [MapControl(Resource.Id.ParrentCity)]
        Spinner ParrentCity;

        [MapControl(Resource.Id.ParrentVillage)]
        EditText ParrentVillage;

        int Year, Month, Day;
        string city;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetEvents();

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;
            var Result = await MpdcContainer.Instance.Get<IRegistrationServices>().GetRegionsAsync();

            var Regions = Result.Select(i => i.title).ToList();
            Regions.Insert(0, "*ქალაქი/მუნიციპალიტეტი");

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
            Regions);

            ParrentCity.Adapter = DataAdapter;
            ParrentCity.ItemSelected += (s, e) =>
            {
                // UserControl.Instance.RegistrationParrentPartOne(UserName.Text,LastName.Text,)
                if (e.Position == 0) 
                city = "";
                else
                    city = Result.ElementAt(e.Position-1).title;
            };

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
                    Android.Resource.Style.ThemeHoloLightDialogNoActionBar,
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
        }

        private void UserName_Click(object sender, EventArgs e)
        {
            (sender as EditText).SetBackgroundResource(Resource.Drawable.izrune_editext_back);
        }


        private void SetEvents()
        {
            ContinueButton.Click += ContinueButton_Click;
            UserName.TextChanged += UserName_Click;
            LastName.TextChanged += UserName_Click;
           

            BdayDay.Click += BdayDay_Click;
            BDayMonth.Click += BdayDay_Click;
            BdayYear.Click += BdayDay_Click;
            ParrentVillage.TextChanged += UserName_Click;
        }

        private void BdayDay_Click(object sender, EventArgs e)
        {
            ShowDialog(1);
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {

            if (!ValidateUser())
            {
                CheckEditext();
            }
            else
            {
                UserControl.Instance.RegistrationParrentPartOne(UserName.Text, LastName.Text, new DateTime(Year, Month, Day), city, ParrentVillage.Text);

                Intent intent = new Intent(this, typeof(NextRegistrationParentActyvity));
                StartActivity(intent);
            }
        }

        private void CheckEditext()
        {
            if (string.IsNullOrEmpty(UserName.Text))
            {
                UserName.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(LastName.Text))
            {
                LastName.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(BdayDay.Text))
            {
                BdayDay.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(BDayMonth.Text))
            {
                BDayMonth.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(BdayYear.Text))
            {
                BdayYear.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(city))
            {
                ParrentCity.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
          
        }

        private bool ValidateUser()
        {
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(BdayDay.Text)
                || string.IsNullOrEmpty(BDayMonth.Text) || string.IsNullOrEmpty(BdayYear.Text)
                ||string.IsNullOrEmpty(city)
                )
            {
                return false;
            }
            else
                return true;
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            DateTime date = new DateTime(year, (month + 1), dayOfMonth);

            Day = dayOfMonth;
            Year = year;
            Month = month+1;


            BdayDay.Text = Day.ToString();
            BDayMonth.Text =(month+1).ToString();
            BdayYear.Text = year.ToString();
        }
    }
}