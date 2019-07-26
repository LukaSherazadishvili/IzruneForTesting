// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;
using UIKit;
using XLPagerTabStrip;
using System.Collections.Generic;
using IZrune.PCL.Abstraction.Services;
using System.Threading.Tasks;
using System.Linq;
using MPDC.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.iOS
{
	public partial class DiagramViewController : UIViewController, IIndicatorInfoProvider
    {
		public DiagramViewController (IntPtr handle) : base (handle)
		{
		}

        public static readonly NSString StoryboardId = new NSString("DiagramStoryboardId");

        private PlotModel _plotModel;
        private PlotView _plotView;

        bool _isAllData = true;
        private List<IStudentsStatistic> statistisData;
        private List<IDiagram> userMonthStatistics;

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.LayoutIfNeeded();

            var statisticsService = ServiceContainer.ServiceContainer.Instance.Get<IStatisticServices>();

             statistisData = (await statisticsService.GetStudentStatisticsAsync(IZrune.PCL.Enum.QuezCategory.QuezExam))?.ToList();

             userMonthStatistics =(await UserControl.Instance.GetDiagramStatistic())?.ToList();

            if (statistisData.Count > 0)
            {
                setUpTimeView();
                setUpPointView();
            }

            if(userMonthStatistics.Count>0)
                setUpThirdView();

        }

        void setUpThirdView()
        {

          
            _plotView = new PlotView(new CoreGraphics.CGRect(0, 25, timeChartView.Frame.Width,
                                                                                   timeChartView.Frame.Height))
            {

            };
            _plotView.BackgroundColor = UIColor.Clear;

            testChartView.AddSubview(_plotView);

            UIColor.FromRGB(73, 181, 65).GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

            var oxyColor = OxyColor.FromRgb((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
            _plotModel = new PlotModel()
            {
                TextColor = oxyColor
                 ,
                PlotAreaBorderColor = OxyColors.Transparent
            };

            _plotModel.Title = $"";



            CategoryAxis xaxis = new CategoryAxis()
            {



                Position = AxisPosition.Bottom
            };
            //xaxis.LabelFormatter = (d) =>
            //{

            //    var dateTime = DateTimeAxis.ToDateTime(d);
            //    return dateTime.ToString("dd/MM/yyyy");
            //};

            xaxis.Position = AxisPosition.Bottom;
            //xaxis.MajorGridlineStyle = LineStyle.Solid;
            //xaxis.MinorGridlineStyle = LineStyle.Dot;



            LinearAxis yaxis = new LinearAxis()
            {
                AbsoluteMinimum = userMonthStatistics.Count() == 1 ? 0 : userMonthStatistics.Min(o => o.TestCount),
                AbsoluteMaximum = userMonthStatistics.Max(o => o.TestCount),
                Minimum = userMonthStatistics.Count() == 1 ? 0 : userMonthStatistics.Min(o => o.TestCount),
                Maximum = userMonthStatistics.Max(o => o.TestCount),
                Position = AxisPosition.Left,
                MinimumPadding = 0,
                MaximumPadding = 0.06
            };

            yaxis.Position = AxisPosition.Left;
            yaxis.MajorGridlineStyle = LineStyle.None;
            xaxis.MinorGridlineStyle = LineStyle.None;

            ColumnSeries s1 = new ColumnSeries();
            s1.FillColor = oxyColor;
            //s1.IsStacked = true;


            foreach (var item in userMonthStatistics)
            {
                xaxis.Labels.Add(item.CurrentDate);
                s1.Items.Add(new ColumnItem(item.TestCount, userMonthStatistics.IndexOf(item)));
            }

            _plotModel.Series.Add(s1);
            _plotModel.Axes.Add(xaxis);
            _plotModel.Axes.Add(yaxis);


            _plotView.Model = _plotModel;
        }

        void setUpPointView()
        {


            statistisData = statistisData.DistinctBy(o => o.ExamDate.Date).ToList();
            _plotView = new PlotView(new CoreGraphics.CGRect(0, 25, timeChartView.Frame.Width,
                                                                                   timeChartView.Frame.Height))
            {

            };
            _plotView.BackgroundColor = UIColor.Clear;

            pointChartView.AddSubview(_plotView);

            UIColor.FromRGB(231, 76, 60).GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

            var oxyColor = OxyColor.FromRgb((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
            _plotModel = new PlotModel()
            {
                TextColor = oxyColor
                 ,
                PlotAreaBorderColor = OxyColors.Transparent
            };

            _plotModel.Title = $"";



            CategoryAxis xaxis = new CategoryAxis()
            {



                Position = AxisPosition.Bottom
            };
            //xaxis.LabelFormatter = (d) =>
            //{

            //    var dateTime = DateTimeAxis.ToDateTime(d);
            //    return dateTime.ToString("dd/MM/yyyy");
            //};

            xaxis.Position = AxisPosition.Bottom;
            //xaxis.MajorGridlineStyle = LineStyle.Solid;
            //xaxis.MinorGridlineStyle = LineStyle.Dot;



            LinearAxis yaxis = new LinearAxis()
            {
                AbsoluteMinimum = statistisData.Count==1? 0: statistisData.Min(o => o.Point),
                AbsoluteMaximum = statistisData.Max(o => o.Point),
                Minimum = statistisData.Count == 1 ? 0 : statistisData.Min(o => o.Point),
                Maximum = statistisData.Max(o => o.Point),
                Position = AxisPosition.Left,
                MinimumPadding = 0,
                MaximumPadding = 0.06
            };

            yaxis.Position = AxisPosition.Left;
            yaxis.MajorGridlineStyle = LineStyle.None;
            xaxis.MinorGridlineStyle = LineStyle.None;

            ColumnSeries s1 = new ColumnSeries();
            
            s1.FillColor = oxyColor;
            //s1.IsStacked = true;


            foreach (var item in statistisData)
            {
                //xaxis.ActualLabels.Add(item.ExamDate.ToString("dd/MM/yyyy"));
               
                //xaxis.LabelField = item.ExamDate.ToString("dd/MM/yyyy");
                xaxis.Labels.Add(item.ExamDate.ToString("dd/MM/yyyy"));
                s1.Items.Add(new ColumnItem(item.Point,statistisData.IndexOf(item)));
                
                //s1.LabelFormatString = "dd/MM/yyyy";
            }

            _plotModel.Series.Add(s1);
            _plotModel.Axes.Add(xaxis);
            _plotModel.Axes.Add(yaxis);


            _plotView.Model = _plotModel;
        }

        void setUpTimeView()
        {

            timeChartView.LayoutIfNeeded();
            statistisData = statistisData.DistinctBy(o=>o.ExamDate.Date).ToList();
            _plotView = new PlotView(new CoreGraphics.CGRect(0, 25, timeChartView.Frame.Width,
                                                                                   timeChartView.Frame.Height))
            {

            };
            _plotView.BackgroundColor = UIColor.Clear;

            
            timeChartView.AddSubview(_plotView);
           
            UIColor.FromRGB(63, 81, 181).GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

            var oxyColor = OxyColor.FromRgb((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
            _plotModel = new PlotModel()
            {
                TextColor = oxyColor
                 ,
                PlotAreaBorderColor = OxyColors.Transparent
            };

            _plotModel.Title = $"";

          

            CategoryAxis xaxis = new CategoryAxis() {
               
                Position =AxisPosition.Bottom
            };
            
            //xaxis.LabelFormatter = (d) =>
            //{

            //    var dateTime = DateTimeAxis.ToDateTime(d);
            //    return dateTime.ToString("dd/MM/yyyy");
            //};

            xaxis.Position = AxisPosition.Bottom;
            //xaxis.MajorGridlineStyle = LineStyle.Solid;
            //xaxis.MinorGridlineStyle = LineStyle.Dot;

            

            LinearAxis yaxis = new LinearAxis() {
                AbsoluteMinimum=statistisData.Count==1? 0: statistisData.Min(o=>o.TestTimeInSecconds),
                AbsoluteMaximum= statistisData.Max(o=>o.TestTimeInSecconds),
                Minimum= statistisData.Count == 1 ? 0 : statistisData.Min(o => o.TestTimeInSecconds),
                Maximum= statistisData.Max(o => o.TestTimeInSecconds),
                Position = AxisPosition.Left,
                MinimumPadding = 0,
                MaximumPadding = 0.06
            };

            yaxis.Position = AxisPosition.Left;
            yaxis.MajorGridlineStyle = LineStyle.None;
            xaxis.MinorGridlineStyle = LineStyle.None;

            ColumnSeries s1 = new ColumnSeries();
            s1.FillColor = oxyColor;
            //s1.IsStacked = true;

            
            foreach (var item in statistisData.Take(10))
            {
                xaxis.Labels.Add(item.ExamDate.ToString("dd/MM/yyyy"));
                s1.Items.Add(new ColumnItem(item.TestTimeInSecconds, statistisData.IndexOf(item)));
            }

            _plotModel.Series.Add(s1);
            _plotModel.Axes.Add(xaxis);
            _plotModel.Axes.Add(yaxis);


            _plotView.Model = _plotModel;
        }

       

        public static PlotModel ColumnSeries()
        {
            var model = new PlotModel
            {
                Title = "ColumnSeries",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var s1 = new ColumnSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new ColumnItem { Value = 25 });
            s1.Items.Add(new ColumnItem { Value = 137 });
            s1.Items.Add(new ColumnItem { Value = 18 });
            s1.Items.Add(new ColumnItem { Value = 40 });

            var s2 = new ColumnSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new ColumnItem { Value = 12 });
            s2.Items.Add(new ColumnItem { Value = 14 });
            s2.Items.Add(new ColumnItem { Value = 120 });
            s2.Items.Add(new ColumnItem { Value = 26 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return model;
        }

        public IndicatorInfo IndicatorInfoForPagerTabStrip(PagerTabStripViewController pagerTabStripController)
        {
            return new IndicatorInfo("დიაგრამები");
        }

    }
}
