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
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class PromoFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.PromoLayout;

        [MapControl(Resource.Id.PromoEditText)]
        EditText promoEdit;

        [MapControl(Resource.Id.SubmitButton)]
        LinearLayout Submit;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public PromoFragment(string cod)
        {
            PromoCod = cod;
        }

        private string PromoCod;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);


            Submit.Click += (s, e) =>
            {
                if (promoEdit.Text == PromoCod && !string.IsNullOrEmpty(PromoCod))
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.izruneback);
                }
                else
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.RebButtonBackground);
                }
            };


        }
    }
}