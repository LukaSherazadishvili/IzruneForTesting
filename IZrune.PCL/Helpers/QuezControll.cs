using IZrune.PCL.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace IZrune.PCL.Helpers
{
    class QuezControll
    {
        private static QuezControll instance = null;
        private static readonly object padlock = new object();

        QuezControll()
        {
        }

        public static QuezControll Instance {
            get {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            AppCore.Instance.InitServices();
                            instance = new QuezControll();
                        }
                    }
                }
                return instance;
            }
        }


        public async Task<bool> IsExamActivated(int studentID)
        {
            var Result = await MpdcContainer.Instance.Get<IStatisticServices>().GetStudentStatisticsAsync(studentID,Enum.QuezCategory.QuezExam);

            return Result.Any(i => i.ExamDate == DateTime.Now);

        }
    }
}
