using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MpdcContainer = ServiceContainer.ServiceContainer;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Helpers;
using Java.Util;
using static Android.App.DatePickerDialog;
using IZrune.PCL.Abstraction.Services;
using Izrune.Fragments.DialogFrag;

namespace Izrune.Activitys
{

    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class RegistrationStudentActivity : MPDCBaseActivity, IOnDateSetListener
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




        private void SetLastData()
        {
            var user = UserControl.Instance.RegistrationStudent;
            if (user != null)
            {
                StudName.Text = user.Name;
                StudLastName.Text = user.LastName;
                BDayDay.Text = user.Bdate.Day.ToString();
                StudentBdaymonth.Text = user.Bdate.Month.ToString();
                StudentBdayYear.Text = user.Bdate.Year.ToString();
                StudentPersonalId.Text = user.PersonalNumber.ToString();
                StudentPhone.Text = user.Phone;
                StudentEmail.Text = user.Email;
                Year = user.Bdate.Year;
                Month = user.Bdate.Month;
                Day = user.Bdate.Day;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetLastData();
            StudName.TextChanged += StudName_TextChanged;
            StudLastName.TextChanged += StudLastName_TextChanged;
            NextButton.Click += NextButton_Click;
            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;
            BDayDay.Click += BDayDay_Click;
            StudentBdaymonth.Click += BDayDay_Click;
            StudentBdayYear.Click += BDayDay_Click;
            StudentPersonalId.TextChanged += StudentPersonalId_TextChanged;
            StudentPhone.TextChanged += StudentPhone_TextChanged;
        }

        private void StudentPhone_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            StudentPhone.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
        }

        private void StudentPersonalId_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            StudentPersonalId.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
        }

        private void StudLastName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            StudLastName.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
        }

        private void StudName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            StudName.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
        }

        private void BDayDay_Click(object sender, EventArgs e)
        {

            StudentBdayYear.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            StudentBdaymonth.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            BDayDay.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
            ShowDialog(1);


        }

        private async void NextButton_Click(object sender, EventArgs e)
        {
            CloseKeyboard();

            if (string.IsNullOrEmpty(StudName.Text) || string.IsNullOrEmpty(StudLastName.Text) || string.IsNullOrEmpty(StudentBdayYear.Text) ||
                string.IsNullOrEmpty(StudentPersonalId.Text)||(await MpdcContainer.Instance.Get<IRegistrationServices>().ExistPersonalId(StudentPersonalId.Text))||StudentPersonalId.Text.Length!=11 )
            {
                if (string.IsNullOrEmpty(StudName.Text))
                {
                    StudName.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                }
                if (string.IsNullOrEmpty(StudLastName.Text))
                {
                    StudLastName.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                }
                if (string.IsNullOrEmpty(StudentBdayYear.Text))
                {
                    StudentBdayYear.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    StudentBdaymonth.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    BDayDay.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                }
                if (string.IsNullOrEmpty(StudentPersonalId.Text)||StudentPersonalId.Text.Length!=11)
                {
                    StudentPersonalId.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                    ShowAlert("შეცდომა", "პირადი ნომერი უნდა შედგებოდეს 11 ციფრისგან");
                }
                //if (StudentPhone.Text.Length != 9)
                //{
                //    StudentPhone.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
                //    ShowAlert("შეცდომა", "ტელეფონის ნომერი უნდა შედგებოდეს 9 ციფრისგან");

                //}
            }
            else
            {

                UserControl.Instance.RegistrationStudentPartOne(StudName.Text, StudLastName.Text, new DateTime(Year, Month, Day), StudentPersonalId.Text, StudentPhone.Text, StudentEmail.Text);

                Intent intent = new Intent(this, typeof(NextRegistrationStudentActivity));
                StartActivity(intent);
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

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            DateTime date = new DateTime(year, (month + 1), dayOfMonth);

            Day = dayOfMonth;
            Month = month + 1;
            Year = year;

            BDayDay.Text = dayOfMonth.ToString();
           StudentBdaymonth.Text = (month+1).ToString();
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

        private void ShowAlert(string title, string text)
        {
            var transcation = FragmentManager.BeginTransaction();
            warningDialogFragment dialog = new warningDialogFragment(title, text, true);
            dialog.Show(transcation, "Image Dialog");
        }
    }
}