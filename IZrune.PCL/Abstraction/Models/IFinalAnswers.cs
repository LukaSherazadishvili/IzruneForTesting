using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
  public  interface IFinalAnswers
    {
        int position { get; set; }
        string Title { get; set; }
        bool QuestionIsRight { get; set; }
        bool StudentIsRight { get; set; }
    }
}
