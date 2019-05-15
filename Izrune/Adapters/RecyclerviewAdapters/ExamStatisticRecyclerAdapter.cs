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
using Izrune.ViewHolders;
using IZrune.PCL.Abstraction.Models;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Adapters.RecyclerviewAdapters
{
    class ExamStatisticRecyclerAdapter : RecyclerView.Adapter
    {

        private List<IStudentsStatistic> StudentsStatisticlist;

        public ExamStatisticRecyclerAdapter(List<IStudentsStatistic> lst)
        {
            StudentsStatisticlist = lst;
        }


        public override int ItemCount => StudentsStatisticlist.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is StatisticViewHolder)
            {

                (holder as StatisticViewHolder).Date.Text = StudentsStatisticlist.ElementAt(position).ExamDate.ToShortDateString();
                (holder as StatisticViewHolder).CorrectAnswer.Text = StudentsStatisticlist.ElementAt(position).CorrectAnswersCount.ToString();
                (holder as StatisticViewHolder).IncorectAnswer.Text = StudentsStatisticlist.ElementAt(position).IncorrectAnswersCount.ToString();
                (holder as StatisticViewHolder).Points.Text = StudentsStatisticlist.ElementAt(position).Point.ToString();
                (holder as StatisticViewHolder).Time.Text = StudentsStatisticlist.ElementAt(position).TestTimeInSecconds.ToString();
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemExamStatistic, parent, false);
            var Result =new StatisticViewHolder(view);
            return Result;
        }
    }
}