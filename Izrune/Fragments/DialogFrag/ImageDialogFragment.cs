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
using FFImageLoading.Views;
using Izrune.Helpers;

namespace Izrune.Fragments.DialogFrag
{
    class ImageDialogFragment: DialogFragment
    {
        private string Image;

        public ImageDialogFragment(string ImageUrl)
        {
            Image = ImageUrl;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.LayoutDialogForImages, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var CarImage = view.FindViewById<ImageViewAsync>(Resource.Id.DialogImage);
            CarImage.LoadImage(Image);

        }


    }
}