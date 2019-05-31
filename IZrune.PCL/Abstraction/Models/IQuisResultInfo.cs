using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IQuisResultInfo
    {
        QuezCategory test_type { get; set; }
        DateTime Date { get; set; }
        string Score { get; set; }
        int Stars { get; set; }
        int Duration { get; set; }
        string Egmu { get; set; }

        int? RightAnswer { get; set; }

        int? WronAnswers { get; set; }

        int? SkipedAnswers { get; set; }


    }
}
