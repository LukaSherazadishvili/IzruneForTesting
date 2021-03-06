﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false)]
    class DiplomaActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutInnerDiploma;

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


        [MapControl(Resource.Id.ShareButton)]
        LinearLayout ShareButton;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }



        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          

            try
            {
                Startloading();
                var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetCurrentTestDiplomaInfo(IzruneHellper.Instance.CurrentStatistic.Id);



             
                StudentNameLastName.Text = $"{UserControl.Instance.CurrentStudent.Name}  {UserControl.Instance.CurrentStudent.LastName}";

          

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
                    ShareButton.Visibility = ViewStates.Visible;
                    ShareButton.Click += (s, e) =>
                    {

                        var SharingIntent = new Intent();
                        SharingIntent.SetAction(Intent.ActionSend);
                        SharingIntent.SetType("text/plain");
                    //  SharingIntent.PutExtra(Intent.ExtraSubject, "Subject");
                    SharingIntent.PutExtra(Intent.ExtraText, IzruneHellper.Instance.CurrentStatistic.DiplomaUrl);
                    // SharingIntent.PutExtra(Intent.ExtraTitle, "Subject");
                    StartActivity(Intent.CreateChooser(SharingIntent, "sharing option"));
                    };
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

                BackButton.Click += BackButton_Click;


                StopLoading();
            }
            catch(Exception ex)
            {
                OnBackPressed();
            }
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
    }
}