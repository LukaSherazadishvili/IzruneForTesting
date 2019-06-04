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
using FFImageLoading.Views;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class ResultStatisticFragment : MPDCBaseFragment
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


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);


            var Result = await QuezControll.Instance.GetExamInfoAsync();

            var QuisInfo = Result.QueisResult;

            StudentNameLastName.Text = $"{UserControl.Instance.CurrentStudent.Name}  {UserControl.Instance.CurrentStudent.LastName}";

            Score.Text = QuisInfo.Score.ToString();
            Time.Text = $"{QuisInfo.Duration / 60}:{QuisInfo.Duration % 60}";
            Correctanswers.Text = QuisInfo.RightAnswer.ToString();
            IncorectAnswers.Text = QuisInfo.WronAnswers.ToString();
            SkippedAnswer.Text = QuisInfo.SkipedAnswers.ToString();

            if (!string.IsNullOrEmpty(Result.DiplomaURl))
            {
                DiplomaImage.Visibility = ViewStates.Visible;
                DiplomaImage.LoadImage(Result.DiplomaURl);

            }


            for (int i = 0; i < 5; i++)
            {

                FrameLayout.LayoutParams imgViewParams = new FrameLayout.LayoutParams(120, 120, GravityFlags.CenterHorizontal);
                imgViewParams.SetMargins(10, 10, 10, 10);
                var Image = new ImageView(this);
                Image.LayoutParameters = imgViewParams;

                Image.SetBackgroundResource(i < QuisInfo.Stars ? Resource.Drawable.ActiveStar : Resource.Drawable.PasiveStar);
                starsContr.AddView(Image);


            }
        }


    }
}