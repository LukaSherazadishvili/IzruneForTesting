using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IZrune.PCL.Abstraction.Services;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using MpdcContainer = ServiceContainer.ServiceContainer;
using IZrune.PCL.Helpers;
using IZrune.PCL.Abstraction.Models;
using Izrune.Fragments;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class NextRegistrationStudentActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutRegistrationStudent;

        [MapControl(Resource.Id.Container)]
        FrameLayout container;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.SendButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.BottomBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.StudentCity)]
        Spinner City;

        [MapControl(Resource.Id.StudentVillage)]
        EditText Village;

        [MapControl(Resource.Id.StudentSchool)]
        Spinner School;

        [MapControl(Resource.Id.StudentClass)]
        Spinner StudentClass;

        private IEnumerable<IZrune.PCL.Abstraction.Models.IRegion> Regions;
        private IRegion CurrentRegion;
        private ISchool CurrentSchool;

        int CurrentClass = 0;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            BackButton.Click += BackButton_Click;
            BotBackButton.Click += BotBackButton_Click;

             Regions = await MpdcContainer.Instance.Get<IZrune.PCL.Abstraction.Services.IRegistrationServices>().GetRegionsAsync();

            var DataAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
           Regions.Select(i => i.title).ToList());

            List<int> Classes = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };

            var ClassDataAdapter = new ArrayAdapter<string>(this,
           Android.Resource.Layout.SimpleSpinnerDropDownItem,
          Classes.Select(i=>$" {i} კლასი").ToList());



            StudentClass.Adapter = ClassDataAdapter;

            StudentClass.ItemSelected += (s, e) =>
            {
                CurrentClass = Classes.ElementAt(e.Position);
            };

            
            City.Adapter = DataAdapter;
            City.ItemSelected += City_ItemSelected;

            School.ItemSelected += School_ItemSelected;

            NextButton.Click += NextButton_Click;
        }

       

        private void School_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            CurrentSchool = CurrentRegion.Schools.ElementAt(e.Position);
        }

        int SelectedClass;

        private void NextButton_Click(object sender, EventArgs e)
        {


            UserControl.Instance.RegistrationStudentPartTwo(CurrentRegion.id, CurrentSchool.id, CurrentClass, Village.Text);



            ChangeFragmentPage(new ServiceFragment() {CurrentId=UserControl.Instance.RegistrationStudent.SchoolId,Backclick=()=> {
                OnBackPressed();
            } }, container.Id);
        }

        private void City_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            CurrentRegion = Regions.ElementAt(e.Position);
            var DataAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
           CurrentRegion.Schools.Select(i => i.title).ToList());
            School.Adapter = DataAdapter;
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
    }
}