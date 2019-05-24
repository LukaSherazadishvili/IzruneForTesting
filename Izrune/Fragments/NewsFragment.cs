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
using Izrune.testModels;
using IZrune.PCL.Abstraction.Services;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Fragments
{
    class NewsFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutNews;

        [MapControl(Resource.Id.NewsRecyclerView)]
        RecyclerView NewsRecyclerView;

      

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }
        public async override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var Result = await MpdcContainer.Instance.Get<INewsService>().GetNewsAsync();

            var manager = new LinearLayoutManager(this);
            var Adapter = new NewsRecyclerAdapter(Result?.ToList());
            NewsRecyclerView.SetLayoutManager(manager);
            NewsRecyclerView.SetAdapter(Adapter);
        }
    }
}