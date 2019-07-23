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
using Izrune.Activitys;
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class SaccesFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutSucces;

        [MapControl(Resource.Id.DoneButton)]
        LinearLayout DoneButton;

        [MapControl(Resource.Id.DoneTExt)]
        TextView DoneText;
        

        public bool IsPasword { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (IsPasword)
            {
                DoneText.Text = "მომხმარებლის პაროლი გაგზავნილია რეგისტრაციის დროს მითითებულ ტელეფონის ნომერზე";
            }
            


            DoneButton.Click += (s, e) =>
            {
                (Activity as ForgotPasswordOrUserNameActivity).OnBackPressed();
            };
        }


    }
}