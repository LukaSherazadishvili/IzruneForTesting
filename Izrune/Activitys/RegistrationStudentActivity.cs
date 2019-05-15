﻿using System;
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

namespace Izrune.Activitys
{

    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class RegistrationStudentActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutRegistrationStudentFirst;


        [MapControl(Resource.Id.Container)]
        FrameLayout Container;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            NextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {

            
           


            Intent intent = new Intent(this,typeof(NextRegistrationStudentActivity));
            StartActivity(intent);
        }

        
    }
}