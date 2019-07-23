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
  public  class MainPageAlertDialog:DialogFragment
    {

        public Action ChangeFragment { get; set; }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            Dialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);

            return inflater.Inflate(Resource.Layout.AlertMainPage, container, false);
        }


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var Buton = view.FindViewById<FrameLayout>(Resource.Id.ChangePaymentButton);

            Buton.Click += (s, e) =>
            {

                ChangeFragment?.Invoke();
                this.Dismiss();
            };


            var CloseImage = view.FindViewById<ImageView>(Resource.Id.CloseButton);
            CloseImage.Click += (s, e) =>
            {

                this.Dismiss();
            };
        }

    }
}