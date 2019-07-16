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
using Izrune.ViewHolders;
using IZrune.PCL.Abstraction.Models;

namespace Izrune.Adapters.RecyclerviewAdapters
{
    class PaymentRecyclerAdapter : RecyclerView.Adapter
    {

        private List<IPaymentHistory> PaymentList;

        public PaymentRecyclerAdapter(List<IPaymentHistory> lst)
        {
            PaymentList = lst;
        }


        public override int ItemCount  => PaymentList.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var Holder = (holder as PaymentHistoryVieHolder);

            Holder.Name.Text = PaymentList.ElementAt(position).StudentName;

            Holder.Date.Text = PaymentList.ElementAt(position).Date.Value.ToShortDateString();

            Holder.Amount.Text = $"{PaymentList.ElementAt(position).Amount} ₾";


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var Result = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemPaymentHistory, parent, false);


            return new PaymentHistoryVieHolder(Result);


        }
    }
}