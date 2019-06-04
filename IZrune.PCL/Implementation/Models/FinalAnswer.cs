using System;
using System.Collections.Generic;
using System.Text;
using IZrune.PCL.Abstraction.Models;

namespace IZrune.PCL.Implementation.Models
{
   public class FinalAnswer : IFinalAnswers
    {
        public string Title { get; set; }
        public bool QuestionIsRight { get; set; }
        public bool StudentIsRight { get; set; }
    }
}
