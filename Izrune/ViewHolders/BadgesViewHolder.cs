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
   public class BadgesViewHolder:RecyclerView.ViewHolder
    {

        public ImageViewAsync BadgesImage { get; set; }


        public BadgesViewHolder(View view) : base(view)
        {
            BadgesImage = view.FindViewById<ImageViewAsync>(Resource.Id.BadgesImages);
        }
    }
}