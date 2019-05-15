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
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class StatisticFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutStatistic;

        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView recycler;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(1);
            
            var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

            recycler.SetAdapter(adapter);
            recycler.SetLayoutManager(new LinearLayoutManager(this));

            


        }
    }
}