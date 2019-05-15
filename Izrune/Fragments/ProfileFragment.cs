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
    class ProfileFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutChangeProfile;

        [MapControl(Resource.Id.ParentProfileEdit)]
        RelativeLayout ParentButton;

        [MapControl(Resource.Id.StudentProfileEdit)]
        RelativeLayout StudentEdit;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ParentButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(ParrentProfileEditActivity));
                StartActivity(intent);
            };

            StudentEdit.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(StudentProfileEditActivity));
                StartActivity(intent);
            };
        }
    }
}