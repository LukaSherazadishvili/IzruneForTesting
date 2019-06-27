using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class Diagram : IDiagram
    {
        public DateTime CurrentDate { get; set; }
        public int TestCount { get; set; }
    }
}
