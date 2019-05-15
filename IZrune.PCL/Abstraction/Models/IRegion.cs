using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IRegion
    {
         string id { get; set; }
         string title { get; set; }
         IEnumerable<ISchool> Schools { get; set; }
    }
}
