using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
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

        [MapControl(Resource.Id.PointText)]
        TextView PointTxt;

        [MapControl(Resource.Id.MarkTXt)]
        TextView Mark;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        MediaPlayer player = null;
        public override  void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Activity.RunOnUiThread(async() => {

                Startloading();

                var Result = await QuezControll.Instance.GetExamInfoAsync();

                var QuisInfo = Result.QueisResult;

                StudentNameLastName.Text = $"{UserControl.Instance.CurrentStudent.Name}  {UserControl.Instance.CurrentStudent.LastName}";

                Score.Text = QuisInfo.Score.ToString();
                Time.Text = $"{QuisInfo.Duration / 60}:{QuisInfo.Duration % 60}";
                Correctanswers.Text = QuisInfo.RightAnswer.ToString();
                IncorectAnswers.Text = QuisInfo.WronAnswers.ToString();
                SkippedAnswer.Text = QuisInfo.SkipedAnswers.ToString();
                PointTxt.Text = QuisInfo.text_title;
                Mark.Text = QuisInfo.text_description;
                if (!string.IsNullOrEmpty(Result.DiplomaURl))
                {
                    DiplomaImage.Visibility = ViewStates.Visible;
                    DiplomaImage.LoadImage(Result.DiplomaURl);

                }

                if (QuisInfo.test_type == IZrune.PCL.Enum.QuezCategory.QuezTest) {
                    switch (QuisInfo.Stars)
                    {
                        case 1:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.testOnestar);

                                break;
                            }
                        case 2:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.testTwoStar);

                                break;
                            }
                        case 3:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.testThreeStar);

                                break;
                            }
                        case 4:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.testFourStar);

                                break;
                            }
                        case 5:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.testFiveStar);

                                break;
                            }
                    }

                }
                else
                {
                    switch (QuisInfo.Stars)
                    {
                        case 1:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.examOneStar);

                                break;
                            }
                        case 2:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.ExamTwoStar);

                                break;
                            }
                        case 3:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.ExamThreeStar);

                                break;
                            }
                        case 4:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.examFourStar);

                                break;
                            }
                        case 5:
                            {

                                player = MediaPlayer.Create(this, Resource.Drawable.ExamFiveStar);

                                break;
                            }
                    }

                }
            
                    player?.Start();
             
                for (int i = 0; i < 5; i++)
                {

                    FrameLayout.LayoutParams imgViewParams = new FrameLayout.LayoutParams(120, 120, GravityFlags.CenterHorizontal);
                    imgViewParams.SetMargins(10, 10, 10, 10);
                    var Image = new ImageView(this);
                    Image.LayoutParameters = imgViewParams;

                    Image.SetBackgroundResource(i < QuisInfo.Stars ? Resource.Drawable.ActiveStar : Resource.Drawable.PasiveStar);
                    starsContr.AddView(Image);


                }
                StopLoading();
            });

          
        }

        public override void OnPause()
        {
            base.OnPause();
         
                player?.Stop();
            
        }
    }
}