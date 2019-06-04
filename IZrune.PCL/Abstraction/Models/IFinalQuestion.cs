using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IFinalQuestion
    {
        string Title { get; set; }
        IEnumerable<string> Images { get; set; }

        IEnumerable<IFinalAnswers> Answers { get; set; }
    }
}
