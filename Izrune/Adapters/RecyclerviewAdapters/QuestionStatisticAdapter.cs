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
using Izrune.Helpers;
using Izrune.ViewHolders;
using IZrune.PCL.Abstraction.Models;

namespace Izrune.Adapters.RecyclerviewAdapters
{
   public class QuestionStatisticAdapter : RecyclerView.Adapter
    {
        private  List<IFinalQuestion> ResQuestions { get; set; }

        private Context context { get; set; }
        public QuestionStatisticAdapter(List<IFinalQuestion> lst,Context cntx)
        {
            ResQuestions = lst;
            context = cntx;
        }


        public override int ItemCount  => ResQuestions.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var hld = (holder as StatisticQuestionViewHolder);
            hld.Title.Text = ResQuestions.ElementAt(position).Title;
            if (ResQuestions.ElementAt(position).Images.Count() > 0) {
                hld.Image.LoadImage(ResQuestions.ElementAt(position).Images.ElementAt(0));
                    }
            var Resultt = LayoutInflater.From(context).Inflate(Resource.Layout.ItemQuezAnswer, null);

            foreach(var items in ResQuestions.ElementAt(position).Answers)
            {
                Resultt.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = items.Title;

                if (items.StudentIsRight)
                {
                    Resultt.FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                }
                else
                {
                    Resultt.FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesInCorectButtonBackground);
                }

                hld.AnswerContainer.AddView(Resultt);

            }


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemQuestionStatistic, parent, false);

            StatisticQuestionViewHolder Result = new StatisticQuestionViewHolder(layout);

            return Result;

        }
    }
}