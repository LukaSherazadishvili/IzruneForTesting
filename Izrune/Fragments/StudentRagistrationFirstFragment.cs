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
    class StudentRagistrationFirstFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutRegistrationStudentFirst;

        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;
       
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            NextButton.Click += (s, e) =>
            {
                ChangeFragmentPage(new ContinueRegistrationStudent(), Container.Id,false);
            };
        }
    }
}