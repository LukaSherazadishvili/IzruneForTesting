using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IAnswer
    {
        string id { get; set; }
         string title { get; set; }
         string right { get; set; }
    }
}
