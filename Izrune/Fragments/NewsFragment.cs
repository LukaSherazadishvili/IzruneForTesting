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

namespace Izrune.Fragments
{
    class NewsFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutNews;

        [MapControl(Resource.Id.NewsRecyclerView)]
        RecyclerView NewsRecyclerView;

        private List<TestNews> NewsList = new List<TestNews>()
        {
            new TestNews(){ImageId=Resource.Drawable.axalcixeizrune},
             new TestNews(){ImageId=Resource.Drawable.chldimg},
              new TestNews(){ImageId=Resource.Drawable.chldimg},
               new TestNews(){ImageId=Resource.Drawable.axalcixeizrune},
                new TestNews(){ImageId=Resource.Drawable.chldimg},
                 new TestNews(){ImageId=Resource.Drawable.chldimg},
                  new TestNews(){ImageId=Resource.Drawable.axalcixeizrune},
                   new TestNews(){ImageId=Resource.Drawable.chldimg},
                    new TestNews(){ImageId=Resource.Drawable.chldimg},
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var manager = new LinearLayoutManager(this);
            var Adapter = new NewsRecyclerAdapter(NewsList);
            NewsRecyclerView.SetLayoutManager(manager);
            NewsRecyclerView.SetAdapter(Adapter);
        }
    }
}