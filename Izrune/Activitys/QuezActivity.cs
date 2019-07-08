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
using Com.Airbnb.Lottie;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Fragments;
using Izrune.Fragments.DialogFrag;
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

        [MapControl(Resource.Id.StudenQuesName)]
        TextView StudentName;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.LikeLottie)]
        LottieAnimationView likesLottie;

       

        int CircProgress = 0;
        int EndProgress = 90;
        int Progr = 90;
        int Sec = 30;
        int minit = 1;


      
        private int CurrentTime=0;

        private int Position = 0;

        private List<QuestionShedule> Sheduler = new List<QuestionShedule>();

        private IEnumerable<IQuestion> QuestionsList;

        string TimeType;
        string ExamType;
        protected async  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



              ExamType = Intent.GetStringExtra("ExamType");
             TimeType = Intent.GetStringExtra("TimeType");

            if (TimeType == "1")
            {
                CurrentTime = 1799;
                
            }
            else
            {
                CurrentTime = 90;
            }

            
            int CircProgress = 0;
            int EndProgress =CurrentTime;
            int Progr =CurrentTime;
            int Sec = CurrentTime%60;
            int minit = CurrentTime/60;


            if (ExamType == "1")
            {
                QuestionsList = await QuezControll.Instance.GetAllQuestion(IZrune.PCL.Enum.QuezCategory.QuezExam);
            }
            else
            {
                QuestionsList = await QuezControll.Instance.GetAllQuestion(IZrune.PCL.Enum.QuezCategory.QuezTest);
            }



            progBar.Max = CurrentTime;
            progBar.SecondaryProgress = 0;

           




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


            StudentName.Text =  UserControl.Instance.CurrentStudent.Name +" "+ UserControl.Instance.CurrentStudent.LastName;


                
            var FragmentQuestion = new QuezFragment(QuezControll.Instance.GetCurrentQuestion(),ExamType);

            FragmentQuestion.AnswerClick = () =>
            {
                
                Position++;
                if (TimeType != "1")
                {
                    Progr = CurrentTime;
                    Sec = CurrentTime % 60;
                    minit = CurrentTime / 60;
                    CircProgress = 0;
                    progBar.Progress = 0;
                }
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

                    if (Progr == 0 && TimeType == "1" && Position < 19)
                    {
                        OnBackPressed();
                    }
                    if (Progr == 0)
                    {

                        Position++;
                        if (TimeType != "1")
                        {
                            Progr = CurrentTime;
                            Sec = CurrentTime % 60;
                            minit = CurrentTime / 60;
                            CircProgress = 0;
                            progBar.Progress = 0;
                        }
                       await QuezControll.Instance.AddQuestion();
                        var frg = new QuezFragment(QuezControll.Instance.GetCurrentQuestion(),ExamType);
                        frg.ChangeResultPage = () =>
                        {
                            ChangeFragmentPage(new DiplomaFragment(), Resource.Id.MainFuckingContainer);
                        };
                        frg.AnswerClick = () =>
                        {
                            Position++;
                            if (TimeType != "1")
                            {
                                Progr = CurrentTime;
                                Sec = CurrentTime % 60;
                                minit = CurrentTime / 60;
                                CircProgress = 0;
                                progBar.Progress = 0;
                            }
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

            BackButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }

        public void OpenDialog(string ImageUrl)
        {
            var transcation = FragmentManager.BeginTransaction();
            ImageDialogFragment dialog = new ImageDialogFragment(ImageUrl);
            dialog.Show(transcation, "Image Dialog");
        }

        public void PlayAnimation()
        {
            likesLottie.Visibility = ViewStates.Visible;
            likesLottie.PlayAnimation();
        }

        public void StopAnimation()
        {
            likesLottie.Visibility = ViewStates.Gone;
            
        }

    }
}