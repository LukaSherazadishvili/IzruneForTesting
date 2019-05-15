using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IPrice
    {
         int months { get; set; }
         int price { get; set; }
    }
}
