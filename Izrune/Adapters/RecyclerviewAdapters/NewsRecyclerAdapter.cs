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
using Izrune.testModels;
using Izrune.ViewHolders;

namespace Izrune.Adapters.RecyclerviewAdapters
{
    class NewsRecyclerAdapter : RecyclerView.Adapter
    {

        private List<TestNews> MyNewsList;

        public NewsRecyclerAdapter(List<TestNews> lst)
        {
            MyNewsList = lst;
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0 || position % 3 == 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public override int ItemCount => MyNewsList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is BigNewsViewHolder)
            {
                (holder as BigNewsViewHolder).Image.SetImageResource(MyNewsList.ElementAt(position).ImageId);
            }
            else if(holder is SmallNewsViewHolder)
            {
                (holder as SmallNewsViewHolder).Image.SetImageResource(MyNewsList.ElementAt(position).ImageId);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == 1)
            {
                var View = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemBigNews, parent, false);
                var Result = new BigNewsViewHolder(View);
                return Result;
            }
            else
            {
                var View = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemSmallNewsImageRight, parent, false);
                var Result = new SmallNewsViewHolder(View);
                return Result;
            }
        }
    }
}