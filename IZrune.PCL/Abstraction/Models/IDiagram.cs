using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
  public  interface IDiagram
    {
        string CurrentDate { get; set; }
        int TestCount { get; set; }

    }
}
