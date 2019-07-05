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

namespace Izrune.Fragments.DialogFrag
{
    class warningDialogFragment:DialogFragment
    {
        private string Title { get; set; }
        private string Text { get; set; }

        public warningDialogFragment(string title,string text)
        {
            Title = title;
            Text = text;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.LayoutAlert, container, false);


        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            view.FindViewById<TextView>(Resource.Id.DialogTitle).Text = Title;
            view.FindViewById<TextView>(Resource.Id.DialogText).Text = Text;

        }
    }
}