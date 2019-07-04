using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class DiplomaStatisticc : IDiplomStatistic
    {
        public string DiplomaDate { get; set; }
       public IEnumerable<IQuisInfo> DiplomaStatistic { get; set; }
    }
}
