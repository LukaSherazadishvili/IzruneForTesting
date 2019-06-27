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
using Izrune.Activitys;
using Izrune.Adapters.SpinerAdapter;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;

namespace Izrune.Fragments
{
    class MainPageTestFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.IzruneTestebi;

        [MapControl(Resource.Id.MainTestButton)]
        FrameLayout ExamtestButton;


        [MapControl(Resource.Id.ExamTestButton)]
        FrameLayout TrainigTestButton;

        [MapControl(Resource.Id.StudentSpiner)]
        Spinner StudSpiner;

        [MapControl(Resource.Id.ExamDayCount)]
        TextView ExamDay;

        [MapControl(Resource.Id.ExamMinitCount)]
        TextView ExamMinit;

        [MapControl(Resource.Id.ExamHoursCount)]
        TextView ExamHours;

        [MapControl(Resource.Id.TestHoursCount)]
        TextView TestHours;

        [MapControl(Resource.Id.TestDayCount)]
        TextView TestDayCount;

        [MapControl(Resource.Id.TestMinitCount)]
        TextView TestMinit;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public async override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ExamtestButton.Click += ExamtestButton_Click;
            TrainigTestButton.Click += TrainigTestButton_Click;

            var Result =await UserControl.Instance.GetCurrentUser();

            var date = QuezControll.Instance.GetExamDate(IZrune.PCL.Enum.QuezCategory.QuezTest);

           var DataAdapter = new ArrayAdapter<string>(this, 
               Android.Resource.Layout.SimpleSpinnerDropDownItem, 
               Result.Students.Select(i=> ($"{i.Name}   {i.LastName}")).ToList());

           var r=await UserControl.Instance.GetDiagramStatistic();

           // var adapter = new CategorySpinnerAdapter(this, Resource.Layout.itemSpinnerText, Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList(), StudSpiner);
           // var add = new MySpinnerAdapter(this, Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());
            StudSpiner.Adapter = DataAdapter;
            StudSpiner.ItemSelected += async(s, e) =>
            {
                UserControl.Instance.SeTSelectedStudent(Result.Students.ElementAt(e.Position).id);

                var TimeResult =await QuezControll.Instance.GetExamDate(IZrune.PCL.Enum.QuezCategory.QuezExam);

                if (TimeResult.Days <= 0 && TimeResult.Hours <= 0 && TimeResult.Minutes <= 0)
                {

                }
                else
                {
                    ExamDay.Text = TimeResult.Days.ToString();
                    ExamHours.Text = TimeResult.Hours.ToString();
                    ExamMinit.Text = TimeResult.Minutes.ToString();

                }


               var  TestTimeRes = await QuezControll.Instance.GetExamDate(IZrune.PCL.Enum.QuezCategory.QuezTest);

                if (TestTimeRes.Days <= 0 && TestTimeRes.Hours <= 0 && TestTimeRes.Minutes <= 0)
                {

                }
                else
                {
                    TestDayCount.Text = TimeResult.Days.ToString();
                    TestHours.Text = TimeResult.Hours.ToString();
                    TestMinit.Text = TimeResult.Minutes.ToString();

                }

            };
        }

        

        private void TrainigTestButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(TrainigTestActivity));
            StartActivity(intent);

        }

        private void ExamtestButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(ExamTestActivity));
            StartActivity(intent);
        }

       
    }
}