using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Helpers;

namespace Izrune.Adapters.RecyclerviewAdapters
{


    public class MenuViewHolder: RecyclerView.ViewHolder
    {

        public ImageView Image { get; set; }
        public  TextView MenuText { get; set; }
        public LinearLayout Item { get; set; }

        public MenuViewHolder(View view) : base(view)
        {
            Image = view.FindViewById<ImageView>(Resource.Id.MenuIcon);
            MenuText = view.FindViewById<TextView>(Resource.Id.MenuItemTExt);
            Item = view.FindViewById<LinearLayout>(Resource.Id.MenuItemContainer);
        }

    }


    class MenuRecyclerAdapter : RecyclerView.Adapter
    {
        private List<MenuItemClass> Menulist;

        public Action<int> OnItemClick { get; set; }

        public MenuRecyclerAdapter(List<MenuItemClass> lst)
        {
            Menulist = lst;
        }


        public override int ItemCount => Menulist.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var Holder = (holder as MenuViewHolder);

            Holder.Image.SetBackgroundResource(Menulist.ElementAt(position).Image);
            Holder.MenuText.Text = Menulist.ElementAt(position).MenuTitle;

            Holder.Item.Click += (s, e) =>
            {

                OnItemClick?.Invoke(position);

                Holder.Image.SetColorFilter(Color.Argb(100,203, 135, 214));
                Holder.MenuText.SetTextColor(Color.Argb(100, 203, 135, 214));


            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var Result = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemMenu, parent, false);
            var Layout = new MenuViewHolder(Result);

            return Layout;
        }
    }
}