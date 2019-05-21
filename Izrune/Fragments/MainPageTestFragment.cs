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
using Izrune.Activitys;
using Izrune.Adapters.SpinerAdapter;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class MainPageTestFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.IzruneTestebi;

        [MapControl(Resource.Id.MainTestButton)]
        FrameLayout ExamtestButton;


        [MapControl(Resource.Id.ExamTestButton)]
        FrameLayout TrainigTestButton;

        [MapControl(Resource.Id.StudentSpiner)]
        Spinner StudSpiner;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public async override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ExamtestButton.Click += ExamtestButton_Click;
            TrainigTestButton.Click += TrainigTestButton_Click;

            var Result =await UserControl.Instance.GetCurrentUser();
            UserControl.Instance.SeTSelectedStudent(1);

           var DataAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, Result.Students.Select(i=> ($"{i.Name}   {i.LastName}")).ToList());

           // var adapter = new CategorySpinnerAdapter(this, Resource.Layout.itemSpinnerText, Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList(), StudSpiner);
           // var add = new MySpinnerAdapter(this, Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());
            StudSpiner.Adapter = DataAdapter;
            
        }

      

        private void TrainigTestButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(TrainigTestActivity));
            StartActivity(intent);

        }

        private void ExamtestButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(ExamTestActivity));
            StartActivity(intent);
        }

       
    }
}