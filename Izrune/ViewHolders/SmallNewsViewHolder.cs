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

namespace Izrune.ViewHolders
{
    class SmallNewsViewHolder:RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }

        public SmallNewsViewHolder(View view) : base(view)
        {
            Image = view.FindViewById<ImageView>(Resource.Id.MainSmallImage);
        }
    }
}