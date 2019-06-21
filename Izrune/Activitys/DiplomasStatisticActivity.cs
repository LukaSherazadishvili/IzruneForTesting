using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class DiplomasStatisticActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.Shemajamebeli_Testi_diplomi;


        [MapControl(Resource.Id.DiplomaRecycler)]
        RecyclerView DiplomaRecycler;

        [MapControl(Resource.Id.DateSpiner)]
        Spinner DateSpinner;



        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);

            var Result = Statistic.Select(i => i.ExamDate).ToList();
            


           var test= Result.DistinctBy(i=>i.Year);

            var DataAdapter = new ArrayAdapter<string>(this,
               Android.Resource.Layout.SimpleSpinnerDropDownItem,
               test.Select(i=>$"{i.Year}-{i.Year-1} სასწავლო წელი").ToList());

            DateSpinner.Adapter = DataAdapter;
            var manager = new GridLayoutManager(this, 2);

            DateSpinner.ItemSelected += (s, e) =>
            {
                var CurrentDate = test.ElementAt(e.Position);

                var StatResult = Statistic.Where(i=>i.DiplomaUrl!="").ToList();

                var FinalResult = StatResult.Where(i => i.ExamDate.Year <= CurrentDate.Year && i.ExamDate.Year >= CurrentDate.Year - 1);

                var adapter = new DiplomaRecyclerViewAdapter(FinalResult.ToList());

                DiplomaRecycler.SetAdapter(adapter);
                DiplomaRecycler.SetLayoutManager(manager);
                adapter.OnDiplomaClikc = (() =>
                {
                    Intent intent = new Intent(this, typeof(DiplomaActivity));
                    StartActivity(intent);
                });
            };

        }
    }
}