﻿using System;
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
using FFImageLoading.Views;
using Izrune.Activitys;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class QuezFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutItemQuestion;

        [MapControl(Resource.Id.ContainerForAnswers)]
        LinearLayout ContainerForAnswer;

        [MapControl(Resource.Id.MainQuestionTxt)]
        TextView QuestionTitle;

        [MapControl(Resource.Id.MainImageCard)]
        CardView ImageCard;

        [MapControl(Resource.Id.MainImage)]
        ImageViewAsync MainImage;


       public Action ChangeResultPage { get; set; }

        IQuestion question;
        public Func<IQuestion> AnswerClick { get; set; }

        public QuezFragment(IQuestion obj)
        {
            question = obj;
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

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            QuestionTitle.Text = question.title;

            if (question.images.ToList().Count > 0)
            {
                ImageCard.Visibility = ViewStates.Visible;
                MainImage.LoadImage(question.images.ElementAt(0));
            }
            else
            {
                ImageCard.Visibility = ViewStates.Gone;
            }

           for(int i = 0; i < question.Answers.Count(); i ++)
            {
                var AnswerView = LayoutInflater.Inflate(Resource.Layout.ItemQuezAnswer, null);
                AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = question.Answers.ElementAt(i).title;
                AnswerView.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);
                AnswerView.Click += AnswerView_Click;
                MyViews.Add(AnswerView);
                ContainerForAnswer.AddView(AnswerView);
            }


           
           
        }

        private async void AnswerView_Click(object sender, EventArgs e)
        {

            var Index = MyViews.IndexOf((sender as View));

            

           await QuezControll.Instance.AddQuestion( question.Answers.ToList().ElementAt(Index).id);

            if (question.Answers.ElementAt(Index).IsRight)
            {
                
                (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);

                (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
            }
            else
            {
                (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesIncorrectAnswerLine);
                (sender as View).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesInCorectButtonBackground);
            }

            foreach(var items in MyViews)
            {
                items.Click -= AnswerView_Click;
            }
            await Task.Delay(800);




            question = AnswerClick?.Invoke();

            if (question == null)
            {

                ChangeResultPage?.Invoke();
                //Intent intent = new Intent(this, typeof(DiplomaActivity));
                //StartActivity(intent);
             
            }
            else
            {
                QuestionTitle.Text = question.title;

                ContainerForAnswer.RemoveAllViews();
                MyViews.Clear();
                if (question.images.ToList().Count > 0)
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
                    AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = question.Answers.ElementAt(i).title;
                    AnswerView.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);

                    AnswerView.Click += AnswerView_Click;
                    MyViews.Add(AnswerView);
                    ContainerForAnswer.AddView(AnswerView);
                }
            }
           
        }
    }
}