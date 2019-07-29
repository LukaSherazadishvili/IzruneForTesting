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
    public class BadgesRecyclerViewAdapter : RecyclerView.Adapter
    {

        private List<IBadges> BadagesList;

        public BadgesRecyclerViewAdapter(List<IBadges> lst)
        {
            BadagesList = lst;

        }

        public override int ItemCount => BadagesList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

         (holder as BadgesViewHolder).BadgesImage.LoadImage(BadagesList.ElementAt(position).ImageURl);

            (holder as BadgesViewHolder).BadgesImage.Click += (s, e) =>
            {
                //var uri = Android.Net.Uri.Parse("https://www.facebook.com/izrune.ge/");
                //var intent = new Intent(Intent.ActionView, uri);
                //StartActivity(intent);

            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemBadges, parent, false);

            return new BadgesViewHolder(view);

        }
    }

    
}