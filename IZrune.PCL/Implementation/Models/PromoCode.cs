using IZrune.PCL.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Implementation.Models
{
    public class PromoCode : IPromoCode
    {
        public IEnumerable<IPrice> Prices { get; set; }
        public string PrommoCode { get; set; }
    }
}
