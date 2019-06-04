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
using FFImageLoading.Views;

namespace Izrune.ViewHolders
{
    class StatisticQuestionViewHolder:RecyclerView.ViewHolder
    {
       public LinearLayout AnswerContainer { get; set; }
        public ImageViewAsync Image { get; set; }

       public TextView Title { get; set; }

        public StatisticQuestionViewHolder(View view):base(view)
        {

            Title = view.FindViewById<TextView>(Resource.Id.QuestionTitle);
            Image = view.FindViewById<ImageViewAsync>(Resource.Id.QuestionImage);
            AnswerContainer = view.FindViewById<LinearLayout>(Resource.Id.ContainerForAnswers);

        }
    }
}