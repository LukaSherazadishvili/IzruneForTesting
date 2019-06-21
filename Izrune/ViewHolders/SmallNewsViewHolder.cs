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

namespace Izrune.ViewHolders
{
    class SmallNewsViewHolder:RecyclerView.ViewHolder
    {
        public ImageViewAsync Image { get; set; }
        public TextView SmallDate { get; set; }
        public TextView SmallTitle { get; set; }
        public LinearLayout Container { get; set; }


        public SmallNewsViewHolder(View view) : base(view)
        {
            Image = view.FindViewById<ImageViewAsync>(Resource.Id.MainSmallImage);
            SmallDate = view.FindViewById<TextView>(Resource.Id.SmaillNewsDate);
            SmallTitle = view.FindViewById<TextView>(Resource.Id.SmallNewsTitle);
            Container = view.FindViewById<LinearLayout>(Resource.Id.MainLinearContainer);
        }
    }
}