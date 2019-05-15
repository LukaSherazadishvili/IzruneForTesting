using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IStudentsStatistic
    {
        DateTime ExamDate { get; set; }
        int? CorrectAnswersCount { get; set; }
        int? IncorrectAnswersCount { get; set; }
        int? SkippedQuestionsCount { get; set; }
        int TestTimeInSecconds { get; set; }
        int Point { get; set; }
    }
}
