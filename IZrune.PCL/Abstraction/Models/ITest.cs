using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface ITestControler
    {
        int Duration { get; set; }
        IEnumerable<IQuezQuestion> Questions { get; set; }
    }
}
