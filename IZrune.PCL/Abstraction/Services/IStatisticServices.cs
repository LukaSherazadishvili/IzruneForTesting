using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IStatisticServices
    {
        Task<IEnumerable<IStudentsStatistic>> GetStudentStatisticsAsync( QuezCategory type);

        Task<IEnumerable<IQuestion>> GetFinalQuestionResult();

        Task<IQuisResultInfo> GetCurrentTestDiplomaInfo(int TestId);
    }
}
