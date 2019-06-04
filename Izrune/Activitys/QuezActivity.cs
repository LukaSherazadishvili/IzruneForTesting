using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Fragments;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
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

       

        [MapControl(Resource.Id.QuestionCountRecycler)]
        RecyclerView ShedulRecycler;


        

       

        int CircProgress = 0;
        int EndProgress = 90;
        int Progr = 90;
        int Sec = 30;
        int minit = 1;


        private List<IQuestion> QuestionsList;


        private int Position = 0;

        private List<QuestionShedule> Sheduler = new List<QuestionShedule>();


        protected async  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progBar.Max = 90;
            progBar.SecondaryProgress = 0;
          
           
           

            var QuestionsList =await QuezControll.Instance.GetAllQuestion(IZrune.PCL.Enum.QuezCategory.QuezExam);


            for (int i = 1; i <QuestionsList.Count()+1; i++)
            {
                QuestionShedule shed = new QuestionShedule() { IsCurrent = false, Position = i };
                Sheduler.Add(shed);
            }


            Sheduler.ElementAt(Position).IsCurrent = true;
            Sheduler.ElementAt(Position).AlreadeBe = true;
            LinearLayoutManager manager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            ShedulRecycler.SetLayoutManager(manager);
            var Adapter = new ShedulerRecyclerAdapter(Sheduler);
            ShedulRecycler.SetAdapter(Adapter);

           


                
                var FragmentQuestion = new QuezFragment(QuezControll.Instance.GetCurrentQuestion());

            FragmentQuestion.AnswerClick = () =>
            {
                Position++;
                Progr = 90;
                Sec = 30;
                minit = 1;
                CircProgress = 0;
                progBar.Progress = 0;

                if (Position < 20)
                {
                    foreach (var items in Sheduler)
                    {
                        items.IsCurrent = false;
                    }
                    Sheduler.ElementAt(Position).AlreadeBe = true;
                    Sheduler.ElementAt(Position).IsCurrent = true;
                    Adapter.NotifyDataSetChanged();
                    if (Position <= 18)
                        ShedulRecycler.ScrollToPosition(Position + 1);
                    else
                        ShedulRecycler.ScrollToPosition(Position);
                }
                return QuezControll.Instance.GetCurrentQuestion();
            };
            FragmentQuestion.ChangeResultPage = () =>
            {
                ChangeFragmentPage(new DiplomaFragment(), Resource.Id.MainFuckingContainer);
            };

            ChangeFragmentPage(FragmentQuestion, Resource.Id.ContainerQuestion);

            #region end
            //RunOnUiThread(async () =>
            //{
            //    while (CircProgress < EndProgress)
            //    {

            //        progBar.Progress = CircProgress++;

            //        await Task.Delay(1000);
            //    }

            //});

            //Task.Run(async() => {


            //});
            #endregion




            RunOnUiThread(async () => {
                while (Progr > 0&&Position<20)
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
                    progBar.Progress = CircProgress++;
                    if (Progr == 0)
                    {

                        Position++;
                        Progr = 90;
                        Sec = 30;
                        minit = 1;
                        CircProgress = 0;
                        progBar.Progress = 0;

                        var frg = new QuezFragment(QuezControll.Instance.GetCurrentQuestion());
                        frg.ChangeResultPage = () =>
                        {
                            ChangeFragmentPage(new DiplomaFragment(), Resource.Id.MainFuckingContainer);
                        };
                        frg.AnswerClick = () =>
                        {
                            Position++;
                            Progr = 90;
                            Sec = 30;
                            minit = 1;
                            CircProgress = 0;
                            progBar.Progress = 0;

                            if (Position < 20)
                            {
                                foreach (var items in Sheduler)
                                {
                                    items.IsCurrent = false;
                                }

                                Sheduler.ElementAt(Position).AlreadeBe = true;
                                Sheduler.ElementAt(Position).IsCurrent = true;
                                Adapter.NotifyDataSetChanged();
                                if (Position <= 18)
                                    ShedulRecycler.ScrollToPosition(Position + 1);
                                else
                                    ShedulRecycler.ScrollToPosition(Position);
                            }

                            return QuezControll.Instance.GetCurrentQuestion();
                        };








                        if (Position < 20)
                        {
                            foreach (var items in Sheduler)
                            {
                                items.IsCurrent = false;
                            }
                            Sheduler.ElementAt(Position).AlreadeBe = true;
                            Sheduler.ElementAt(Position).IsCurrent = true;
                            Adapter.NotifyDataSetChanged();
                            if (Position <= 18)
                                ShedulRecycler.ScrollToPosition(Position + 1);
                            else
                                ShedulRecycler.ScrollToPosition(Position);

                            ChangeFragmentPage(frg, Resource.Id.ContainerQuestion);
                        }
                    }

                    TimerTxt.Text = string.Format($"{minit.ToString().PadLeft(2, '0')}:{Sec.ToString().PadLeft(2, '0')}");
                    await Task.Delay(1000);

                    
                }
            });
        }



        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

    }
}