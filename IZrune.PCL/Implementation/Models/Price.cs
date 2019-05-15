using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    class Price : IPrice
    {
        public int months { get; set; }
        public int price { get; set; }
    }
}
                
