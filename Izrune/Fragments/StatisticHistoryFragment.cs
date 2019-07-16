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
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class StatisticHistoryFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.Statistika_Istoria;

        [MapControl(Resource.Id.StudentSpiner)]
        Spinner StudentSpiner;

        [MapControl(Resource.Id.ExamTestDiplomas)]
        LinearLayout DiplomasButton;

        [MapControl(Resource.Id.ExamTestStatistickButton)]
        LinearLayout ExamTestButton;

        [MapControl(Resource.Id.QuesStatistickButton)]
        LinearLayout QuesExamButton;

        [MapControl(Resource.Id.EndPackDateTxt)]
        TextView EndPackTxt;

        [MapControl(Resource.Id.HistoryButton)]
        LinearLayout HistoryButton;



        public IStudent CurrentStudent;
        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            var Result = await UserControl.Instance.GetCurrentUser();

            var DataAdapter = new ArrayAdapter<string>(this,
              Android.Resource.Layout.SimpleSpinnerDropDownItem,
              Result.Students.Select(i => ($"{i.Name}   {i.LastName}")).ToList());

            StudentSpiner.Adapter = DataAdapter;

            StudentSpiner.ItemSelected += (s, e) =>
            {
                CurrentStudent = Result.Students.ElementAt(e.Position);
                EndPackTxt.Text = CurrentStudent.PakEndDate?.ToShortDateString();
                UserControl.Instance.SeTSelectedStudent(CurrentStudent.id);


            };

            QuesExamButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(MainExamStatisticActivity));
                StartActivity(intent);
            };

            ExamTestButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(ExamStatisticActivity));
                StartActivity(intent);

              
            };

            DiplomasButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this,typeof(DiplomasStatisticActivity));
                StartActivity(intent);
            };

            HistoryButton.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(PaymentHistoryActivity));
                StartActivity(intent);
            };

        }

    }
}