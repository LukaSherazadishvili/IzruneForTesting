using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MpdcContainer = ServiceContainer.ServiceContainer;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Helpers;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL;
using Izrune.Helpers;
using Izrune.Fragments.DialogFrag;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
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

            SetLastData();


            NextButton.Click += NextButton_Click;

            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

            Phone.TextChanged += Phone_TextChanged;
            Password.TextChanged += Phone_TextChanged;
            RepPassword.TextChanged += Phone_TextChanged;
            Name.TextChanged += Phone_TextChanged;
        }


        private void SetLastData()
        {
            var user = UserControl.Instance.RegistrationUser;
            if (user != null)
            {
                Name.Text = user.UserName;
                Phone.Text = user.Phone;
                Password.Text = user.Password;
                RepPassword.Text = user.Password;
                Email.Text = user.Email;
            }
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

        private async void NextButton_Click(object sender, EventArgs e)
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

            var isNameExist = await MpdcContainer.Instance.Get<IRegistrationServices>().ExistUserName(Name.Text);

            bool IsGeorgianKey = false;

            foreach(var items in IzruneHellper.Instance.letters)
            {

                IsGeorgianKey = Name.Text.Contains(items) ? true : false;
                if (IsGeorgianKey)
                    break;
                
            };


            if (isNameExist||Name.Text.Length<5||IsGeorgianKey)
            {

                if(IsGeorgianKey)
                    ShowAlert("შეცდომა", "მომხმარებლის სახელი უნდა შეიცავდეს მხოლოდ ლათინურ ასოებს");

                Name.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }

            bool IsGeorgianPassword = false;


            foreach (var items in IzruneHellper.Instance.letters)
            {

                IsGeorgianPassword = Password.Text.Contains(items) ? true : false;
                if (IsGeorgianPassword)
                    break;

            };

            if (Password.Text.Length < 7 || IsGeorgianPassword)
            {
                Password.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);

                if (IsGeorgianPassword)
                {
                    ShowAlert("შეცდომა", "პაროლი უნდა შეიცავდეს მხოლოდ ლათინურ ასოებს");
                }

            }

            if (Phone.Text.Length != 9)
            {
                Phone.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                ShowAlert("შეცდომა", "ტელეფონის ნომერი უნდა შედგებოდეს 9 ციფრისგან");
            }



            if (!(string.IsNullOrEmpty(Phone.Text) && string.IsNullOrEmpty(Name.Text) && string.IsNullOrEmpty(Password.Text)
                && string.IsNullOrEmpty(RepPassword.Text) )  && !IsGeorgianPassword && !IsGeorgianKey && Phone.Text.Length == 9)
            {




                if (Password.Text == RepPassword.Text&&Password.Text.Length>7&& Name.Text.Length > 5&&Password.Text.Length<15&&Name.Text.Length<15&&!isNameExist&& Phone.Text.Length == 9)
                {

                    UserControl.Instance.RegistrationParrentPartTwo(Phone.Text, Email.Text, Name.Text, Password.Text);

                    Intent intent = new Intent(this, typeof(RegistrationStudentActivity));
                    StartActivity(intent);
                }
                else
                {
                   

                    if (Name.Text.Length < 5||Name.Text.Length>15)
                        ShowAlert("შეცდომა", "მომხმარებლის სახელი უნდა შეიცავდეს მინიმუმ 5 სიმბოლოს და მაქსიმუმ 15 სიმბოლოს");


                    if (Password.Text.Length < 7 || Password.Text.Length > 15)
                    {
                        ShowAlert("შეცდომა", "პაროლი უნდა შეიცავდეს მინიმუმ 7 სიმბოლოს და მაქსიმუმ 15 სიმბოლოს");
                        Password.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                        RepPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    }
                    if (Password.Text != RepPassword.Text)
                    {
                        ShowAlert("შეცდომა", "პაროლები არ ემთხვევა ერთმანეთს");
                        Password.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                        RepPassword.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    }
                }

            }
        }

        private void ShowAlert(string title,string text)
        {
            var transcation = FragmentManager.BeginTransaction();
            warningDialogFragment dialog = new warningDialogFragment(title, text, true);
            dialog.Show(transcation, "Image Dialog");
        }
    }
}