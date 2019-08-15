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
using Android.Text;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using FFImageLoading.Views;
using Izrune.Activitys;
using Izrune.Attributes;
using Izrune.Fragments.DialogFrag;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class QuezFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutItemQuestion;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.ContainerForAnswers)]
        LinearLayout ContainerForAnswer;

        [MapControl(Resource.Id.MainQuestionTxt)]
        TextView QuestionTitle;

        [MapControl(Resource.Id.MainImageCard)]
        CardView ImageCard;

        [MapControl(Resource.Id.MainImage)]
        ImageViewAsync MainImage;

        [MapControl(Resource.Id.GridForImages)]
        GridLayout ImagesGrid;


        [MapControl(Resource.Id.SkipQuestion)]
        LinearLayout SkipButton;





       public Action ChangeResultPage { get; set; }

        public Action SkipQuestion { get; set; }

        private static int CorrectAnswerIndex = 0;

        IQuestion question;
        public Func<IQuestion> AnswerClick { get; set; }
        string testType;

        public QuezFragment(IQuestion obj,string TestType)
        {
            question = obj;
            testType = TestType;
        }

        


        private List<string> QuestionVersioSimbols = new List<string>()
        {
            "ა","ბ","გ","დ","ე","ვ"
        };


        private List<View> MyViews = new List<View>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

        }

        List<View> ImageViews = new List<View>();

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            (Activity as QuezActivity).RunOnUiThread(() => { 
            // QuestionTitle.Text = question.title.Replace("span style=\"color:", "font color='").Replace(";\"", "'").Replace("</span>", "</font>"); ;
            QuestionTitle.SetHtml(question.title);
            if (question.images.ToList().Count > 1)
            {
                ImagesGrid.Visibility = ViewStates.Visible;
                ImageViews.Clear();
                foreach(var items in question.images)
                {
                  
                    var Images = LayoutInflater.Inflate(Resource.Layout.ItemQuestionImage, null);
                    Images.FindViewById<ImageViewAsync>(Resource.Id.CardImage).LoadImage(items);
                    ImageViews.Add(Images);
                    Images.Click += Images_Click;
                    ImagesGrid.AddView(Images);

                }

              

            }
            if (question.images.ToList().Count == 1)
            {
                ImageCard.Visibility = ViewStates.Visible;
                MainImage.LoadImage(question.images.ElementAt(0));
            }
            else
            {
                ImageCard.Visibility = ViewStates.Gone;
                ImagesGrid.Visibility = ViewStates.Gone;
            }

           for(int i = 0; i < question.Answers.Count(); i ++)
            {
                var AnswerView = LayoutInflater.Inflate(Resource.Layout.ItemQuezAnswer, null);

                    AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).SetHtml(question.Answers.ElementAt(i).title);
                //AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).Text =question.Answers.ElementAt(i).title.Replace("span style=\"color:", "font color='").Replace(";\"", "'").Replace("</span>", "</font>");
                AnswerView.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);
                AnswerView.Click += AnswerView_Click;
                MyViews.Add(AnswerView);
                ContainerForAnswer.AddView(AnswerView);
            }

          
            SkipButton.Click += SkipButton_Click;
            });
        }

      
        public  void CheckQuestion()
        {
            (Activity as QuezActivity).RunOnUiThread(() =>
            {
                        foreach(var item in MyViews)
                {
                    item.Click -= AnswerView_Click;
                }



                SkipButton.Click -= SkipButton_Click;

                CorrectAnswerIndex = 0;

                foreach (var items in question.Answers)
                {
                    CorrectAnswerIndex++;

                    if (items.IsRight)
                    {
                        var CorrectAnswerView = MyViews[CorrectAnswerIndex - 1];

                        CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                        CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);

                        CorrectAnswerIndex = 0;
                        break;
                    }

                }

              


            });
        }

        private async  void SkipButton_Click(object sender, EventArgs e)
        {
            try
            {
                SkipButton.Click -= SkipButton_Click;
                
                (Activity as QuezActivity).RunOnUiThread(() =>
                {


                    foreach (var item in MyViews)
                    {
                        item.Click -= AnswerView_Click;
                    }


                    CorrectAnswerIndex = 0;

                    foreach (var items in question.Answers)
                    {
                        CorrectAnswerIndex++;

                        if (items.IsRight)
                        {
                            var CorrectAnswerView = MyViews[CorrectAnswerIndex - 1];

                            CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                            CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);

                            CorrectAnswerIndex = 0;
                            break;
                        }

                    }



                });
                await QuezControll.Instance.AddQuestion();

                await Task.Delay(1000);
                question = AnswerClick?.Invoke();
                if (question == null && testType != "1")
                {
                    // (Activity as QuezActivity).OnBackPressed();
                    Intent intent = new Intent(this, typeof(MainDiplomaActivity));
                    StartActivity(intent);
                }
                else if (question == null)
                {

                    // ChangeResultPage?.Invoke();
                    Intent intent = new Intent(this, typeof(MainDiplomaActivity));
                    StartActivity(intent);

                }
                else
                {
                    QuestionTitle.SetHtml(question.title);

                    ContainerForAnswer.RemoveAllViews();
                    MyViews.Clear();

                    ImageViews.Clear();

                    if (question.images.ToList().Count > 1)
                    {

                        foreach (var items in question.images)
                        {
                            var Images = LayoutInflater.Inflate(Resource.Layout.ItemQuestionImage, null);
                            Images.FindViewById<ImageViewAsync>(Resource.Id.CardImage).LoadImage(items);
                            ImageViews.Add(Images);
                            Images.Click += Images_Click;
                            ImagesGrid.AddView(Images);

                        }



                    }

                    if (question.images.ToList().Count == 1)
                    {
                        ImageCard.Visibility = ViewStates.Visible;
                        MainImage.LoadImage(question.images.ElementAt(0));
                    }
                    else
                    {
                        ImageCard.Visibility = ViewStates.Gone;
                    }
                    for (int i = 0; i < question.Answers.Count(); i++)
                    {
                        var AnswerView = LayoutInflater.Inflate(Resource.Layout.ItemQuezAnswer, null);
                        AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).SetHtml(question.Answers.ElementAt(i).title);
                        AnswerView.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);

                        AnswerView.Click += AnswerView_Click;
                        MyViews.Add(AnswerView);
                        ContainerForAnswer.AddView(AnswerView);
                    }
                }
                SkipButton.Click += SkipButton_Click;
            }
            catch(Exception ex)
            {
              //  Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
        }

        private void Images_Click(object sender, EventArgs e)
        {
            var Index = ImageViews.IndexOf((sender as View));

          
            (Activity as QuezActivity).OpenDialog(question.images.ElementAt(Index));
        }


        string CurrentImageUrl="";

        bool IsLike = true;
        private async  void AnswerView_Click(object sender, EventArgs e)
        {
            try
            {

                var Index = MyViews.IndexOf((sender as View));

                (Activity as QuezActivity).RunOnUiThread(() =>
               {

                // (Activity as QuezActivity).StopAnimation();


                if (question.Answers.ElementAt(Index).IsRight)
                   {

                       (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);

                       (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                       CorrectAnswerIndex++;

                       if (CorrectAnswerIndex == 5)
                       {
                           CorrectAnswerIndex = 0;
                           if (IsLike)
                           {
                               (Activity as QuezActivity).PlayAnimation();
                               IsLike = false;
                           }
                           else
                           {
                               (Activity as QuezActivity).PlayAnimationTwo();
                               IsLike = true;

                           }

                       }

                   }
                   else
                   {
                       CorrectAnswerIndex = 0;
                     
                       foreach (var items in question.Answers)
                       {
                           CorrectAnswerIndex++;

                           if (items.IsRight)
                           {
                               var CorrectAnswerView = MyViews[CorrectAnswerIndex - 1];

                               CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                               CorrectAnswerView.FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);

                               CorrectAnswerIndex = 0;
                               break;
                           }

                       }





                       (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesIncorrectAnswerLine);
                       (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesInCorectButtonBackground);
                   }

               });
                foreach (var items in MyViews)
                {
                    items.Click -= AnswerView_Click;
                }
                Startloading(true);
                await QuezControll.Instance.AddQuestion(question.Answers.ToList().ElementAt(Index).id);
                await Task.Delay(500);

                StopLoading();




                question = AnswerClick?.Invoke();
                if (question == null && testType != "1")
                {
                    // (Activity as QuezActivity).OnBackPressed();
                    Intent intent = new Intent(this, typeof(MainDiplomaActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                    (Activity as QuezActivity).Finish();
                }
                else if (question == null)
                {

                    // ChangeResultPage?.Invoke();
                    Intent intent = new Intent(this, typeof(MainDiplomaActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                    (Activity as QuezActivity).Finish();
                }
                else
                {
                    QuestionTitle.Text = question.title;

                    ContainerForAnswer.RemoveAllViews();
                    MyViews.Clear();

                    ImageViews.Clear();

                    if (question.images.ToList().Count > 1)
                    {
                        ImagesGrid.Visibility = ViewStates.Visible;
                        foreach (var items in question.images)
                        {
                            var Images = LayoutInflater.Inflate(Resource.Layout.ItemQuestionImage, null);
                            Images.FindViewById<ImageViewAsync>(Resource.Id.CardImage).LoadImage(items);
                            ImageViews.Add(Images);
                            Images.Click += Images_Click;
                            ImagesGrid.AddView(Images);

                        }



                    }
                    else
                        ImagesGrid.Visibility = ViewStates.Gone;

                    if (question.images.ToList().Count == 1)
                    {
                        ImageCard.Visibility = ViewStates.Visible;
                        CurrentImageUrl = question.images.ElementAt(0);
                        MainImage.LoadImage(CurrentImageUrl);
                        MainImage.Click -= MainImage_Click;
                        MainImage.Click += MainImage_Click;
                    }
                    else
                    {
                        ImageCard.Visibility = ViewStates.Gone;
                        
                    }
                    for (int i = 0; i < question.Answers.Count(); i++)
                    {
                        var AnswerView = LayoutInflater.Inflate(Resource.Layout.ItemQuezAnswer, null);
                        AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = question.Answers.ElementAt(i).title;
                        AnswerView.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);

                        AnswerView.Click += AnswerView_Click;
                        MyViews.Add(AnswerView);
                        ContainerForAnswer.AddView(AnswerView);
                    }
                    //await Task.Delay(4000);
                    // (Activity as QuezActivity).StopAnimation();
                }
            }
            catch(Exception ex)
            {
               // Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
           

        }

       


        private void MainImage_Click(object sender, EventArgs e)
        {
            (Activity as QuezActivity).OpenDialog(CurrentImageUrl);
        }
    }
}