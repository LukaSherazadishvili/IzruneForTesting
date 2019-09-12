using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
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
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class QuezActivity : MPDCBaseActivity
    {

       

        protected override int LayoutResource { get; } = Resource.Layout.layoutQuez;

        [MapControl(Resource.Id.MainFuckingContainer)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.QuestionProgressbar)]
         ProgressBar progBar;

        [MapControl(Resource.Id.TimerText)]
        TextView TimerTxt;

        [MapControl(Resource.Id.QuestionCountRecycler)]
        RecyclerView ShedulRecycler;

        [MapControl(Resource.Id.StudenQuesName)]
        TextView StudentName;

        [MapControl(Resource.Id.BadgesRecyclerView)]
        RecyclerView BadgesRecycler;



        [MapControl(Resource.Id.LikeLottie)]
        LottieAnimationView likesLottie;

       

        int CircProgress = 0;
        int EndProgress = 90;
        int Progr;
        int Sec ;
        int minit;


      
        private int CurrentTime=0;

        private int Position = 0;

        private List<QuestionShedule> Sheduler = new List<QuestionShedule>();

        private IEnumerable<IQuestion> QuestionsList;

        string TimeType;
        string ExamType;
        ShedulerRecyclerAdapter Adapter;
        protected   override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

          
               

               

              //  Startloading();

                var Res = await MpdcContainer.Instance.Get<IUserServices>().GetBadgesAsync();

                
                if (Res?.Count() > 0)
                {
                    LinearLayoutManager bManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                    var badapter = new BadgesRecyclerViewAdapter(Res?.ToList()) { OnBadgetClick=()=> {

                        var uri = Android.Net.Uri.Parse("http://www.izrune.ge/geo/175");
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);

                    }
                    };
                    BadgesRecycler.SetLayoutManager(bManager);
                    BadgesRecycler.SetAdapter(badapter);
                }
                else
                    BadgesRecycler.Visibility = ViewStates.Gone;

               // StopLoading();



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





                 CircProgress = 0;
                 EndProgress = CurrentTime;
                 Progr = CurrentTime;
                 Sec = CurrentTime % 60;
                 minit = CurrentTime / 60;


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






                for (int i = 1; i < QuestionsList.Count() + 1; i++)
                {
                    QuestionShedule shed = new QuestionShedule() { IsCurrent = false, Position = i };
                    Sheduler.Add(shed);
                }


                Sheduler.ElementAt(Position).IsCurrent = true;
                Sheduler.ElementAt(Position).AlreadeBe = true;
                LinearLayoutManager manager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                ShedulRecycler.SetLayoutManager(manager);
                 Adapter = new ShedulerRecyclerAdapter(Sheduler);
                ShedulRecycler.SetAdapter(Adapter);


                StudentName.Text = UserControl.Instance.CurrentStudent.Name + " " + UserControl.Instance.CurrentStudent.LastName;



                var FragmentQuestion = new QuezFragment(QuezControll.Instance.GetCurrentQuestion(), ExamType);
                    CurrentFragment = FragmentQuestion;
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







                    StartTimer();

            //    StopLoading();



            }
            catch (Exception ex)
            {
               // Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
              //  this.Finish();
            }
        }


        private QuezFragment CurrentFragment;
        private  void StartTimer()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    while (Progr > 0 && Position < 20)
                    {
                        if (IsOnbackPressed)
                            break;

                        if (IsOnPaused)
                            break;

                            //    ChangeTime(ref minit, ref Sec);
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
                            CurrentFragment.CheckQuestion();
                            await Task.Delay(1500);

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
                            var frg = new QuezFragment(QuezControll.Instance.GetCurrentQuestion(), ExamType);
                            CurrentFragment = frg;
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
            }
            catch(Exception ex)
            {

            }
        }





        DateTime PausedTime;
        bool IsOnPaused = false;
        bool IsOnbackPressed = false;
        protected override void OnPause()
        {
            base.OnPause();
          
            if (!IsOnbackPressed)
            {
                IsOnPaused = true;
                var sss = Progr;
                IsOpenedFromPaused = true;
           
                PausedTime = DateTime.Now;
            }

          
        }

        bool IsOpenedFromPaused = false;
        private TimeSpan ResuMeTime;
        protected override void OnResume()
        {
            try
            {
                base.OnResume();
                if (IsOpenedFromPaused)
                {
                    IsOnPaused = false;
                    ComeFromPause = true;
                    ResuMeTime = DateTime.Now.Subtract(PausedTime);

                    ChangeTime();

                    //   StartTimer();

                }
            }
            catch(Exception ex)
            {

            }
        }

        bool ComeFromPause = false;
        public  void ChangeTime()
        {
            try
            {
                if (ComeFromPause)
                {
                    ComeFromPause = false;

                    if (TimeType == "1")
                    {

                        if (ResuMeTime.Seconds > Sec)
                        {
                            minit--;
                            Sec = 60 - (ResuMeTime.Seconds - Sec);
                        }
                        else
                            Sec -= ResuMeTime.Seconds - 1;

                        if (!((minit - ResuMeTime.Minutes) < 0))
                        {
                            minit -= ResuMeTime.Minutes;
                        }
                        else
                        {
                            OnBackPressed();
                        }
                        StartTimer();
                    }
                    else
                    {
                        RunOnUiThread(async () =>
                        {
                            if (!(ResuMeTime.Hours > 0))
                            {

                                var TotalTimeInSecond = (ResuMeTime.Minutes * 60) + ResuMeTime.Seconds;

                                if (TotalTimeInSecond > 1800)
                                {
                                    OnBackPressed();
                                    return;
                                }
                                var SkippedQuestionCount = 0;

                                if (TotalTimeInSecond > ((minit * 60) + Sec))
                                {
                                    SkippedQuestionCount++;

                                    SkippedQuestionCount += (TotalTimeInSecond - ((minit * 60) + Sec)) / 90;

                                    if (TotalTimeInSecond < 90)
                                        Sec = (90 - (TotalTimeInSecond - ((minit * 60) + Sec))) % 90;
                                    else
                                        Sec = 90 - ((TotalTimeInSecond - ((minit * 60) + Sec)) % 90);
                                    //   Progr = TotalTimeInSecond;

                                    CircProgress = 90 - Sec;
                                    Progr = Sec;
                                    minit = Sec / 60;

                                    Sec %= 60;



                                }
                                else
                                {





                                    var CurrentTimeInSecond = (minit * 60) + Sec;



                                    CurrentTimeInSecond = CurrentTimeInSecond - TotalTimeInSecond;
                                    Progr = CurrentTimeInSecond;
                                    CircProgress = 90 - CurrentTimeInSecond;

                                    Sec = CurrentTimeInSecond % 60;
                                    minit = CurrentTimeInSecond / 60;

                                }

                                Position += SkippedQuestionCount;
                                if (Position > 20)
                                {
                                    OnBackPressed();
                                    return;
                                }

                                if (SkippedQuestionCount > 0)
                                {
                                    for (int i = 0; i <= Position; i++)
                                    {
                                        if (Position < 20)
                                        {
                                            foreach (var items in Sheduler)
                                            {
                                                items.IsCurrent = false;
                                            }
                                            Sheduler.ElementAt(i).AlreadeBe = true;
                                            Sheduler.ElementAt(i).IsCurrent = true;
                                            Adapter.NotifyDataSetChanged();
                                            if (Position <= 18)
                                                ShedulRecycler.ScrollToPosition(i + 1);
                                            else
                                                ShedulRecycler.ScrollToPosition(i);


                                        }
                                        Startloading(true);
                                        await QuezControll.Instance.AddQuestion();
                                        StopLoading();
                                    }

                                    var frg = new QuezFragment(QuezControll.Instance.GetCurrentQuestion(), ExamType);
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


                                    ChangeFragmentPage(frg, Resource.Id.ContainerQuestion);

                                }
                                StartTimer();



                            }
                            else
                            {
                                OnBackPressed();
                            }
                        });
                    }

                }
            }
            catch(Exception ex)
            {

            }
        }


        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainPageAtivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);  
            UserControl.Instance.Resetregistration();
            StartActivity(intent);
            this.Finish();
        }

        public void OpenDialog(string ImageUrl)
        {
            try
            {
                var transcation = FragmentManager.BeginTransaction();
                ImageDialogFragment dialog = new ImageDialogFragment(ImageUrl);
                dialog.Show(transcation, "Image Dialog");
            }
            catch(Exception ex)
            {
               
            }
        }

        public void PlayAnimation( )
        {
          
               likesLottie.SetAnimation("like.json");
                likesLottie.Visibility = ViewStates.Visible;
                likesLottie.PlayAnimation();
        }

        public void PlayAnimationTwo()
        {
            
                likesLottie.SetAnimation("sssss.json");
            likesLottie.Visibility = ViewStates.Visible;
            likesLottie.PlayAnimation();
        }

        //public void StopAnimation()
        //{
        //    likesLottie.Visibility = ViewStates.Gone;
            
        //}



    }
}