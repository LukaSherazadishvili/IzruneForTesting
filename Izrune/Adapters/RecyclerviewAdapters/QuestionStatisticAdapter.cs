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

        private List<string> QuestionVersioSimbols = new List<string>()
        {
            "ა","ბ","გ","დ","ე","ვ"
        };

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

            if (!string.IsNullOrEmpty(ResQuestions.ElementAt(position).Description))
            {
                hld.Description.Visibility = ViewStates.Visible;
                hld.Description.Text = ResQuestions.ElementAt(position).Description;

            }
            else
                hld.Description.Visibility = ViewStates.Gone;




            Answers.Clear();
            hld.AnswerContainer.RemoveAllViews();
            for( int i=0;i<ResQuestions.ElementAt(position).Answers.Count();i++ )
            {
                var Resultt = LayoutInflater.From(context).Inflate(Resource.Layout.ItemQuezAnswer, null);
                Resultt.FindViewById<TextView>(Resource.Id.AnswerTxt).Text = ResQuestions.ElementAt(position).Answers.ElementAt(i).title;

                Resultt.FindViewById<TextView>(Resource.Id.AnswerVersionSimbol).Text = QuestionVersioSimbols.ElementAt(i);


                Answers.Add(Resultt);
                hld.AnswerContainer.AddView(Resultt);

            }
            var Index = ResQuestions.ElementAt(position).StudentAnswerIndex;
            if (Index >= 0)
            {
                if (ResQuestions.ElementAt(position).Answers.ElementAt(Index).IsRight)
                {

                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                }
                else
                {
                    var CorrectIndex = ResQuestions.ElementAt(position).Answers.ToList().IndexOf(ResQuestions.ElementAt(position).Answers.FirstOrDefault(i => i.IsRight));

                    int index = 0;
                    foreach(var items in ResQuestions.ElementAt(position).Answers)
                    {
                        index++;
                        if (items.IsRight)
                        {
                            Answers.ElementAt(index-1).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                            Answers.ElementAt(index-1).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                            index = 0;
                            break;
                        }
                    }
                   
                   

                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesInCorectButtonBackground);
                    Answers.ElementAt(Index).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesIncorrectAnswerLine);

                }
            }
            else
            {
                int index = 0;
                foreach (var items in ResQuestions.ElementAt(position).Answers)
                {
                  
                    if (items.IsRight)
                    {
                        Answers.ElementAt(index).FindViewById<FrameLayout>(Resource.Id.QuesButton).SetBackgroundResource(Resource.Drawable.QuesCorrectButtonBackground);
                        Answers.ElementAt(index).FindViewById<FrameLayout>(Resource.Id.QuesSimbol).SetBackgroundResource(Resource.Drawable.QuesCorrectAnswerLine);
                        index = 0;
                        break;
                    }
                    index++;
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