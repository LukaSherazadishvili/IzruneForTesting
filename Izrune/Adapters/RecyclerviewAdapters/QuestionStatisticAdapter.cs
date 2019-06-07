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



        private List<View> Answers = new List<View>();
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var hld = (holder as StatisticQuestionViewHolder);
            hld.Title.Text = ResQuestions.ElementAt(position).title;
            if (ResQuestions.ElementAt(position).images.Count() > 0) {
                hld.Image.Visibility = ViewStates.Visible;
                hld.Image.LoadImage(ResQuestions.ElementAt(position).images.ElementAt(0));
                    }
            else
            {
                hld.Image.Visibility = ViewStates.Gone;
            }

            hld.PositionTxt.Text = (position+1).ToString()+") ";



            Answers.Clear();
            hld.AnswerContainer.RemoveAllViews();
            foreach(var items in ResQuestions.ElementAt(position).Answers)
            {
                var Resultt = LayoutInflater.From(context).Inflate(Resource.Layout.ItemQuezAnswer, null);
                Resultt.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = items.title;

                Answers.Add(Resultt);
                hld.AnswerContainer.AddView(Resultt);

            }
            var Index = ResQuestions.ElementAt(position).StudentAnswerIndex;
            if (Index >= 0)
            {
                if (ResQuestions.ElementAt(position).Answers.ElementAt(Index).IsRight)
                {
                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                }
                else
                {
                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesInCorectButtonBackground);
                }
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