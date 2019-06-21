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
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace Izrune.Activitys
{
    [Activity(Label = "DiagramAcrivity", Theme = "@style/AppTheme", MainLauncher = false)]
     class DiagramAcrivity : MPDCBaseActivity
    {

        protected override int LayoutResource { get; } = Resource.Layout.LayoutDiagram;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<ChartEntry> entries = new List<ChartEntry>
                       {
                           new ChartEntry(50)
                           {
                               Label = "11.04.2019",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(65)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "65",
                           Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(20)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#ff0000")
                           },
                           new ChartEntry(40)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(50)
                           {
                               Label = "11.04.2019",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(70)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#20a512")
                           },
                           new ChartEntry(20)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#ff0000")
                           },
                           new ChartEntry(40)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#d6d61d")
                           },
                            new ChartEntry(50)
                           {
                               Label = "11.04.2019",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(100)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#20a512")
                           },
                           new ChartEntry(20)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#ff0000")
                           },
                           new ChartEntry(30)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "30",
                           Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(50)
                           {
                               Label = "11.04.2019",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#d6d61d")
                           },
                           new ChartEntry(100)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#20a512")
                           },
                           new ChartEntry(20)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#ff0000")
                           },
                           new ChartEntry(40)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#3F51B5")
                           },
                            new ChartEntry(50)
                           {
                               Label = "11.04.2019",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#3F51B5")
                           },
                           new ChartEntry(100)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#20a512")
                           },
                           new ChartEntry(20)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#ff0000")
                           },
                           new ChartEntry(40)
                           {
                           Label = "11.04.2019",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#3F51B5")
                           },


                        };

            List<ChartEntry> entries2 = new List<ChartEntry>
            {
                           new ChartEntry(50)
                           {
                               Label = "January",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#E74C3C")
                           },
                           new ChartEntry(100)
                           {
                           Label = "February",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#E74C3C")
                           },
                           new ChartEntry(20)
                           {
                           Label = "March",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#E74C3C")
                           },
                           new ChartEntry(40)
                           {
                           Label = "March",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#E74C3C")
                           }
                        };

            List<ChartEntry> entries3 = new List<ChartEntry>
            {
                           new ChartEntry(50)
                           {
                               Label = "о привет",
                               ValueLabel = "50",
                               Color = SKColor.Parse("#49B541")
                           },
                           new ChartEntry(100)
                           {
                           Label = "თებერვალი",
                           ValueLabel = "100",
                           Color = SKColor.Parse("#49B541")
                           },
                           new ChartEntry(20)
                           {
                           Label = "მარტი",
                           ValueLabel = "20",
                           Color = SKColor.Parse("#49B541")
                           },
                           new ChartEntry(40)
                           {
                           Label = "აპრილი",
                           ValueLabel = "40",
                           Color = SKColor.Parse("#49B541")
                           }
                        };

            var chart = new BarChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 35,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            var chart2 = new BarChart()
            {
                Entries = entries2,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 35,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            var chart3 = new BarChart()
            {
                Entries = entries3,
                BackgroundColor = SKColors.Transparent,
                MaxValue = 100,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelTextSize = 35,
                Margin = 15,
                Typeface = SKTypeface.FromFamilyName("Georgia")
            };

            //var chartView = FindViewById<ChartView>(Resource.Id.chartView);
            //chartView.Chart = chart;
            //chartView.LayoutParameters.Width = 205 * entries.Count;

            //var chartView2 = FindViewById<ChartView>(Resource.Id.chartView1);
            //chartView2.Chart = chart2;
            //chartView2.LayoutParameters.Width = 205 * entries2.Count;

            //var chartView3 = FindViewById<ChartView>(Resource.Id.chartView2);
            //chartView3.Chart = chart3;
            //chartView3.LayoutParameters.Width = 205 * entries3.Count;
        }
    }
}