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

namespace Izrune.Adapters.RecyclerviewAdapters
{
    public class ShedulerRecyclerAdapter : RecyclerView.Adapter
    {
       private List<QuestionShedule> ShedulerList;

        public ShedulerRecyclerAdapter(List<QuestionShedule> lst)
        {
            ShedulerList = lst;
        }

        public override int ItemCount => ShedulerList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            (holder as ShedulerViewHolder).ShedulerText.Text = ShedulerList.ElementAt(position).Position.ToString();

            if (!ShedulerList.ElementAt(position).IsCurrent)
            {
                //  (holder as ShedulerViewHolder).ShedulerImage.Visibility = ViewStates.Invisible;
                (holder as ShedulerViewHolder).ShedulerText.SetTextColor(Android.Graphics.Color.Black);
                  (holder as ShedulerViewHolder).ShedulerContainer.SetBackgroundResource(Resource.Drawable.UnselectedSheduleBack);
            }          
            else
            {
                // (holder as ShedulerViewHolder).ShedulerImage.Visibility = ViewStates.Visible;
                (holder as ShedulerViewHolder).ShedulerText.SetTextColor(Android.Graphics.Color.White);
                (holder as ShedulerViewHolder).ShedulerContainer.SetBackgroundResource(Resource.Drawable.SheduleBackground);
            }

            if (ShedulerList.ElementAt(position).AlreadeBe == true&& ShedulerList.ElementAt(position).IsCurrent == false)
            {
                // (holder as ShedulerViewHolder).ShedulerImage.Visibility = ViewStates.Invisible;
                (holder as ShedulerViewHolder).ShedulerText.SetTextColor(Android.Graphics.Color.White);
                (holder as ShedulerViewHolder).ShedulerContainer.SetBackgroundResource(Resource.Drawable.SheduleBackground);
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemShedule, parent, false);
            var Result = new ShedulerViewHolder(view);
            return Result;
        }
    }
}