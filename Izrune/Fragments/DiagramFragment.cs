using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using Java.Lang;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Components;
using MikePhil.Charting.Data;
using MikePhil.Charting.Formatter;
using MikePhil.Charting.Util;
using static MikePhil.Charting.Components.YAxis;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class DiagramFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutDiagram;

        [MapControl(Resource.Id.Container)]
        protected override FrameLayout MainFrame { get ; set ; }

        [MapControl(Resource.Id.chart1)]
        BarChart TestCharts;

        [MapControl(Resource.Id.chart2)]
        BarChart PointCharst;

        [MapControl(Resource.Id.chart3)]
        BarChart QuesChart;
         

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Startloading();
            var Statistic = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezTest);

            var LastDataDiagram =await UserControl.Instance.GetDiagramStatistic();
            List<BarEntry> entriesThree = new List<BarEntry>();
            foreach (var items in LastDataDiagram)
            {
                var resultThree = new BarEntry(entriesThree.Count, items.TestCount);
                entriesThree.Add(resultThree);
            }
        

            List<BarEntry> entries = new List<BarEntry>();
            List<BarEntry> entriesTwo = new List<BarEntry>();
           
            List<string> day = new List<string>();
            foreach (var item in Statistic)
            {
                var result = new BarEntry(entries.Count, item.TestTimeInSecconds/60);
               
                entries.Add(result);

                var resultTwo = new BarEntry(entriesTwo.Count, item.Point);

                entriesTwo.Add(resultTwo);

             

              
                day.Add(item.ExamDate.ToShortDateString());
            }


            BarDataSet set = new BarDataSet(entries, "");
            List<Integer> colors = new List<Integer>();

            colors.Add((Integer)ColorTemplate.Rgb("#3F51B5"));
            set.Colors = colors;
            set.ValueTextSize = 7;
            BarData data = new BarData(set);
            TestCharts.Data = data;
            TestCharts.Invalidate();
            

            TestCharts.Description.Enabled = false;
            TestCharts.Legend.Enabled = false;


            XAxis xAxis = TestCharts.XAxis;
            xAxis.ValueFormatter = new IndexAxisValueFormatter(day);
            //xAxis.SetCenterAxisLabels(true);
            xAxis.Position = XAxis.XAxisPosition.Bottom;
            xAxis.Granularity = 1;
            xAxis.GranularityEnabled = true;
            xAxis.TextSize = 7;

            

            TestCharts.DragEnabled = true;
            TestCharts.SetVisibleXRangeMaximum(4);
            TestCharts.AxisLeft.SetDrawGridLines(false);
            TestCharts.AxisRight.SetDrawGridLines(false);
            TestCharts.XAxis.SetDrawGridLines(false);
            TestCharts.SetExtraOffsets(0, 0, 20, 12);
            TestCharts.Selected = false;
            TestCharts.DoubleTapToZoomEnabled = false;
            TestCharts.AxisLeft.AxisMinimum = 0;
            TestCharts.AxisRight.AxisMinimum = 0;

            if (entries.Count < 7)
            {
                for(int i = 1; i <= entries.Count; i++)
                {
                    data.BarWidth = (0.5f / 7) * i;
                }
            }
            else
            {
                data.BarWidth = 0.5f;
            }

            TestCharts.Invalidate();




            BarDataSet set2 = new BarDataSet(entriesTwo, "");
            List<Integer> colors2 = new List<Integer>();

            colors2.Add((Integer)ColorTemplate.Rgb("#E74C3C"));
            set2.Colors = colors2;
            set2.ValueTextSize = 7;
            BarData data2 = new BarData(set2);
            PointCharst.Data = data2;
            PointCharst.Invalidate();

            PointCharst.Description.Enabled = false;
            PointCharst.Legend.Enabled = false;


            XAxis xAxis2 = PointCharst.XAxis;
            xAxis2.ValueFormatter = new IndexAxisValueFormatter(day);
            //xAxis.SetCenterAxisLabels(true);
            xAxis2.Position = XAxis.XAxisPosition.Bottom;
            xAxis2.Granularity = 1;
            xAxis2.GranularityEnabled = true;
            xAxis2.TextSize = 7;

            PointCharst.DragEnabled = true;
            PointCharst.SetVisibleXRangeMaximum(4);
            PointCharst.AxisLeft.SetDrawGridLines(false);
            PointCharst.AxisRight.SetDrawGridLines(false);
            PointCharst.XAxis.SetDrawGridLines(false);
            PointCharst.SetExtraOffsets(0, 0, 20, 12);
            PointCharst.Selected = false;
            PointCharst.DoubleTapToZoomEnabled = false;
            PointCharst.AxisLeft.AxisMinimum = 0;
            PointCharst.AxisRight.AxisMinimum = 0;
            //PointCharst.AxisLeft.Typeface = Typeface.CreateFromFile(".../Resources/font/bpg_mrgvlovani.ttf");

            if (entriesTwo.Count < 7)
            {
                for (int i = 1; i <= entriesTwo.Count; i++)
                {
                    data2.BarWidth = (0.5f / 7) * i;
                }
            }
            else
            {
                data2.BarWidth = 0.5f;
            }

            PointCharst.Invalidate();




            BarDataSet set3 = new BarDataSet(entriesThree, "");
            List<Integer> colors3 = new List<Integer>();

            colors3.Add((Integer)ColorTemplate.Rgb("#49B541"));
            set3.Colors = colors3;
            set3.ValueTextSize = 7;
            BarData data3 = new BarData(set3);
            QuesChart.Data = data3;
            QuesChart.Invalidate();

            QuesChart.Description.Enabled = false;
            QuesChart.Legend.Enabled = false;
            QuesChart.AxisLeft.SetDrawGridLines(false);
            QuesChart.AxisRight.SetDrawGridLines(false);
            QuesChart.XAxis.SetDrawGridLines(false);
            QuesChart.SetExtraOffsets(0, 0, 20, 12);



            XAxis xAxis3 = QuesChart.XAxis;
            xAxis3.ValueFormatter = new IndexAxisValueFormatter(LastDataDiagram.Select(i=>i.CurrentDate).ToList());
            //xAxis.SetCenterAxisLabels(true);
            xAxis3.Position = XAxis.XAxisPosition.Bottom;
            xAxis3.Granularity = 1;
            xAxis3.GranularityEnabled = true;
            xAxis3.TextSize = 7;

            QuesChart.DragEnabled = true;
            QuesChart.SetVisibleXRangeMaximum(4);
            QuesChart.Selected = false;
            QuesChart.DoubleTapToZoomEnabled = false;
            QuesChart.AxisLeft.AxisMinimum = 0;
            QuesChart.AxisRight.AxisMinimum = 0;

            if (entriesThree.Count < 7)
            {
                for (int i = 1; i <= entriesThree.Count; i++)
                {
                    data3.BarWidth = (0.5f / 7) * i;
                }
            }
            else
            {
                data3.BarWidth = 0.5f;
            }

            QuesChart.Invalidate();

            StopLoading();
           


        }

    }
}