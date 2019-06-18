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
using IZrune.PCL.Abstraction.Services;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class DiagramFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutDiagram;


        [MapControl(Resource.Id.chartView)]
        ChartView TestCharts;

        [MapControl(Resource.Id.chartView1)]
        ChartView PointCharst;

        [MapControl(Resource.Id.chartView2)]
        ChartView QuesChart;

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);


            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam);


            List<ChartEntry> entries = new List<ChartEntry>();
            List<ChartEntry> entriesTwo = new List<ChartEntry>();
            List<ChartEntry> entriesThree = new List<ChartEntry>();
            foreach (var item in Statistic)
            {
             var result=   new ChartEntry(item.Point) { Label = item.ExamDate.ToShortDateString(), ValueLabel = item.TestTimeInSecconds.ToString(), Color = SKColor.Parse("#3F51B5") };

                entries.Add(result);

                var resultTwo = new ChartEntry(item.Point) { Label = item.ExamDate.ToShortDateString(), ValueLabel = item.TestTimeInSecconds.ToString(), Color = SKColor.Parse("#E74C3C") };

                entriesTwo.Add(resultTwo);

                var resultThree = new ChartEntry(item.Point) { Label = item.ExamDate.ToShortDateString(), ValueLabel = item.TestTimeInSecconds.ToString(), Color = SKColor.Parse("#49B541") };

                entriesThree.Add(resultThree);

            }



            var TestPoinChart = new BarChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 18,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            TestCharts.Chart = TestPoinChart;
            TestCharts.LayoutParameters.Width = 110 * entries.Count;



            var PointChartsEntrie = new BarChart()
            {
                Entries = entriesTwo,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 18,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            PointCharst.Chart = PointChartsEntrie;
            PointCharst.LayoutParameters.Width = 110 * entries.Count;
            



            var QuezCountentry = new BarChart()
            {
                Entries = entriesThree,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 18,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            QuesChart.Chart = QuezCountentry;
            QuesChart.LayoutParameters.Width = 110 * entries.Count;


        }

    }
}