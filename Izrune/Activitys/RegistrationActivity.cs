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
    class RegistrationActivity : MPDCBaseActivity
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
        EditText BdayDay;

        [MapControl(Resource.Id.ParentBdayMonth)]
        EditText BDayMonth;

        [MapControl(Resource.Id.ParentBdayYear)]
        EditText BdayYear;

        [MapControl(Resource.Id.ParrentCity)]
        Spinner ParrentCity;

        [MapControl(Resource.Id.ParrentVillage)]
        EditText ParrentVillage;


        string city;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetEvents();

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;
            var Result = await MpdcContainer.Instance.Get<IRegistrationServices>().GetRegionsAsync();

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
             Result.Select(i=>i.title).ToList());

            ParrentCity.Adapter = DataAdapter;
            ParrentCity.ItemSelected += (s, e) =>
            {
                city = Result.ElementAt(e.Position).title;
            };

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
            BdayDay.TextChanged += UserName_Click;
            BDayMonth.TextChanged += UserName_Click;
            BdayYear.TextChanged += UserName_Click;
            
            ParrentVillage.TextChanged += UserName_Click;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {

            if (!ValidateUser())
            {
                CheckEditext();
            }
            else
            {

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
            if (string.IsNullOrEmpty(ParrentVillage.Text))
            {
                ParrentVillage.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
        }

        private bool ValidateUser()
        {
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(BdayDay.Text)
                || string.IsNullOrEmpty(BDayMonth.Text) || string.IsNullOrEmpty(BdayYear.Text)
                ||string.IsNullOrEmpty(city)
                || string.IsNullOrEmpty(ParrentVillage.Text))
            {
                return false;
            }
            else
                return true;
        }
    }
}