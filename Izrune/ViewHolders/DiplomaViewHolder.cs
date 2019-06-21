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
   public class DiplomaViewHolder:RecyclerView.ViewHolder
    {
       public TextView DateText { get; set; }
        

        public DiplomaViewHolder(View view) : base(view)
        {
            DateText = view.FindViewById<TextView>(Resource.Id.DateTittle);
        }
    }
}