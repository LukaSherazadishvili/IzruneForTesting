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
using IZrune.PCL.Helpers;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class NextRegistrationParentActyvity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutNextRegistrationParrent;


        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.ParentPhonetx)]
        EditText Phone;

        [MapControl(Resource.Id.EmailParent)]
        EditText Email;

        [MapControl(Resource.Id.NameTxt)]
        EditText Name;

        [MapControl(Resource.Id.PasswordTxt)]
        EditText Password;

        [MapControl(Resource.Id.RepPasswordTxt)]
        EditText RepPassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            NextButton.Click += NextButton_Click;

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

            Phone.TextChanged += Phone_TextChanged;
            Password.TextChanged += Phone_TextChanged;
            RepPassword.TextChanged += Phone_TextChanged;
            Name.TextChanged += Phone_TextChanged;
        }

        private void Phone_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            (sender as EditText).SetBackgroundResource(Resource.Drawable.izrune_editext_back);
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

        private void NextButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Phone.Text))
            {
                Phone.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(Name.Text))
            {
                Name.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(Password.Text))
            {
                Password.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (string.IsNullOrEmpty(RepPassword.Text))
            {
                RepPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }

            if (!(string.IsNullOrEmpty(Phone.Text) || string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(RepPassword.Text)))
            {

                if (Password.Text == RepPassword.Text)
                {

                    UserControl.Instance.RegistrationParrentPartTwo(Phone.Text, Email.Text, Name.Text, Password.Text);

                    Intent intent = new Intent(this, typeof(RegistrationStudentActivity));
                    StartActivity(intent);
                }
                else
                {
                    Password.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    RepPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                }

            }
        }
    }
}