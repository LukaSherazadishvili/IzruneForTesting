using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class Price : IPrice
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int price { get; set; }
        public int MonthCount { get; set; }
    }
}
                
