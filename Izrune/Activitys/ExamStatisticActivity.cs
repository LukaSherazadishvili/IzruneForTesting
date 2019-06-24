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
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class ExamStatisticActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; }= Resource.Layout.LayoutStatistic;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.StatisticRecycler)]
        RecyclerView recycler;

        [MapControl(Resource.Id.YearSpinner)]
        Spinner YearSpinner;

        [MapControl(Resource.Id.MonthSpiner)]
        Spinner MonthSpinner;

        [MapControl(Resource.Id.HeaderText)]
        TextView Headertext;

        [MapControl(Resource.Id.BackButton)]
        FrameLayout BackButton;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


         

            Startloading();

            Headertext.Text = "შემაჯამებელი ტესტები";


            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);

            var DateResult = Statistic.DistinctBy(i => i.ExamDate.Year);

            var YearAdapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerDropDownItem,
            DateResult.Select(i => $"{i.ExamDate.Year} წელი").ToList());

            var MonthAdapter = new ArrayAdapter<string>(this,
          Android.Resource.Layout.SimpleSpinnerDropDownItem,
         IzruneHellper.Instance.Monthes);


            YearSpinner.Adapter = YearAdapter;
            MonthSpinner.Adapter = MonthAdapter;
            int Month=0;
            int Year=0;

            YearSpinner.ItemSelected += (s, e) =>
            {
                Year = DateResult.ElementAt(e.Position).ExamDate.Year;



                var Res = Statistic.Where(i => i.ExamDate.Year == Year && i.ExamDate.Month == IzruneHellper.Instance.Monthes.IndexOf(IzruneHellper.Instance.Monthes.ElementAt(MonthSpinner.SelectedItemPosition)) + 1);


                if (Res.Count() > 0)
                {

                    var adapterr = new ExamStatisticRecyclerAdapter(Res?.ToList());

                    recycler.SetAdapter(adapterr);
                    recycler.SetLayoutManager(new LinearLayoutManager(this));



                }

            };

            MonthSpinner.ItemSelected += (s, e) =>
            {
                Month = e.Position + 1;
                var Res = Statistic.Where(i => i.ExamDate.Month == Month && i.ExamDate.Year == Year);
                if (Res.Count() > 0)
                {

                    var adapterr = new ExamStatisticRecyclerAdapter(Res?.ToList());

                    recycler.SetAdapter(adapterr);
                    recycler.SetLayoutManager(new LinearLayoutManager(this));



                }

            };


            var adapter = new ExamStatisticRecyclerAdapter(Statistic?.ToList());

            recycler.SetAdapter(adapter);
            recycler.SetLayoutManager(new LinearLayoutManager(this));


            BackButton.Click += BackButton_Click;

            StopLoading();

         

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}