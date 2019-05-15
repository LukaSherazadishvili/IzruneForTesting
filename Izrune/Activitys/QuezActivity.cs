using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using Izrune.Fragments;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class QuezActivity : MPDCBaseActivity
    {

       

        protected override int LayoutResource { get; } = Resource.Layout.layoutQuez;

        [MapControl(Resource.Id.QuestionProgressbar)]
         ProgressBar progBar;


        [MapControl(Resource.Id.TimerText)]
        TextView TimerTxt;

        int CircProgress = 0;
        int EndProgress = 90;
        int Progr = 90;
        int Sec = 30;
        int minit = 1;


        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progBar.Max = 90;
            progBar.SecondaryProgress = 0;

            ChangeFragmentPage(new QuezFragment(), Resource.Id.ContainerQuestion);



            Task.Run(async() => {

                while (CircProgress < EndProgress)
                {
                    CircProgress += 1;
                    progBar.Progress = CircProgress;
                   
                    await Task.Delay(1000);
                }
               
            });

            RunOnUiThread(async () => {
                while (Progr > 0)
                {
                    Progr--;
                    Sec--;
                    if (Sec == 0)
                    {
                        if (minit != 0)
                        {
                            minit--;
                            Sec = 60;
                        }
                    }
                    

                    TimerTxt.Text = string.Format($"{minit.ToString().PadLeft(2, '0')}:{Sec.ToString().PadLeft(2, '0')}");
                    await Task.Delay(1000);
                }
            });
        }

    }
}