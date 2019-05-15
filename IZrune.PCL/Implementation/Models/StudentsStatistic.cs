﻿using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class StudentsStatistic : IStudentsStatistic
    {
        public DateTime ExamDate { get; set; }
        public int? CorrectAnswersCount { get; set; }
        public int? IncorrectAnswersCount { get; set; }
        public int? SkippedQuestionsCount { get; set; }
        public int TestTimeInSecconds { get; set; }
        public int Point { get; set; }
    }
}
