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
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = false)]
    class TrainigTestActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutTrainingTest;


        [MapControl(Resource.Id.ExamTestFullTimeButton)]
        LinearLayout ExamTestFullTimeButton;

        [MapControl(Resource.Id.ExamPartTimeButton)]
        LinearLayout ExamPartTimeButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ExamTestFullTimeButton.Click += ExamTestFullTimeButton_Click;

            ExamPartTimeButton.Click += ExamPartTimeButton_Click;
        }

        private async void ExamPartTimeButton_Click(object sender, EventArgs e)
        {
            var Result = (await MpdcContainer.Instance.Get<IQuezServices>().GetQuestionsAsync(1, IZrune.PCL.Enum.QuezCategory.QuezExam)).ToList();

            Intent intent = new Intent(this, typeof(QuezActivity));
            StartActivity(intent);
        }

        private async void ExamTestFullTimeButton_Click(object sender, EventArgs e)
        {

            var Result =(await MpdcContainer.Instance.Get<IQuezServices>().GetQuestionsAsync(1,IZrune.PCL.Enum.QuezCategory.QuezExam)).ToList();

            Intent intent = new Intent(this, typeof(QuezActivity));
            StartActivity(intent);
        }
    }
}