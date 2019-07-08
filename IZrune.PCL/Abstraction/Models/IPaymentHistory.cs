using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Models
{
   public interface IPaymentHistory
    {
        string StudentName { get; set; }
        DateTime? Date { get; set; }
        int Amount { get; set; }
    }
}
