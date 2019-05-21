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
    class ShedulerViewHolder:RecyclerView.ViewHolder
    {
        public FrameLayout ShedulerContainer { get; set; }
        public ImageView ShedulerImage { get; set; }

        public ShedulerViewHolder(View view) : base(view)
        {
            ShedulerContainer = view.FindViewById<FrameLayout>(Resource.Id.ShedulerContainer);
            ShedulerImage = view.FindViewById<ImageView>(Resource.Id.ShedulerImage);
        }
    }
}