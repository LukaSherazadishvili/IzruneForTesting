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
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ExamStatisticActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; }= Resource.Layout.LayoutStatistic;

        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView recycler;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //int.TryParse( Intent.GetStringExtra("StudentId"),out int StudentId);

            //var Result =await UserControl.Instance.GetCurrentUser();

            //var CurrentStudent = Result.Students.FirstOrDefault(i => i.id == StudentId);


            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);

            var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

            recycler.SetAdapter(adapter);
            recycler.SetLayoutManager(new LinearLayoutManager(this));

        }
    }
}