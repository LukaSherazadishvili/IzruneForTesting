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
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ParrentProfileEditActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutParrentProfile;

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


        [MapControl(Resource.Id.SaveButton)]
        LinearLayout SaveButton;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BackButton.Click += BackButton_Click;
            SaveButton.Click += SaveButton_Click;


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

            ParrentRegion.Adapter = DataAdapter;


        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await MpdcContainer.Instance.Get<IUserServices>().EditParentProfileAsync(ParrentMail.Text, Phone.Text, ParrentRegion.SelectedItem.ToString(), ParrentVillage.Text);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}