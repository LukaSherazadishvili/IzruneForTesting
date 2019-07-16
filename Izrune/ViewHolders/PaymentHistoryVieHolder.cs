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
     class PaymentHistoryVieHolder:RecyclerView.ViewHolder
    {

       public TextView Name { get; set; }

        public TextView Date { get; set; }

        public TextView Amount { get; set; }

        public PaymentHistoryVieHolder(View view) : base(view)
        {

            Name = view.FindViewById<TextView>(Resource.Id.NameTxt);

            Date = view.FindViewById<TextView>(Resource.Id.TimeTxt);

            Amount = view.FindViewById<TextView>(Resource.Id.AmountTxt);

        }

    }
}