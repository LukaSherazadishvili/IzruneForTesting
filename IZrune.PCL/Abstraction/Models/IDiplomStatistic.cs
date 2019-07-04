using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IDiplomStatistic
    {
        string DiplomaDate { get; set; }
        IEnumerable<IQuisInfo> DiplomaStatistic { get; set; }

    }
}
