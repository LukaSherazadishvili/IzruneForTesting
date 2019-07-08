using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ParrentProfileEditActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutParrentProfile;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.ParentNameTxt)]
        TextView ParrentName;

        [MapControl(Resource.Id.ParentLastNameTxt)]
        TextView ParrentLastName;

        [MapControl(Resource.Id.ParrentRegionSpinner)]
        Spinner ParrentRegion;

        [MapControl(Resource.Id.ParrentVillageTxt)]
        EditText ParrentVillage;

        [MapControl(Resource.Id.MobilePhoneTxt)]
        EditText Phone;


        [MapControl(Resource.Id.ParentMailTxt)]
        EditText ParrentMail;

        [MapControl(Resource.Id.ParrentBDayDay)]
        TextView ParrentBadaDay;

        [MapControl(Resource.Id.ParrentBdayMonth)]
        TextView ParrentBadayMonth;

        [MapControl(Resource.Id.ParrentdayYear)]
        TextView ParrentBdayYear;

        [MapControl(Resource.Id.SaveButton)]
        LinearLayout SaveButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BtBack;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Startloading();
            


            var Result = UserControl.Instance.GetCurrentUser();
            var Region = MpdcContainer.Instance.Get<IRegistrationServices>().GetRegionsAsync();


            await Task.WhenAll(Result, Region);

            ParrentName.Text = Result.Result.Name;
            ParrentLastName.Text = Result.Result.LastName;
            ParrentMail.Hint = Result.Result.Email;
            Phone.Text = Result.Result.Phone;

            var DataAdapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerDropDownItem,
            Region.Result.Select(i => i.title).ToList());


            ParrentVillage.Text = Result.Result.Vilage;
            ParrentRegion.Adapter = DataAdapter;

            int SpinerPosition = DataAdapter.GetPosition(Result.Result.City);
            ParrentRegion.SetSelection(SpinerPosition);


          //var CurrentRegion=  Region.Result.FirstOrDefault(i => i.title == Result.Result.City);

          //  var Index = Region.Result.ToList().IndexOf(CurrentRegion);

          //  ParrentRegion.SetSelection(Index);

            ParrentBadaDay.Text = Result.Result.bDate.Value.Day.ToString();
            ParrentBadayMonth.Text = Result.Result.bDate.Value.Month.ToString();
            ParrentBdayYear.Text = Result.Result.bDate.Value.Year.ToString();

            StopLoading();

            BackButton.Click += BackButton_Click;
            SaveButton.Click += SaveButton_Click;


            BtBack.Click += (s, e) =>
            {
                OnBackPressed();
            };
           

        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Startloading(true);
              await UserControl.Instance.EditParrentProfile(ParrentMail.Text, Phone.Text, ParrentRegion.SelectedItem.ToString(), ParrentVillage.Text);
                StopLoading();
            }
            catch(Exception ex)
            {
                Console.WriteLine();
            }
           

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
    }
}