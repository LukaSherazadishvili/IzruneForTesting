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
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class ExamStatisticFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.ItemBestExamStatistic;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set; }

        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView statisticRecycler;

        [MapControl(Resource.Id.FavoriteContainer)]
        LinearLayout FavContainer;

        [MapControl(Resource.Id.bestDate)]
        TextView date;

        [MapControl(Resource.Id.BestPoint)]
        TextView Point;

        [MapControl(Resource.Id.minute)]
        TextView Minute;

        [MapControl(Resource.Id.BestTimeDate)]
        TextView TimeDate;

        [MapControl(Resource.Id.second)]
        TextView Second;

        [MapControl(Resource.Id.TestCount)]
        TextView TestCount;

        [MapControl(Resource.Id.BetwenDate)]
        TextView BetwenDate;

        [MapControl(Resource.Id.YearSpinner)]
        Spinner YearSpinner;

        [MapControl(Resource.Id.MonthSpinner)]
        Spinner MonthSpinner;

        [MapControl(Resource.Id.BestExamContainer)]
        LinearLayout FavCont;

        IEnumerable<IStudentsStatistic> Statistic;


        bool FirstIncome = false;

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Startloading();


             Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest);


           
            
            if (Statistic.Count() > 0)
            {


                var Result = Statistic.OrderByDescending(i => i.Point).FirstOrDefault();



                var DateResult = Statistic.DistinctBy(i => i.ExamDate.Year);

                var YearAdapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerDropDownItem,
             DateResult.Select(i => $"{i.ExamDate.Year} წელი").ToList());

                YearSpinner.Adapter = YearAdapter;

                var MonthAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
           IzruneHellper.Instance.Monthes);


               

                MonthSpinner.Adapter = MonthAdapter;

                var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

                statisticRecycler.SetAdapter(adapter);
                statisticRecycler.SetLayoutManager(new LinearLayoutManager(this));


                int Year = 0;
                int Month = 0;

              
                    YearSpinner.ItemSelected += (s, e) =>
                    {

                        Activity.RunOnUiThread(() =>
                        {
                            if (FirstIncome)
                            {
                                Year = DateResult.ElementAt(e.Position).ExamDate.Year;
                                var Res = Statistic.Where(i => i.ExamDate.Year == Year && i.ExamDate.Month == IzruneHellper.Instance.Monthes.IndexOf(IzruneHellper.Instance.Monthes.ElementAt(MonthSpinner.SelectedItemPosition)) + 1);

                                if (Res.Count()<0)
                                    FavCont.Visibility = ViewStates.Gone;
                                else
                                    FavCont.Visibility = ViewStates.Visible;

                                var CurrentStatistic = Res.OrderByDescending(i => i.Point).FirstOrDefault();
                                if (CurrentStatistic != null)
                                { 
                                    date.Text = CurrentStatistic.ExamDate.ToShortDateString();
                                    Point.Text = CurrentStatistic.Point.ToString();

                                }
                                var bestStatisticByTime = Res.OrderByDescending(i => i.TestTimeInSecconds).LastOrDefault();

                                if (bestStatisticByTime != null)
                                {
                                    TimeDate.Text = bestStatisticByTime.ExamDate.ToShortDateString();
                                    Minute.Text = (bestStatisticByTime.TestTimeInSecconds / 60).ToString();
                                    Second.Text = (bestStatisticByTime.TestTimeInSecconds % 60).ToString();
                                }

                                var GroupdExams = Res.GroupBy(c =>
                                     c.ExamDate.Day
                                   ).Select(i => i.Select(o => o.ExamDate.ToShortDateString()).ToList()).ToList();

                                if (GroupdExams.Count() > 0)
                                {
                                    var GroupdResult = GroupdExams.OrderByDescending(i => i.Count).FirstOrDefault();

                                    BetwenDate.Text = GroupdResult[0];
                                    TestCount.Text = GroupdResult.Count.ToString();
                                }
                                //    BetwenDate.Text=GroupdResult.FirstOrDefault()?

                                adapter = new ExamStatisticRecyclerAdapter(Res.ToList());
                                statisticRecycler.SetAdapter(adapter);

                            }
                        });
                    };

                    MonthSpinner.ItemSelected += (s, e) =>
                    {
                        if (FirstIncome)
                        {
                            Month = e.Position;
                            var Res = Statistic?.Where(i => i.ExamDate.Month == Month && i.ExamDate.Year == Year);

                            adapter = new ExamStatisticRecyclerAdapter(Res?.ToList());
                            statisticRecycler.SetAdapter(adapter);

                            if (Res.ToList().Count==0)
                                FavCont.Visibility = ViewStates.Gone;
                            else
                                FavCont.Visibility = ViewStates.Visible;


                            var CurrentStatistic = Res.OrderByDescending(i => i.Point).FirstOrDefault();
                            if (CurrentStatistic != null)
                            {
                                date.Text = CurrentStatistic.ExamDate.ToShortDateString();
                                Point.Text = CurrentStatistic.Point.ToString();

                            }
                            var bestStatisticByTime = Res.OrderByDescending(i => i.TestTimeInSecconds).LastOrDefault();

                            if (bestStatisticByTime != null)
                            {
                                TimeDate.Text = bestStatisticByTime.ExamDate.ToShortDateString();
                                Minute.Text = (bestStatisticByTime.TestTimeInSecconds / 60).ToString();
                                Second.Text = (bestStatisticByTime.TestTimeInSecconds % 60).ToString();
                            }

                            var GroupdExams = Res.GroupBy(c =>
                                    c.ExamDate.Day
                                  ).Select(i => i.Select(o => o.ExamDate.ToShortDateString()).ToList()).ToList();

                            if (GroupdExams.Count() > 0)
                            {
                                var GroupdResult = GroupdExams.OrderByDescending(i => i.Count).FirstOrDefault();


                             //   var Groupdcount = GroupdExams.Select(i => i.Where(x => x == GroupdResult)).FirstOrDefault().Count();

                                BetwenDate.Text = GroupdResult[0];
                                TestCount.Text = GroupdResult.Count.ToString();

                            }


                            adapter = new ExamStatisticRecyclerAdapter(Res.ToList());
                            statisticRecycler.SetAdapter(adapter);


                        }
                    };
                FirstIncome = true;
               
            }
            else
            {
                FavContainer.Visibility = ViewStates.Gone;
            }
            StopLoading();

        }

       


    }
}