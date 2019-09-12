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
using FFImageLoading.Views;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class ResultQuestionStatisticFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutQuestionStatistic;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
            }
            catch(Exception ex)
            {

            }
        }

        [MapControl(Resource.Id.StatisticecyclerView)]
        RecyclerView StatisticRecyclerView;

        public override  void OnViewCreated(View view, Bundle savedInstanceState)
        {
            try
            {
                base.OnViewCreated(view, savedInstanceState);
                Activity.RunOnUiThread(async () =>
                {
                    Startloading(true);
                    var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetFinalQuestionResult();


                    var adapter = new QuestionStatisticAdapter((Result as IEnumerable<IFinalQuestion>).ToList(), this);
                    StatisticRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
                    StatisticRecyclerView.SetAdapter(adapter);
                    StopLoading();
                });
            }
            catch(Exception ex)
            {

            }
        }

    }
}