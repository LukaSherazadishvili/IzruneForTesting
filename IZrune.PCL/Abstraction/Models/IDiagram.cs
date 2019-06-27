using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
  public  interface IDiagram
    {
        DateTime CurrentDate { get; set; }
        int TestCount { get; set; }
    }
}
