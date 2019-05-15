using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IPromoCode
    {
        string PrommoCode { get; set; }
        IEnumerable<IPrice> Prices { get; set; }
    }
}
