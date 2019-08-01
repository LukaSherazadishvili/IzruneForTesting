using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Izrune.Adapters.SpinerAdapter
{
    class MySpinnerAdapter : BaseAdapter<string>
    {
        readonly LayoutInflater inflater;
        List<string> MyListString;

        public Action<int> OnSpinerClick { get; set; }

        public MySpinnerAdapter(Context context, List<string> list)
        {
            MyListString = list;
            inflater = LayoutInflater.FromContext(context);
        }

        public override string this[int position] =>MyListString[position];

        public override int Count => MyListString.Count;

        public override long GetItemId(int position)
        {
            return position;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return MyListString.ElementAt(position);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? inflater.Inflate(Resource.Layout.itemSpinnerText, parent, false);

            view.FindViewById<TextView>(Resource.Id.spinnerText).Text = MyListString.ElementAt(position);

            //view.FindViewById<FrameLayout>(Resource.Id.SpinderContainer).Click+= (s, e) =>
            //{
            //    OnSpinerClick?.Invoke(position);
            //};

          

            return view;
        }
    }
}