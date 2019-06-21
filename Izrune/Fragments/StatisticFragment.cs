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
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class StatisticFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutStatistic;

        public bool IsExam { get; set; }


        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView recycler;

        IEnumerable<IStudentsStatistic> Statistic;
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (IsExam)
            {
                 Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);
            }
            else
            {
                Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest);
            }
            var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

            recycler.SetAdapter(adapter);
            recycler.SetLayoutManager(new LinearLayoutManager(this));

            


        }
    }
}