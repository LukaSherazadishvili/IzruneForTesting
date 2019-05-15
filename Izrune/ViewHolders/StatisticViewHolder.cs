using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Izrune.ViewHolders
{
    class StatisticViewHolder:RecyclerView.ViewHolder
    {
        public TextView Date { get; set; }
        public TextView CorrectAnswer { get; set; }
        public TextView IncorectAnswer { get; set; }
        public TextView SkipedAnswers { get; set; }
        public TextView Time { get; set; }
        public TextView Points { get; set; }

        public StatisticViewHolder(View view) : base(view)
        {
            Date = view.FindViewById<TextView>(Resource.Id.CurrentDate);
            CorrectAnswer = view.FindViewById<TextView>(Resource.Id.CorrectAnswersCount);
            IncorectAnswer = view.FindViewById<TextView>(Resource.Id.IncorrectAnswersCount);
            SkipedAnswers = view.FindViewById<TextView>(Resource.Id.SkipedQuestionsCount);
            Time = view.FindViewById<TextView>(Resource.Id.TimeCost);
            Points = view.FindViewById<TextView>(Resource.Id.Points);

        }
    }
}