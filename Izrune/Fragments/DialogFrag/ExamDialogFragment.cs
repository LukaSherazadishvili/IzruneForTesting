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
    class ExamDialogFragment:DialogFragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.AlertExam, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var CloseImage = view.FindViewById<ImageView>(Resource.Id.CloseButton);
            CloseImage.Click += (s, e) =>
            {

                this.Dismiss();
            };

        }
    }
}