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
using Izrune.Helpers;
using Izrune.ViewHolders;
using IZrune.PCL.Abstraction.Models;

namespace Izrune.Adapters.RecyclerviewAdapters
{
    class DiplomaRecyclerViewAdapter : RecyclerView.Adapter
    {

        public List<IStudentsStatistic> StatisticList;

        public Action OnDiplomaClikc { get; set; }

        public DiplomaRecyclerViewAdapter(List<IStudentsStatistic> lst)
        {
            StatisticList = lst;
        }


        public override int ItemCount => StatisticList.Count;

        

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var Result = (holder as DiplomaViewHolder);
            Result.DateText.Text = StatisticList.ElementAt(position).ExamDate.ToShortDateString();

            Result.DateText.Click += (s, e) =>
            {
                IzruneHellper.Instance.CurrentStatistic = StatisticList.ElementAt(position);
                OnDiplomaClikc?.Invoke();


            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var Layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemDiploma, parent, false);
            DiplomaViewHolder holder = new DiplomaViewHolder(Layout);
            return holder;
        }
    }
}