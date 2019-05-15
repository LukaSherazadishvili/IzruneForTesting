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
    class ContinueRegistrationParrentFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutNextRegistrationParrent;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //NextButton.Click += (s, e) =>
            //{
            //    ChangeFragmentPage(new ContinueRegistrationParrent(), Container.Id);

            //};


        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            NextButton.Click += (s, e) =>
            {
                ChangeFragmentPage(new StudentRagistrationFirstFragment(), Container.Id);

            };
        }
    }
}