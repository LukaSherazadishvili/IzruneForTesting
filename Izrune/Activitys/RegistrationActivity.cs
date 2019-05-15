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
    class RegistrationActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutParentRegistration;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout ContinueButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;


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
        EditText ParrentCity;

        [MapControl(Resource.Id.ParrentVillage)]
        EditText ParrentVillage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetEvents();
            

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
            ParrentCity.TextChanged += UserName_Click;
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
            if (string.IsNullOrEmpty(ParrentCity.Text))
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
                || string.IsNullOrEmpty(BDayMonth.Text) || string.IsNullOrEmpty(BdayYear.Text) || string.IsNullOrEmpty(ParrentCity.Text)
                || string.IsNullOrEmpty(ParrentVillage.Text))
            {
                return false;
            }
            else
                return true;
        }
    }
}