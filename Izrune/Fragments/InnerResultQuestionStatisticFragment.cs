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
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class InnerResultQuestionStatisticFragment:MPDCBaseFragment
    {

        protected override int LayoutResource { get; } = Resource.Layout.LayoutQuestionStatistic;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        [MapControl(Resource.Id.StatisticecyclerView)]
        RecyclerView StatisticRecyclerView;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            Activity.RunOnUiThread(async () => {
                Startloading(true);
                var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);

              

                var adapter = new QuestionStatisticAdapter((Result.Where(i => i.Id == IzruneHellper.Instance.CurrentStatistic.Id)?.FirstOrDefault()?.Questions as IEnumerable<IFinalQuestion>)?.ToList(), this);
                StatisticRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
                StatisticRecyclerView.SetAdapter(adapter);
                StopLoading();
            });

        }
    }
}