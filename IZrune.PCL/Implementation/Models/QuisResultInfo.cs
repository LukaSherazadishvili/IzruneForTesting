using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class QuisResultInfo : IQuisResultInfo
    {
        public QuezCategory test_type { get; set; }
        public DateTime Date { get; set; }
        public string Score { get; set; }
        public int Stars { get; set; }
        public int Duration { get; set; }
        public string Egmu { get; set; }
        public int? RightAnswer { get; set; }
        public int? WronAnswers { get; set; }
        public int? SkipedAnswers { get; set; }
    }
}
