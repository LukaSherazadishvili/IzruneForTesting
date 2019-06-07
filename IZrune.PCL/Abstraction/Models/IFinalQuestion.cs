using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IFinalQuestion:IQuestion
    {
        int StudentAnswerIndex { get; set; }
     

    }
}
