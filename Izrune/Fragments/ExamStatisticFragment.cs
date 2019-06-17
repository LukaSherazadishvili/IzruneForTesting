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
    class ExamStatisticFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.ItemBestExamStatistic;

        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView statisticRecycler;

        [MapControl(Resource.Id.FavoriteContainer)]
        LinearLayout FavContainer;

        [MapControl(Resource.Id.bestDate)]
        TextView date;

        [MapControl(Resource.Id.BestPoint)]
        TextView Point;

        [MapControl(Resource.Id.minute)]
        TextView Minute;


        [MapControl(Resource.Id.second)]
        TextView Second;

        [MapControl(Resource.Id.TestCount)]
        TextView TestCount;

        [MapControl(Resource.Id.BetwenDate)]
        TextView BetwenDate;

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);

            if (Statistic.Count() > 0)
            {
                var Result = Statistic.OrderByDescending(i => i.Point).FirstOrDefault();




                var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

                statisticRecycler.SetAdapter(adapter);
                statisticRecycler.SetLayoutManager(new LinearLayoutManager(this));
            }


        }




    }
}