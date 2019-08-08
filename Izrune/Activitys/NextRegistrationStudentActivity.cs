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
using Android.Content.PM;
using Izrune.Fragments.DialogFrag;
using Izrune.Adapters.SpinerAdapter;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class NextRegistrationStudentActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutRegistrationStudent;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

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

        [MapControl(Resource.Id.SchoolContainer)]
        FrameLayout SchoolContainer;

        [MapControl(Resource.Id.CityContainer)]
        FrameLayout CityContainer;

        [MapControl(Resource.Id.ClassContainer)]
        FrameLayout ClassContainer;

        


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

            var Regionss = Regions.Select(i => i.title).ToList();
            Regionss.Insert(0, "*ქალაქი/მუნიციპალიტეტი");

            var DataAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
           Regionss);

            List<int> Classes = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };

            var Classess = Classes.Select(i => $" {i} კლასი").ToList();
            Classess.Insert(0, "კლასი");

            var ClassDataAdapter = new ArrayAdapter<string>(this,
           Android.Resource.Layout.SimpleSpinnerDropDownItem,
          Classess);





            StudentClass.Adapter = ClassDataAdapter;

            StudentClass.ItemSelected += (s, e) =>
            {
                if (e.Position != 0)
                {
                    ClassContainer.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
                    CurrentClass = Classes.ElementAt(e.Position-1);
                   

                }
                else
                    CurrentClass = 0;
            };

            
            City.Adapter = DataAdapter;
            City.ItemSelected += City_ItemSelected;

            School.ItemSelected += School_ItemSelected;

            NextButton.Click += NextButton_Click;
        }

       

        private async void School_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != 0)
            {
                SchoolContainer.SetBackgroundResource(Resource.Drawable.izrune_editext_back);
                var index = e.Position - 1;
                CurrentSchool = CurrentRegion.Schools.ElementAt(index);

                Startloading(true);
                var Result = await MpdcContainer.Instance.Get<IUserServices>().GetPromoCodeAsync(CurrentSchool.id);
                if (!string.IsNullOrEmpty(Result.PrommoCode))
                {
                    var transcation = FragmentManager.BeginTransaction();
                    SchoolAlert dialog = new SchoolAlert();
                    dialog.Show(transcation, "Image Dialog");

                }
                StopLoading();
            }
            else
                CurrentSchool = null;
        }

        int SelectedClass;

        private void NextButton_Click(object sender, EventArgs e)
        {
            CloseKeyboard();
            if (CurrentSchool == null)
            {
                SchoolContainer.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (CurrentRegion == null)
            {
                CityContainer.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);
            }
            if (!(CurrentClass > 0))
            {
                ClassContainer.SetBackgroundResource(Resource.Drawable.InvalidEditTextBackground);


            }


            if (CurrentSchool != null && CurrentRegion != null&&CurrentClass>0)
            {

                UserControl.Instance.RegistrationStudentPartTwo(CurrentRegion.id, CurrentSchool.id, CurrentClass, Village.Text);

                ChangeFragmentPage(new ServiceFragment()
                {
                    CurrentId = UserControl.Instance.RegistrationStudent.SchoolId,
                    Backclick = () => {
                        OnBackPressed();
                    }
                }, container.Id);
            }


        }

        private void City_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           


            if (e.Position != 0)
            {
                CurrentRegion = Regions.ElementAt(e.Position-1);
                CityContainer.SetBackgroundResource(Resource.Drawable.izrune_editext_back);

                var Resukt = CurrentRegion.Schools.Select(i => i.title)?.ToList();
               
                Resukt.Insert(0, "*სკოლა");



                //  var DataAdapter = new ArrayAdapter<string>(this,
                // Resource.Layout.ItemDropDownSpiner,
                //  Resukt
                //);

                //  DataAdapter.SetDropDownViewResource(Resource.Layout.ItemDropDownSpiner);

                MySpinnerAdapter DataAdapter = new MySpinnerAdapter(this, Resukt);
                  
                
                School.Adapter = DataAdapter;

                School.ItemSelected -= School_ItemSelected;
                School.ItemSelected += School_ItemSelected;
            }
            else
            {
                CurrentRegion = null;
                List<string> Resukt = new List<string>();
                Resukt.Insert(0, "*სკოლა");


                var DataAdapter = new ArrayAdapter<string>(this,
                Resource.Layout.ItemDropDownSpiner,
                Resukt
              );


                School.Adapter = DataAdapter;
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
    }
}