using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace Izrune.Adapters.SpinerAdapter
{
   public class CategorySpinnerAdapter : ArrayAdapter<string>
    {
        List<string> items;

        Spinner Spinner;

        public CategorySpinnerAdapter(Context context, int textViewResourceId, IList<string> objects, Spinner spinner) : base(context, textViewResourceId, objects)
        {
            items = objects?.ToList();
            Spinner = spinner;
        }

        public override int Count => (items != null) ? items.Count : 0;

        public override long GetItemId(int position)
        {
            return position;
        }

        Drawable TopDrawable = null;
        Drawable BottomDrawable = null;

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var LayoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                convertView = LayoutInflater.Inflate(Resource.Layout.itemSpinnerText, parent, false);
            }

            if (TopDrawable == null)
            {
                TopDrawable = ContextCompat.GetDrawable(Context, Resource.Drawable.spinnerTopItemBackground)?.Mutate();
            }
            if (BottomDrawable == null)
            {
                BottomDrawable = ContextCompat.GetDrawable(Context, Resource.Drawable.spinnerBorromItemBackground)?.Mutate();
            }

            Drawable backgroundDrawable = null;

            if (position == 0)
            {
                var density = Context.Resources.DisplayMetrics.Density;
                convertView.SetPadding((int)(8 * density), (int)(12 * density), (int)(8 * density), (int)(12 * density));
            }

            if (position == 0 || position == items.Count - 1)
            {
                backgroundDrawable = (position == 0) ? TopDrawable : (position == items.Count - 1) ? BottomDrawable : null;
            }

            TextView text = convertView.FindViewById<TextView>(Resource.Id.spinnerText);

            if (position == Spinner.SelectedItemPosition)
            {
                if (backgroundDrawable != null)
                {
                  
                    convertView.Background = backgroundDrawable;
                }
                else
                {
                    convertView.SetBackgroundColor( Android.Graphics.Color.ParseColor("#004655") );
                }
                text.SetTextColor(Android.Graphics.Color.Black);
            }
            else
            {
                if (backgroundDrawable != null)
                {
                    backgroundDrawable.SetColorFilter( Android.Graphics.Color.ParseColor("#004655") , PorterDuff.Mode.SrcOver);
                    convertView.Background = backgroundDrawable;
                }
                else
                {
                    convertView.SetBackgroundColor( Android.Graphics.Color.ParseColor("#004655"));
                }
            }
            text.Text = items.ElementAt(position);
            return convertView;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var LayoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                convertView = LayoutInflater.Inflate(Resource.Layout.itemSpinnerText, parent, false);
            }
            var density = Context.Resources.DisplayMetrics.Density;
            convertView.SetPadding((int)(8 * density), (int)(12 * density), (int)(8 * density), (int)(12 * density));
            TextView text = convertView.FindViewById<TextView>(Resource.Id.spinnerText);
            text.Text = items.ElementAt(position);
            text.SetTextColor(Android.Graphics.Color.Black);
            return convertView;
        }
    }
}