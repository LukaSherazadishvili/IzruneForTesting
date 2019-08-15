using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using FFImageLoading.Views;
using Izrune.Activitys;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class InnerResultStatisticFragment:MPDCBaseFragment
    {

        protected override int LayoutResource { get; } = Resource.Layout.LayoutDiplomi;

        [MapControl(Resource.Id.Diplom)]
        ImageViewAsync DiplomaImage;

        [MapControl(Resource.Id.StudentName)]
        TextView StudentNameLastName;

        [MapControl(Resource.Id.StarsContainer)]
        LinearLayout StarsContainer;

        [MapControl(Resource.Id.ScoreTxt)]
        TextView Score;

        [MapControl(Resource.Id.TimeTxt)]
        TextView Time;

        [MapControl(Resource.Id.CorrectAnswerTxt)]
        TextView Correctanswers;

        [MapControl(Resource.Id.IncorrectAnswerCountTxt)]
        TextView IncorectAnswers;

        [MapControl(Resource.Id.SkippedAnswerCount)]
        TextView SkippedAnswer;

        [MapControl(Resource.Id.StarsContainer)]
        LinearLayout starsContr;

        [MapControl(Resource.Id.PointText)]
        TextView PointTxt;

        [MapControl(Resource.Id.MarkTXt)]
        TextView Mark;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        [MapControl(Resource.Id.EGmuImage)]
        ImageView EgmuImage;

        [MapControl(Resource.Id.BadgesRecyclerView)]
        RecyclerView BadgesRecycler;

        [MapControl(Resource.Id.FireWorckLottie)]
        LottieAnimationView Fireworck;


        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }

        public IQuisResultInfo Result { get; set; }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
      

            Startloading();

             Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetCurrentTestDiplomaInfo(IzruneHellper.Instance.CurrentStatistic.Id);

            var Res = await MpdcContainer.Instance.Get<IUserServices>().GetBadgesAsync();

            if (Res.Count() > 0)
            {
                LinearLayoutManager bManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                var badapter = new BadgesRecyclerViewAdapter(Res?.ToList())
                {
                    OnBadgetClick = () => {

                        (Activity as InnerDiplomaStatisticActivity).OnBadgetClick();
                    }
                };
                BadgesRecycler.SetLayoutManager(bManager);
                BadgesRecycler.SetAdapter(badapter);
            }
            else
                BadgesRecycler.Visibility = ViewStates.Gone;


            // var QuisInfo = Result.QueisResult;

            StudentNameLastName.Text = $"{UserControl.Instance.CurrentStudent.Name}  {UserControl.Instance.CurrentStudent.LastName}";

            var EgmuUrl = await MpdcContainer.Instance.Get<IQuezServices>().GetEgmuAsync(IzruneHellper.Instance.CurrentStatistic.Id);

            Score.Text = Result.Score.ToString();

            Time.Text = string.Format($"{(Result.Duration / 60).ToString().PadLeft(2, '0')}:{(Result.Duration % 60).ToString().PadLeft(2, '0')}");
            Correctanswers.Text = IzruneHellper.Instance.CurrentStatistic.CorrectAnswersCount.ToString();
            IncorectAnswers.Text = IzruneHellper.Instance.CurrentStatistic.IncorrectAnswersCount.ToString();
            SkippedAnswer.Text = IzruneHellper.Instance.CurrentStatistic.SkippedQuestionsCount.ToString();
            PointTxt.Text = Result.text_title;
            Mark.Text = Result.text_description;
            if (!string.IsNullOrEmpty(IzruneHellper.Instance.CurrentStatistic.DiplomaUrl))
            {
                DiplomaImage.Visibility = ViewStates.Visible;
                DiplomaImage.LoadImage(IzruneHellper.Instance.CurrentStatistic.DiplomaUrl);
              
            }

            if (Result.Stars == 5)
            {
                Fireworck.Visibility = ViewStates.Visible;
                Fireworck.PlayAnimation();
            }
            else
            {
                Fireworck.Visibility = ViewStates.Gone;
            }

            for (int i = 0; i < 5; i++)
            {

                FrameLayout.LayoutParams imgViewParams = new FrameLayout.LayoutParams(110, 110, GravityFlags.CenterHorizontal);
                imgViewParams.SetMargins(15, 10, 15, 10);
                var Image = new ImageView(this);
                Image.LayoutParameters = imgViewParams;

                Image.SetBackgroundResource(i < Result.Stars ? Resource.Drawable.ActiveStar : Resource.Drawable.PasiveStar);
                starsContr.AddView(Image);


            }

            EgmuImage.Click +=async (s, e) =>
            {
               
              (Activity as InnerDiplomaStatisticActivity).OnEgmuClick(EgmuUrl);
            };

            StopLoading();

        }

       


       
    }
}