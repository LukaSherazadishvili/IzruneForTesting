using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IPrice
    {
         DateTime? StartDate { get; set; }
         DateTime ?EndDate { get; set; }
        int? price { get; set; }
        int? MonthCount { get; set; }
        string Period { get; set; }
    }
}
