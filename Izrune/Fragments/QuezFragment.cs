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
using Izrune.Attributes;

namespace Izrune.Fragments
{
    class QuezFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutItemQuestion;

        [MapControl(Resource.Id.ContainerForAnswers)]
        LinearLayout ContainerForAnswer;

        class testQuestion
        {
            public string Question { get; set; }
        }

        private List<testQuestion> MyQuestion = new List<testQuestion>()
        {
            new testQuestion(){Question="ტექსტი ეხმარება დიზაინერებს და ტიპოგრაფიული"},
              new testQuestion(){Question="ტექსტი ეხმარება დიზაინერებს და ტიპოგრაფიული"},
                new testQuestion(){Question="ტექსტი ეხმარება დიზაინერებს და ტიპოგრაფიული"},
                  new testQuestion(){Question="ტექსტი ეხმარება დიზაინერებს და ტიპოგრაფიული"}
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

          


            foreach (var items in MyQuestion)
            {
                var AnswerView = LayoutInflater.Inflate(Resource.Layout.ItemQuezAnswer, null);
                AnswerView.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = items.Question;
                ContainerForAnswer.AddView(AnswerView);
            }
            //ContainerForAnswer.AddView(AnswerView);
            //ContainerForAnswer.AddView(AnswerView);
            //ContainerForAnswer.AddView(AnswerView);
        }

    }
}