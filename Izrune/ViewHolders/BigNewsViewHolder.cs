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
    class BigNewsViewHolder:RecyclerView.ViewHolder
    {
        public ImageViewAsync Image { get; set; }
        public TextView Date { get; set; }
        public TextView Title { get; set; }
        public BigNewsViewHolder(View view) : base(view)
        {
            Image = view.FindViewById<ImageViewAsync>(Resource.Id.MainImage);
            Date = view.FindViewById<TextView>(Resource.Id.BigNewsDatetime);
            Title = view.FindViewById<TextView>(Resource.Id.BigNewsTitle);

        }
    }
}