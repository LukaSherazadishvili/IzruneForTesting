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
    class SchoolAlert:DialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            Dialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);

            return inflater.Inflate(Resource.Layout.SchoolAlert, container, false);
        }

    }
}